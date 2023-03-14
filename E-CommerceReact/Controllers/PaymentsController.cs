using E_CommerceReact.Data;
using E_CommerceReact.DTO;
using E_CommerceReact.Entities;
using E_CommerceReact.Entities.OrderAggregate;
using E_CommerceReact.Extensions;
using E_CommerceReact.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace E_CommerceReact.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly PaymentService paymentService;
        private readonly StoreContext storeContext;
        private readonly IConfiguration configuration;

        public PaymentsController(PaymentService paymentService, UserManager<User> userManager, StoreContext storeContext, IConfiguration configuration)
        {
            this.paymentService = paymentService;
            this.storeContext = storeContext;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
        {
            var basket = await storeContext.Baskets
                .RetrieveBasketWithItems(User.Identity.Name)
                .FirstOrDefaultAsync();

            if (basket == null) return NotFound();

            var intent = await paymentService.CreateOrUpdatePaymentIntent(basket);

            if (intent == null) return BadRequest(new ProblemDetails { Title = "Problem with your Payment Intent" });

            basket.PaymentIntentId = basket.PaymentIntentId ?? intent.Id;
            basket.ClientSecret = basket.ClientSecret ?? intent.ClientSecret;

            storeContext.Update(basket);

            var result = await storeContext.SaveChangesAsync() > 0;

            if (!result) return BadRequest(new ProblemDetails { Title = "Problem saving changes" });

            return basket.MapBasketToDTO();
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
                configuration["StripeSettings:WhSecret"]);

            var charge = stripeEvent.Data.Object as Charge;

            var order = await storeContext.Orders.FirstOrDefaultAsync(x => x.PaymentIntentId == charge.PaymentIntentId);

            if (charge.Status == "succeeded")
            {
                order.OrderStatus = OrderStatus.PaymentReceived;
            }

            await storeContext.SaveChangesAsync();

            return new EmptyResult();
        }

    }
}
