using ShopCart.BLL.Interfaces;

namespace ShoppingCart
{
    public class NotificationService : INotificationService
    {
        //Make this take an email address so that is included in the order.json
        public string SendEmail()
        {
            return "you got mail";
        }
    }
}
