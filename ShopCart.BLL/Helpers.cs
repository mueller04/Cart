using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ShoppingCart;

namespace ShopCart.BLL
{
    public static class Helpers
    {
        public static string GetStringNameOnly(string inputStr)
        {
            string[] nameOnlystr = inputStr.Split(':');
            return nameOnlystr[0];
        }

        public static string ReturnDisplay(OrderItem item)
        {
            return item.Name + ": " + item.Quantity + ": " + String.Format("{0:C}", item.Price);
        }

        public static string ReturnDisplay(Product product)
        {
            return product.Name + ": " + product.OnHand;
        }


    }
}
