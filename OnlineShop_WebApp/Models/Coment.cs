using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_WebApp.Models
{
    public class Coment
    {
        public int Id { get; set; }
        public int ProdutId { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
    }
}
