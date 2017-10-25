using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ConsoleAddress
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Menu items
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;
            }
        }
        #endregion

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var addresses = new List<AddressItem>{};
            var grid = sender as DataGrid;
            grid.ItemsSource = addresses;
        }
    }
}
