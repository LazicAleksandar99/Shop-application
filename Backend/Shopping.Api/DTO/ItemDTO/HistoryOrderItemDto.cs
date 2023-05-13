﻿using System.ComponentModel.DataAnnotations;

namespace Shopping.Api.DTO.ItemDTO
{
    public class HistoryOrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
    }
}
