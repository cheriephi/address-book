using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace ConsoleAddress
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

        #region Menu items
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;
                using (var input = File.OpenRead(fileName))
                {
                    var controller = new Controller();
                    var addressBook = controller.Load(input);

                    //TODO: This is not properly refreshing the data
                    addressBookViewModel.AddressBook = addressBook;
                    this.DataContext = addressBookViewModel;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;

                using (var output = File.Create(fileName))
                {
                    var controller = new Controller();
                    controller.Save(output, addressBookViewModel.AddressBook);
                }
            }
        }
        #endregion
    }
}
