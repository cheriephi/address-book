using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ConsoleAddress
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller;
        private List<AddressItem> addresses;

    public MainWindow()
        {
            InitializeComponent();
            controller = new Controller();
            AddressItem.AddressItemBook = new AddressBook();
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
                    var addressBook = controller.Load(input);

                    AddressItem.AddressItemBook = addressBook;

                    throw new NotImplementedException(); // TODO: Need to set the data binding so it refreshes
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
                    controller.Save(output, AddressItem.AddressItemBook);
                }
            }
        }
        #endregion

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            addresses = new List<AddressItem>{};
            var grid = sender as DataGrid;
            grid.ItemsSource = addresses;
        }
    }
}
