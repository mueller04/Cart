using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCart.BLL
{
    public class Product
    {
        public string Name { get; set; }
        public int OnHand { get; set; }
        public decimal Price { get; set; }
        public int PendingOnHand { get; set; }
    }
}
