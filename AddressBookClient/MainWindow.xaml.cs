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
        private List<AddressViewModel> addresses;

    public MainWindow()
        {
            InitializeComponent();
            controller = new Controller();
            AddressViewModel.AddressItemBook = new AddressBook();
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

                    AddressViewModel.AddressItemBook = addressBook;

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
                    controller.Save(output, AddressViewModel.AddressItemBook);
                }
            }
        }
        #endregion

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            addresses = new List<AddressViewModel>{
                new AddressViewModel("Mom", "9026 SE 60th St", "Mercer Island", "WA", "98040", "USA"),
                new AddressViewModel("Me", "7721 11th Ave NW", "Seattle", "WA", "98117", "USA"),
                new AddressViewModel("You", "One Main Street", "San Francisco", "CA", "94117", "USA"),
            };
            var grid = sender as DataGrid;
            grid.ItemsSource = addresses;
        }
    }
}
