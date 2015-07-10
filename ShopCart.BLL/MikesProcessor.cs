using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopCart.BLL.Interfaces;

namespace ShoppingCart
{
    public class MikesProcessor : IPaymentProcessor
    {
        public string Process()
        {
            return "PayPal Processed Successfully";
        }
    }
}
