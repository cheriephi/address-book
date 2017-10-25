using System.Collections.ObjectModel;

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
            AddressBook.Remove(name);

            // Add
            var address = new Address(street, city, state, zip, country);
            AddressBook.Add(name, address);
        }

        public ObservableCollection<AddressViewModel> Addresses
        {
            get { return addresses; }
            set
            {
                addresses = value;
            }
        }

        public AddressBook AddressBook // TODO: Shouldn't name it like its type
        {
            get => addressBook;
            set
            {
                addressBook = value;

                // Refresh the addresses in this class
                var addressBookItems = addressBook.GetAll();
                addresses.Clear();
                foreach (var address in addressBookItems)
                {
                    var addressViewModel = new AddressViewModel(address.Key, address.Value.getSpec("street"), address.Value.getSpec("city"), address.Value.getSpec("state"), address.Value.getSpec("zip"), address.Value.getSpec("country"));
                    addresses.Add(addressViewModel);
                }
            }
        }
    }
}
