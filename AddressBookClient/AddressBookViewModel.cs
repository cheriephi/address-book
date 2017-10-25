using System.Collections.ObjectModel;
using System.IO;

namespace ConsoleAddress
{
    /// <summary>
    /// Communicates between the view and the model.
    /// </summary>
    class AddressBookViewModel
    {
        private AddressBook addressBook;
        private ObservableCollection<AddressViewModel> addresses;

        public AddressBookViewModel()
        {
            var addresses = new ObservableCollection<AddressViewModel>
            {
                new AddressViewModel("Joe Bloggs", "1 New St.", "Birmingham", "England", "B01 3TN", "UK"),
                new AddressViewModel("Michelle Obama", "1600 Pennsylvania Ave", "Washington", "DC", "20500", "USA")
            };

            // Update the Address Book domain class
            addressBook = new AddressBook();
            foreach (var address in addresses)
            {
                Update(address.Name, address.Street, address.City, address.State, address.Zip, address.Country);
            }

            // Update this class
            this.addresses = addresses;
        }

        private void Update(string name, string street, string city, string state, string zip, string country)
        {
            // Remove if exists
            addressBook.Remove(name);

            // Add
            var address = new Address(street, city, state, zip, country);
            addressBook.Add(name, address);
        }

        public ObservableCollection<AddressViewModel> Addresses
        {
            get { return addresses; }
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
            //TODO: This is not properly refreshing the data

            var controller = new Controller();

            addressBook = controller.Load(input);

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
            var controller = new Controller();
            //TODO: addressBook is not in sync with the data
            controller.Save(output, addressBook);
        }
        #endregion
    }
}
