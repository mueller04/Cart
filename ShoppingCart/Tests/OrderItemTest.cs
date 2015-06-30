using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ShoppingCart.Tests
{
    [TestFixture]
    class OrderItemTest
    {

        [Test]
        public void IncrementCableQuantityByOne()
        {
            //Arrange
            OrderItem item = new OrderItem("cable", 1);

            //Act
            item.AddToTotal(3);

            //Assert
            Assert.AreEqual(item.Quantity, 4);
        }




    }
}
