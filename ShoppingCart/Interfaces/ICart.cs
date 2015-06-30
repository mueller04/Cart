using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface ICart
    {
        List<OrderItem> Items { get; set; }
        void Add(OrderItem item);
    }
}
