using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Newtonsoft.Json;
using ShopCart.BLL.Interfaces;

namespace ShopCart.BLL
{
    public class JsonProductRepository :IProductRepository
    {
        readonly JsonSerializer _serializer = new JsonSerializer();
        public List<Product> ProductsOnHand { get; private set; }


        public List<Product> GetProducts()
        {
            using (StreamReader file = File.OpenText(@"c:\Users\mike\Documents\Visual Studio 2013\Projects\ShoppingCart\files.json"))
            {
                ProductsOnHand = (List<Product>)_serializer.Deserialize(file, typeof(List<Product>));
                return ProductsOnHand;;
            }      
        }

        // TO DO - see if it can save json with the nicer formatting
        public void SaveProducts(List<Product> productsToSave)
        {
            using (StreamWriter sw = new StreamWriter(@"c:\Users\mike\Documents\Visual Studio 2013\Projects\ShoppingCart\files.json"))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, productsToSave);
            }
        }


        public void CreateOrderFile(List<string> orderDetails, int orderNumber 
            )
        {
            using (
                StreamWriter sw =
                    new StreamWriter(@"c:\Users\mike\Documents\Visual Studio 2013\Projects\ShoppingCart\Order\order" + orderNumber +".json"))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, orderDetails);
            }
 

        }

        public int IncrementFile()
        {
            int incrementor;
            using (
                StreamReader file =
                    File.OpenText(
                        @"c:\Users\mike\Documents\Visual Studio 2013\Projects\ShoppingCart\orderNumberIncrementor.json")
                )
            {
                incrementor = (int)_serializer.Deserialize(file, typeof (int));
            }

            incrementor++;

            using (StreamWriter sw = new StreamWriter(@"c:\Users\mike\Documents\Visual Studio 2013\Projects\ShoppingCart\orderNumberIncrementor.json"))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, incrementor);
            }

            return incrementor;


        }


    }
}
