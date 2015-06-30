using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework;
using ShoppingCart;
using Moq;
using ShopCart.BLL;
using ShopCart.BLL.Interfaces;


namespace ShoppingCartView.Tests
{
    [TestFixture]
    class ShoppingCartViewTests
    {
        //[Test]
        //public void DisplayOrderTest()
        //{
        ////This Example Moq does not actually work the collectionAssert gives matching 
        //    //results even if I go into displayOrder method and break the code
        ////Arrange
        //    List<OrderItem> testList = new List<OrderItem>()
        //    {
        //        new OrderItem{Name = "testitem1", Quantity = 2},
        //        new OrderItem{Name = "testitem2", Quantity =7}
        //    };


        //INotificationService Notification = new NotificationService();
        //IPaymentProcessor PaymentProcessor = new MikesProcessor();
        ////Cart ActiveCart = new Cart();
        //var MockCart = new Mock<IOrderItemRepository>();
        //    MockCart.SetupAllProperties();
        //    MockCart.Object.Items = testList;
        //    Order ActiveOrder = new Order(Notification, MockCart.Object, PaymentProcessor);


        ////Act
        //    CollectionAssert.AreEqual(ActiveOrder.DisplayOrder(), testList);
        //}

        [Test]
        public void ProdRepoTest()
        {
            IProductRepository prodRepo = new JsonProductRepository();
            var prodList = prodRepo.GetProducts();
            Assert.AreEqual("cable", prodList[0].Name);
            Assert.AreEqual(5, prodList[0].OnHand);
            Assert.AreEqual("charger", prodList[1].Name);
            Assert.AreEqual(4, prodList[1].OnHand);
        }

        [RequiresSTA]
        [Test]
        public void GetProductRepositoryTestIsCalled()
        {
            //Arrange
            MainWindow windowTest = new MainWindow();
            var MockNotificationSvc = new Mock<INotificationService>();
            var MockPaymentProcessor = new Mock<IPaymentProcessor>();
            var MockProductRepo = new Mock<IProductRepository>();
            OrderItemRepository orderRepo = new OrderItemRepository();
            Cart ActiveCart = new Cart(orderRepo);


            List<Product> catalogProducts = new List<Product>()
            {
                new Product(){Name = "cable", OnHand = 5},
                new Product(){Name = "charger", OnHand = 4},
                new Product() {Name = "battery", OnHand = 3}
            };

            MockProductRepo.Setup(x => x.GetProducts()).Returns(catalogProducts);

            //Act
            windowTest.ListProductsInCatalog(MockProductRepo.Object);
            
            Assert.AreEqual(true, windowTest.lstSelection.Items.Contains("cable"));
            MockProductRepo.Verify(x => x.GetProducts());
        }
        //TO DO - cbQuantity is not initializing it's list of strings (1 - 9) throwing nullref ex.  since its an event i probably have to invoke it somehow during my arrange.
        [RequiresSTA]
        [Test]
        public void WhenTwoisSelectedAndAddisClickedTwoProductsAreAdded()
        {
           //Arrange
            MainWindow windowTest = new MainWindow();
            
            
            List<Product> catalogProducts = new List<Product>()
            {
                new Product(){Name = "cable", OnHand = 5},
                new Product(){Name = "charger", OnHand = 4},
                new Product() {Name = "battery", OnHand = 3}
            };

            //Act
            windowTest.cbQuantity.SelectedItem = "2";
            windowTest.lstSelection.SelectedItem = "battery";
            windowTest.AddItemToCart();

            //Assert
            Assert.AreEqual(2, windowTest.orderRepo.Items[3].Quantity);
        }


    }
}
