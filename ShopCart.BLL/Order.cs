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


        public void SubmitOrder(List<Product> catalogProducts )
        {
            orderNumber = _jsonRepo.IncrementFile();

            var orderDetails = new List<string>();
            orderDetails.Add("Order Number: " + orderNumber);
            orderDetails.Add(_notificationServce.ToString());
            orderDetails.Add(_paymentProcessor.Process());
            foreach (var orderItem in _cart.OrderRepo.Items)
            {
                orderDetails.Add(_cart.OrderRepo.ReturnDisplay(orderItem));
            }

            UpdateOnHand(catalogProducts);
            SaveNewOrder(orderDetails); 
        }

        private void UpdateOnHand(List<Product> catalogProducts)
        {
            _jsonRepo.SaveProducts(catalogProducts);
        }

        private void SaveNewOrder(List<string> orderDetails )
        {
            _jsonRepo.CreateOrderFile(orderDetails, orderNumber);
        }

    }
}