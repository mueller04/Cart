using ShoppingCart.Interfaces;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ShoppingCart
{
    public class Order
    {
        private readonly INotificationService _notificationServce;
        private readonly ICart _cart;
        private readonly IPaymentProcessor _paymentProcessor;



        public Order(INotificationService notificationService, ICart cart, IPaymentProcessor payment)
        {
            _notificationServce = notificationService;
            _paymentProcessor = payment;
            _cart = cart;
        }

        public void SubmitOrder()
        {
            foreach (OrderItem item in _cart.Items)
            Console.WriteLine("{0}{1}{3}{4}", item.Name, item.Quantity, _notificationServce.SendEmail(), _paymentProcessor.Process());
        }

        public IEnumerable<string> DisplayOrder()
        {
            
            foreach (OrderItem item in _cart.Items)
            {
                yield return item.ToString();
            }
        }

    }

}
