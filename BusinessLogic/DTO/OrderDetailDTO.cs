﻿namespace BusinessLogic.DTO
{
    public class OrderDetailDTO
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
