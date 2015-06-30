using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class NotificationService : INotificationService
    {
        public string SendEmail()
        {
            return "you got mail";
        }
    }
}
