﻿using System.Collections.Generic;
using ShopCart.BLL.Interfaces;
using ShoppingCart;

namespace ShopCart.BLL
{
    //test
    public class Cart
    {
        public IOrderItemRepository OrderRepo { get; set; }

        public Cart(IOrderItemRepository orderRepo)
        {
            OrderRepo = orderRepo;
        }

        public List<OrderItem> GetItemsInCart()
        {
            var resultlist = new List<OrderItem>();
            foreach (OrderItem item in OrderRepo.CartItems)
            {
                resultlist.Add(item);
            }
            return resultlist;
        }
        
    }
}
