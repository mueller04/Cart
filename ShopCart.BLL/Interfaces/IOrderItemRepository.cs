using System.Collections.Generic;
using ShoppingCart;

namespace ShopCart.BLL.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> Items { get; set; }
        string Add(string name, int quantity, List<Product> catalogProducts);
        string ReturnDisplay(OrderItem item);
        void Remove(string name, List<Product> catalogProducts);
    }
}
