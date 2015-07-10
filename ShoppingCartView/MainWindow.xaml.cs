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
            decimal TotalPrice = 0;

            var cartItemsList = ActiveCart.GetItemsInCart();
            foreach (var cartItem in cartItemsList)
            {
                lstDisplay.Items.Add(Helpers.ReturnDisplay(cartItem));
                
                TotalPrice += cartItem.Price;
            }
            //TODO this should be returning the currency format with $ to the order total in the UI but it doesn't
            txtTotal.Text = String.Format("{0:C}",TotalPrice.ToString());
            cartItemsList.Clear();
        }

        private void SubmitOrder()
        {
            ActiveOrder = new Order(notification, ActiveCart, paymentProcessor);
            ActiveOrder.SubmitOrder(catalogProducts);
            lstDisplay.Items.Clear();
            txtTotal.Clear();
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

        //TODO - adding the dollar to the string in the cart listbox broke this remove method.  The problem now is the regex
        //here does not remove all of the characters like the $ anymore.  Find a way to use regex to remove eveything
        //probably put that into the helper method, remove the regex code from here and call the Helper. method from here.
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
