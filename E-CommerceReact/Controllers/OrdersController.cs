using E_CommerceReact.Data;
using E_CommerceReact.DTO;
using E_CommerceReact.Entities;
using E_CommerceReact.Entities.OrderAggregate;
using E_CommerceReact.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceReact.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly StoreContext storeContext;

        public OrdersController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            var orders = await storeContext.Orders
                .ProjectOrderToOrderDto()
                .Where(x => x.BuyerId == User.Identity.Name)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await storeContext.Orders
                .Include(o => o.OrderItems)
                .Where(x => x.Id == id && x.BuyerId == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (order == null) return NotFound();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder(CreateOrderDTO orderDTO)
        {
            var basket = await storeContext.Baskets
                .RetrieveBasketWithItems(User.Identity.Name)
                .FirstOrDefaultAsync();

            if (basket == null) return BadRequest(new ProblemDetails { Title = "Basket not found" });

            var items = new List<OrderItems>();

            foreach (var item in basket.Items)
            {
                var productItem = await storeContext.Products.FindAsync(item.ProductId);

                if (productItem == null) return BadRequest(new ProblemDetails { Title = "Product not found" });

                var itemOrdered = new ProductItemOrdered 
                {
                    ProductId = productItem.Id,
                    Name = productItem.Name,
                    PictureUrl = productItem.PictureUrl
                };

                var orderItem = new OrderItems
                {
                    ItemOrdered = itemOrdered,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
                productItem.QuantityInStock -= item.Quantity;
            };

            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var deliveryFee = subtotal > 10000 ? 0 : 500;

            var order = new Order
            {
                BuyerId = User.Identity.Name,
                OrderItems = items,
                ShippingAddress = orderDTO.ShippingAddress,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
            };

            storeContext.Orders.Add(order);
            storeContext.Baskets.Remove(basket);

            if (orderDTO.SaveAddress)
            {
                var user = await storeContext.Users
                    .Include(a => a.Address)
                    .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                var address = new UserAddress
                {
                    FullName = orderDTO.ShippingAddress.FullName,
                    Address1 = orderDTO.ShippingAddress.Address1,
                    Address2 = orderDTO.ShippingAddress.Address2,
                    City = orderDTO.ShippingAddress.City,
                    Zip = orderDTO.ShippingAddress.Zip,
                    Country = orderDTO.ShippingAddress.Country,
                };
                user.Address = address;
            }

            var result = await storeContext.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetOrder", new { id = order.Id }, order.Id);

            return BadRequest(new ProblemDetails { Title = "Problem creating order" });
        }
    }
}
