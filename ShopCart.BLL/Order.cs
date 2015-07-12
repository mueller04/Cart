using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using ShopCart.BLL;
using ShopCart.BLL.Interfaces;

namespace ShoppingCart
{
    public class Order
    {
        private readonly INotificationService _notificationServce;
        private readonly Cart _cart;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly JsonProductRepository _jsonRepo = new JsonProductRepository();
        private int orderNumber; 


        public Order(INotificationService notificationService, Cart cart, IPaymentProcessor payment)
        {
            _notificationServce = notificationService;
            _paymentProcessor = payment;
            _cart = cart;
        }


        public void SubmitOrder(List<Product> catalogProducts, decimal totalPrice)
        {
            orderNumber = _jsonRepo.IncrementFile();

            var orderDetails = new List<string>();
            orderDetails.Add("Order Number: " + orderNumber);
            orderDetails.Add(_notificationServce.ToString());
            orderDetails.Add(_paymentProcessor.Process());
            foreach (var orderItem in _cart.OrderRepo.CartItems)
            {
                orderDetails.Add(Helpers.ReturnDisplay(orderItem));
            }
            //TODO the below line is throwing an invalidFormat Exception even though I use the same format method in MainWindow.xaml.cs as I result I cn't pass this to orderDetails.Add
            //string totalPriceString = String.Format("{O:C}", totalPrice);
            orderDetails.Add(" Total is: " + totalPrice);

            UpdateOnHand(catalogProducts);
            SaveNewOrder(orderDetails); 
        }

        private void UpdateOnHand(List<Product> catalogProducts)
        {
            foreach (Product product in catalogProducts)

            product.OnHand = product.PendingOnHand;
            _jsonRepo.SaveProducts(catalogProducts);
        }

        private void SaveNewOrder(List<string> orderDetails )
        {
            _jsonRepo.CreateOrderFile(orderDetails, orderNumber);
        }

    }
}