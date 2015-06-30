using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ShopCart.BLL.Interfaces;

namespace ShopCart.BLL.Tests
{   
    [TestFixture]
    class BLLTest
    {
      


       /*
        [Test]
        public void IsDatabaseConnected()
        {
            var conn = DBFactory.GetLocalDbConnection().State;

            if (conn == ConnectionState.Open)
            {
                Console.WriteLine("You are connected");
                Console.WriteLine("The connection is " + conn);
            }

            Assert.AreEqual(DBFactory.GetLocalDbConnection().State, ConnectionState.Open);
        }
    */
     
        [Test]
        public void WhenAddIsCalledNotEnoughInventoryCausesStringReturn()
        {
            //Arrange
            //var mockOrderItemRepo = new Mock<IOrderItemRepository>();
            var orderItemRepo = new OrderItemRepository();
            List<Product> catalogProducts = new List<Product>()
            {
                new Product(){Name = "cable", OnHand = 5},
                new Product(){Name = "charger", OnHand = 4},
                new Product() {Name = "battery", OnHand = 3}
            };

            //Act
            var message = orderItemRepo.Add("battery", 4, catalogProducts);

            //Assert
            Assert.AreEqual("Not enough inventory.", message);
        }

        [Test]
        public void WhenAddIsCalledOnHandDecrements()
        {
            var orderItemRepo = new OrderItemRepository();
            List<Product> catalogProducts = new List<Product>()
            {
                new Product(){Name = "cable", OnHand = 5},
                new Product(){Name = "charger", OnHand = 4},
                new Product() {Name = "battery", OnHand = 3}
            };

            //Act
            var message = orderItemRepo.Add("battery", 2, catalogProducts);

            //Assert
            Assert.AreEqual(1, catalogProducts[2].OnHand);
        }

        [Test]
        public void WhenAddisCalledOnOnlyOneTypeofItemItRestrictsAddPerTheRightOnHand()
        {
            var orderItemRepo = new OrderItemRepository();
            List<Product> catalogProducts = new List<Product>()
            {
                new Product(){Name = "cable", OnHand = 5},
                new Product(){Name = "charger", OnHand = 4},
                new Product() {Name = "battery", OnHand = 3}
            };

            //Act

            var message = orderItemRepo.Add("battery", 1, catalogProducts);
            var messagetwo = orderItemRepo.Add("battery", 1, catalogProducts);
            var messagethree = orderItemRepo.Add("battery", 1, catalogProducts);
            var messagefour = orderItemRepo.Add("battery", 1, catalogProducts);
            

            //Assert
            Assert.AreEqual(0, catalogProducts[2].OnHand);
            Assert.AreEqual("Not enough inventory.", messagefour);
        }




    }
}
