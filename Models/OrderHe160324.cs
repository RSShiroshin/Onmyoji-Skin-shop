using System;
using System.Collections.Generic;

namespace PRN_Project2.Models
{
    public partial class OrderHe160324
    {
        public int OrderId { get; set; }
        public string? UserName { get; set; }
        public string? Receiver { get; set; }
        public string? ProductId { get; set; }
        public bool? PaymentMethod { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? Price { get; set; }
        public bool? StatusDb { get; set; }
        public bool? Done { get; set; }

        public OrderHe160324(string? userName, string? receiver, string? productId, bool? paymentMethod, DateTime? orderDate, double? price, bool? statusDb, bool? done)
        {
            UserName = userName;
            Receiver = receiver;
            ProductId = productId;
            PaymentMethod = paymentMethod;
            OrderDate = orderDate;
            Price = price;
            StatusDb = statusDb;
            Done = done;
        }

        public virtual ProductHe160324? Product { get; set; }
        public virtual UserHe160324? UserNameNavigation { get; set; }
    }
}
