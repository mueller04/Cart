using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCart.BLL.Interfaces
{
    public interface IProductRepository
    { 
        List<Product> GetProducts();
        void SaveProducts(List<Product> productsToSave);
        void CreateOrderFile(List<string> orderDetails, int orderNumber);
    }
}
