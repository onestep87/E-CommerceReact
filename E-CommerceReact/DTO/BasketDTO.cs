﻿using Microsoft.AspNetCore.Mvc;

namespace E_CommerceReact.DTO
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}