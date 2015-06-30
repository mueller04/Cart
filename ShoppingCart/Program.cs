using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Program
    {
        private static Cart _cart = new Cart();

        static void Main(string[] args)
        {

            bool continueProgram = true;

            while (continueProgram)
            {
                while (continueProgram)
                {
                    Selection();
                    Console.WriteLine("Complete Order? 1) Yes 2) No");
                    var choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        continueProgram = false;
                    }
                }
                INotificationService notificationService = new NotificationService();
                IPaymentProcessor paymentProcessor = new MikesProcessor();
                Order order = new Order(notificationService, _cart, paymentProcessor);
                foreach (var item in order.DisplayOrder())
                {
                    Console.WriteLine(item);
                }
                Console.ReadKey();
            }


           
        }

        private static void Selection()
        {
            Console.WriteLine("Order an item, 1) cable, 2) screen, 3) charger, 4) no item");
            string select = Console.ReadLine();
            string itemname = "";

            switch (select)
            {
                case "1":
                    itemname = "cable";
                    break;
                case "2":
                    itemname = "screen";
                    break;
                case "3":
                    itemname = "charger";
                    break;
                case "4":    
                    break;
                default:
                    break;
            }
            if (!String.IsNullOrWhiteSpace(itemname))
            {
                OrderItem item = new OrderItem(itemname, 1);
                _cart.Add(item);
            }
            else
            {
                Console.WriteLine("Unrecognized");
            }

            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
        }
    }
}
