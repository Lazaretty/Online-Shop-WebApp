using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_WebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
