using System.Collections.ObjectModel;

namespace ConsoleAddress
{
    /// <summary>
    /// Communicates between the view and the model.
    /// </summary>
    class AddressBookViewModel
    {
        public ObservableCollection<AddressViewModel> Addresses { get; set; }

        public AddressBookViewModel()
        {
            AddressViewModel.AddressItemBook = new AddressBook();

            this.Addresses = new ObservableCollection<AddressViewModel>
            {
                new AddressViewModel("Michelle Obama", "1600 Pennsylvania Ave", "Washington", "DC", "20500", "USA")
            };

        }

        public AddressBookViewModel(AddressBook addressBook)
        {
            AddressViewModel.AddressItemBook = addressBook;
        }
    }
}
