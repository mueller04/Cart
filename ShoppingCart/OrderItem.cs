using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{

    public class OrderItem
    {
        public string Name { get; set; }
        public int Quantity { get; private set; }


        public OrderItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public void AddToTotal(int quantity)
        {
            Quantity += quantity;
        }

        public override string ToString()
        {
            return Name + ": " + Quantity.ToString();
        }
    }
}
