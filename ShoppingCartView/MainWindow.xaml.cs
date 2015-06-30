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

        public MainWindow()
        {
            InitializeComponent();

            notification = new NotificationService();
            paymentProcessor = new MikesProcessor();
            orderRepo = new OrderItemRepository();
            ActiveCart = new Cart(orderRepo);
            productRepo = new JsonProductRepository();


            ListProductsInCatalog(productRepo);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItemToCart();
        }

        public void AddItemToCart()
        {
            if (lstSelection.SelectedIndex != -1)
            {
                int quantitySelected;
                int.TryParse(cbQuantity.SelectedItem.ToString(), out quantitySelected);
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
                lstDisplay.Items.Add(orderRepo.ReturnDisplay(cartItem));  
            }
            cartItemsList.Clear();
        }

        private void SubmitOrder()
        {
            ActiveOrder = new Order(notification, ActiveCart, paymentProcessor);
            ActiveOrder.SubmitOrder(catalogProducts);
            lstDisplay.Items.Clear();
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
                lstSelection.Items.Add(prod.Name);   
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
                var regex = new Regex("\\d");
                var displayItemName = regex.Replace(lstDisplay.SelectedItem.ToString(), String.Empty);
                char[] charsToTrim = {':', ' '};          
                displayItemName = displayItemName.TrimEnd(charsToTrim);

                orderRepo.Remove(displayItemName, catalogProducts);
                DisplayOrderView(); 
            }
        }

    }
}
