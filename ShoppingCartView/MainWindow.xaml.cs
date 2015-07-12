using ShopCart.BLL;
using ShopCart.BLL.Interfaces;
using ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ShoppingCartView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        private INotificationService notification;
        private IPaymentProcessor paymentProcessor;
        private IProductRepository productRepo;
        private Cart ActiveCart;
        private Order ActiveOrder;
        public IOrderItemRepository orderRepo;
        private List<Product> catalogProducts;
        decimal TotalPrice = 0;

        public MainWindow()
        {
            InitializeComponent();
            productRepo = new JsonProductRepository();
            notification = new NotificationService();
            paymentProcessor = new MikesProcessor();
            orderRepo = new OrderItemRepository();
            ActiveCart = new Cart(orderRepo);
            ListProductsInCatalog(productRepo);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItemToCart();
        }

        //TODO after adding items the on hand doesn't decrement from the cataloglist because the order was not submitted yet
        //Change how the onhand is displayed in the cataloglist so that it decrements appropriately when adding to catalog
        //and increments when hitting remove from catalog button
        public void AddItemToCart()
        {
            if (lstSelection.SelectedIndex != -1)
            {
                int quantitySelected;
                int.TryParse(cbQuantity.SelectedItem.ToString(), out quantitySelected);
                //TODO probably return an action result object from orderRepo.Add so I can return the strings as well as the successful number of items I added so I can safely change the display by decremening the pending on hand by the amount just added / also add these features in reverse for the remove button
                string addMessage = orderRepo.Add(lstSelection.SelectedItem.ToString(), quantitySelected, catalogProducts);
                if (addMessage != "")
                {
                    MessageBox.Show(addMessage);
                }
                DisplayOrderView();     
            }
        }

        public void DisplayOrderView()
        {
            lstDisplay.Items.Clear();
            var cartItemsList = ActiveCart.GetItemsInCart();
            foreach (var cartItem in cartItemsList)
            {
                lstDisplay.Items.Add(Helpers.ReturnDisplay(cartItem));
                TotalPrice += cartItem.Price * cartItem.Quantity;
            }
            txtTotal.Text = String.Format("{0:C}",TotalPrice);
            cartItemsList.Clear();
        }

        private void SubmitOrder()
        {
            ActiveOrder = new Order(notification, ActiveCart, paymentProcessor);
            ActiveOrder.SubmitOrder(catalogProducts, TotalPrice);
            lstDisplay.Items.Clear();
            lstSelection.Items.Clear();
            txtTotal.Clear();
            ListProductsInCatalog(productRepo);
            TotalPrice = 0;
            MessageBox.Show("Order Submitted Successfully");
        }

        private void btnSubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            SubmitOrder();
        }

        public void ListProductsInCatalog(IProductRepository productRepo)
        {
            catalogProducts = productRepo.GetProducts();
            foreach (Product prod in catalogProducts)
            {
                lstSelection.Items.Add(Helpers.ReturnDisplay(prod));   
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbQuantity_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> cbData = new List<string>();

            for (int i = 1; i < 10; i++)
            {
                cbData.Add(i.ToString());
            }

            var cbQuantityReference = sender as ComboBox;

            if (cbQuantityReference != null)
            {
                cbQuantityReference.ItemsSource = cbData;

                cbQuantityReference.SelectedIndex = 0;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem();
        }

        private void RemoveItem()
        {
            if (lstDisplay.SelectedIndex != -1)
            {
                var displayItemName = Helpers.GetStringNameOnly(lstDisplay.SelectedItem.ToString());
                orderRepo.Remove(displayItemName, catalogProducts);
                DisplayOrderView(); 
            }
        }

        private void txtTotal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
