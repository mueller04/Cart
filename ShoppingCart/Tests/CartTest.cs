using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShoppingCart.Tests
{
    [TestFixture]
    class CartTest
    {

        [Test]
        public void AddCableToCart()
        {
            //Arrange
            Cart cart = new Cart();
            OrderItem item = new OrderItem("cable", 1);
            
            //Act
            cart.Add(item);
            
            //Assert
            OrderItem match = cart.Items.Find(x => x.Name == "cable");
            Assert.AreEqual(match.Name, "cable");
            Assert.AreEqual(match.Quantity, 1);
        }






    }
}
