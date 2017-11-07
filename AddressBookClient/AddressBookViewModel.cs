namespace AddressBookClient
{
    /// <summary>
    /// Communicates between the view and the model.
    /// </summary>
    class AddressBookViewModel
    {
        private AddressBookDataSet addresses;
        private AddressBookDataSetTableAdapters.AddressTableAdapter addressBookTableAdapter;

        public AddressBookViewModel()
        {
            addresses = new AddressBookDataSet();
            addressBookTableAdapter = new AddressBookDataSetTableAdapters.AddressTableAdapter();
            addressBookTableAdapter.Fill(addresses.Address);
        }

        public AddressBookDataSet Addresses
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

        /// <summary>
        /// Saves the address book data to the data store.
        /// </summary>
        public void Save()
        {
            addressBookTableAdapter.Update(addresses.Address);
        }
    }
}
