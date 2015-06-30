using ShoppingCart.Interfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class Cart : ICart
    {

        public List<OrderItem> Items { get; set; }


        public Cart()
        {
            Items = new List<OrderItem>();
        }

        public void Add(OrderItem item)
        {
            Items.Add(item);
        }
    }


}
