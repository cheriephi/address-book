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
                using (var input = File.OpenRead(dialog.FileName))
                {
                    addressBookViewModel.Load(input);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                using (var output = File.Create(dialog.FileName))
                {
                    addressBookViewModel.Save(output);
                }
            }
        }
        #endregion
    }
}
