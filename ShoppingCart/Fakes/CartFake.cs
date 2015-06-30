using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Fakes
{
    class CartFake : ICart
    {
        public List<OrderItem> Items { get; set; }

        public CartFake()
        {
            Items = new List<OrderItem>()
            {
                new OrderItem("myitem", 1)
                {
                    //Name = name;
                    // Quantity = quantity;
                }
            };

        }

        public void Add(OrderItem item)
        {
            
        }
    }
}
