using System;
using System.Collections.Generic;

namespace PRN_Project2.Models
{
    public partial class ProductHe160324
    {
        public ProductHe160324()
        {
            OrderHe160324s = new HashSet<OrderHe160324>();
        }

        public ProductHe160324(string productId, string? productName, string? shikiName, double? price, double? phonePrice, string? img, string? description, int? sold, int? sale)
        {
            ProductId = productId;
            ProductName = productName;
            ShikiName = shikiName;
            Price = price;
            PhonePrice = phonePrice;
            Img = img;
            Description = description;
            Sold = sold;
            Sale = sale;
        }

        public string ProductId { get; set; } = null!;
        public string? ProductName { get; set; }
        public string? ShikiName { get; set; }
        public double? Price { get; set; }
        public double? PhonePrice { get; set; }
        public string? Img { get; set; }
        public string? Description { get; set; }
        public int? Sold { get; set; }
        public int? Sale { get; set; }

        public virtual ICollection<OrderHe160324> OrderHe160324s { get; set; }
    }
}
