using E_CommerceReact.DTO;
using E_CommerceReact.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceReact.Extensions
{
    public static class BasketExtensions
    {
       public static BasketDto MapBasketToDTO(this Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> baskets, string buyerId)
        {
            return baskets.Where(basket => basket.BuyerId == buyerId)
                .Include(basket => basket.Items)
                .ThenInclude(item => item.Product);
        }
    }
}
