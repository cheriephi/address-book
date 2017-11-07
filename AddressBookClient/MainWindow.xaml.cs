using System.Windows;

namespace AddressBookClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AddressBookViewModel addressBookViewModel;

        public MainWindow()
        {
            InitializeComponent();

            addressBookViewModel = new AddressBookViewModel();
            this.DataContext = addressBookViewModel;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            addressBookViewModel.Save();
        }
    }
}
