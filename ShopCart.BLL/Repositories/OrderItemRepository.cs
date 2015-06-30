using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ShopCart.BLL.Interfaces;
using ShoppingCart;

namespace ShopCart.BLL
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public List<OrderItem> Items { get; set; }
        
        public OrderItemRepository()
        {
            Items = new List<OrderItem>();
        }

        public string Add(string name, int quantity, List<Product> catalogProducts)
        {
            OrderItem newItem = new OrderItem() { Name = name, Quantity = quantity};


            Product matchedCatalogProduct = catalogProducts.FirstOrDefault(z => z.Name == newItem.Name);
            int indexOfCatalogProduct = catalogProducts.FindIndex(t => t.Name == newItem.Name);

            if (matchedCatalogProduct.OnHand < newItem.Quantity)
            {
                return "Not enough inventory.";
            }

            if (Items.Count != 0)
            {
                var foundItem = Items.FirstOrDefault(x => x.Name == newItem.Name);

                if (foundItem != null)
                {
                    foundItem.Quantity += newItem.Quantity;                                    
                }
                else
                {
                    Items.Add(newItem);
                }
               catalogProducts[indexOfCatalogProduct].OnHand -= newItem.Quantity;
                return "";
            }
            else
            {
                Items.Add(newItem);
                catalogProducts[indexOfCatalogProduct].OnHand -= newItem.Quantity;
                return "";
            }
        }

        public void Remove(string name, List<Product> catalogProducts)
        {
            var matchedItemIndex = Items.FindIndex(x => x.Name == name);
            var onHandToRestore = Items[matchedItemIndex].Quantity;
            Items.RemoveAt(matchedItemIndex);

            var matchedCatalogIndex = catalogProducts.FindIndex(x => x.Name == name);
            catalogProducts[matchedCatalogIndex].OnHand += onHandToRestore;
        }

        public string ReturnDisplay(OrderItem item)
        {
            return item.Name + ": " + item.Quantity;
        }
    }
   
}
