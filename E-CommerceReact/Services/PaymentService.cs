using E_CommerceReact.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceReact.Services
{
    public class PaymentService
    {
        private readonly IConfiguration configuration;

        public PaymentService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Basket basket)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();

            var intent = new PaymentIntent();

            var subtotal = basket.Items.Sum(item => item.Quantity * item.Product.Price);
            var deliveryFee = subtotal > 10000 ? 0 : 500;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = subtotal + deliveryFee,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = subtotal + deliveryFee
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            return intent;

        }
    }
}
