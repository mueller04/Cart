using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using ShopCart.BLL.Interfaces;
using ShoppingCart;

namespace ShopCart.BLL
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public List<OrderItem> CartItems { get; set; }
        
        public OrderItemRepository()
        {
            CartItems = new List<OrderItem>();
        }

        public string Add(string name, int quantity, List<Product> catalogProducts)
        {
            string nameOnlystr = Helpers.GetStringNameOnly(name);

            OrderItem newItemToAddToCart = new OrderItem()
            {
                Name = nameOnlystr,
                Quantity = quantity,
            };

            Product matchedCatalogProduct = catalogProducts.FirstOrDefault(z => z.Name == newItemToAddToCart.Name);

            if (matchedCatalogProduct.Price == 0)
            {
                return ("There was no price listed in the Product file, item not added.");
            }

            newItemToAddToCart.Price = matchedCatalogProduct.Price;

            if (matchedCatalogProduct.PendingOnHand < newItemToAddToCart.Quantity)
            {
                return "Not enough inventory, item was not added.";
            }

            if (CartItems.Count != 0)
            {
                var foundItemInCart = CartItems.FirstOrDefault(x => x.Name == newItemToAddToCart.Name);

                if (foundItemInCart != null)
                {
                    foundItemInCart.Quantity += newItemToAddToCart.Quantity;
                }
                else
                {
                    CartItems.Add(newItemToAddToCart);
                }
                matchedCatalogProduct.PendingOnHand -= newItemToAddToCart.Quantity;
                return "";
            }
            else
            {
                CartItems.Add(newItemToAddToCart);
                matchedCatalogProduct.PendingOnHand -= newItemToAddToCart.Quantity;
                return "";
            }
        }

        public void Remove(string name, List<Product> catalogProducts)
        {
            var matchedItemIndex = CartItems.FindIndex(x => x.Name == name);
            var onHandToRestore = CartItems[matchedItemIndex].Quantity;
            CartItems.RemoveAt(matchedItemIndex);

            var matchedCatalogIndex = catalogProducts.FindIndex(x => x.Name == name);
            catalogProducts[matchedCatalogIndex].PendingOnHand += onHandToRestore;
        }
    }
}
