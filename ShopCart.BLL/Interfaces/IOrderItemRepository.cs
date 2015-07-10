using System.Collections.Generic;
using ShoppingCart;

namespace ShopCart.BLL.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> CartItems { get; set; }
        string Add(string name, int quantity, List<Product> catalogProducts);
        void Remove(string name, List<Product> catalogProducts);
    }
}
