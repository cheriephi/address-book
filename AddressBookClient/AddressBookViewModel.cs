using ConsoleAddress;
using System.Collections.ObjectModel;
using System.IO;

namespace AddressBookClient
{
    /// <summary>
    /// Communicates between the view and the model.
    /// </summary>
    class AddressBookViewModel
    {
        private ObservableCollection<AddressViewModel> addresses;

        public AddressBookViewModel()
        {
            this.addresses = new ObservableCollection<AddressViewModel>
            {
                new AddressViewModel("Joe Bloggs", "1 New St.", "Birmingham", "England", "B01 3TN", "UK"),
                new AddressViewModel("Michelle Obama", "1600 Pennsylvania Ave", "Washington", "DC", "20500", "USA")
            };
        }

        public ObservableCollection<AddressViewModel> Addresses
        {
            get
            {
                return addresses;
            }

            set
            {
                addresses = value;
            }
        }

        #region Persistence
        /// <summary>
        /// Loads the address book data from the input stream.
        /// </summary>
        /// <param name="input"></param>
        public void Load(Stream input)
        {
            var controller = new Controller();
            var addressBook = controller.Load(input);

            // Refresh the addresses in this class
            var addressBookItems = addressBook.GetAll();
            addresses.Clear();
            foreach (var address in addressBookItems)
            {
                var addressViewModel = new AddressViewModel(address.Key, address.Value.getSpec("street"), address.Value.getSpec("city"), address.Value.getSpec("state"), address.Value.getSpec("zip"), address.Value.getSpec("country"));
                addresses.Add(addressViewModel);
            }
        }

        /// <summary>
        /// Saves the address book data to the output stream.
        /// </summary>
        /// <param name="output"></param>
        public void Save(Stream output)
        {
            // Populate the domain class with the view model's data
            var addressBook = new AddressBook();
            foreach (var address in addresses)
            {
                var addressItem = new Address(address.Street, address.City, address.State, address.Zip, address.Country);
                addressBook.Add(address.Name, addressItem);
            }

            // Persist it using the domain object's method
            var controller = new Controller();
            controller.Save(output, addressBook);
        }
        #endregion
    }
}
