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
            using (StreamReader file = File.OpenText(@"C:\Users\mike\Documents\GitHub\Cart\files.json"))
            {
                ProductsOnHand = (List<Product>)_serializer.Deserialize(file, typeof(List<Product>));
                return ProductsOnHand;;
            }      
        }

        // TO DO - see if it can save json with the nicer formatting
        public void SaveProducts(List<Product> productsToSave)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\mike\Documents\GitHub\Cart\files.json"))
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
                    new StreamWriter(@"C:\Users\mike\Documents\GitHub\Cart\Order\order" + orderNumber + ".json"))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, orderDetails);
            }
 

        }

        public int IncrementFile()
        {
            int incrementor = GetIncrementorNumberFromFile();
            incrementor++;
            WriteIncrementNumberToIncrementFile(incrementor);
            return incrementor;
        }

        public int GetIncrementorNumberFromFile()
        {
            using (
                StreamReader file =
                    File.OpenText(
                        @"C:\Users\mike\Documents\GitHub\Cart\orderNumberIncrementor.json")
                )
            {
                return (int) _serializer.Deserialize(file, typeof (int));
            }
        }

        public void WriteIncrementNumberToIncrementFile(int incrementor)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\mike\Documents\GitHub\Cart\orderNumberIncrementor.json"))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                _serializer.Serialize(writer, incrementor);
            }
        }

    }
}
