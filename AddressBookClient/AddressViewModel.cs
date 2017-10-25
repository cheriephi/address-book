using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Wraps the domain address book classes into data that can easily bind to a view.
    /// </summary>
    public class AddressViewModel
    {
        private static AddressBook addressBook;
        private KeyValuePair<string, Address> addressItem;

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>Datagrid Add binding requires a default constructor.
        /// The guid is a workaround to make sure we stay unique. There may be a more elegant solution.
        /// </remarks>
        public AddressViewModel() : this(Guid.NewGuid().ToString("N"), "", "", "", "", "") {}

        public AddressViewModel(string name, string street, string city, string state, string zip, string country)
        {
            var address = new Address(street, city, state, zip, country);
                addressBook.Add(name, address);

            var addresses = addressBook.GetAll();
            addressItem = addresses.FirstOrDefault(pair => pair.Key == name);
        }

        public AddressViewModel(KeyValuePair<string, Address> addressItem)
        {
            this.addressItem = addressItem;
        }
        #endregion

        public static AddressBook AddressItemBook { get => addressBook; set => addressBook = value; }

        #region Accessors
        public string Name
        {
            get => addressItem.Key;
            set
            { 
                addressBook.Update(addressItem.Key, "name", value);

                var addresses = addressBook.GetAll();
                addressItem = addresses.FirstOrDefault(pair => pair.Key == value);
            }
        }

        public string Street
        {
            get => addressItem.Value.getSpec("street");
            set => addressBook.Update(addressItem.Key, "street", value);
        }

        public string City
        {
            get => addressItem.Value.getSpec("city");
            set => addressBook.Update(addressItem.Key, "city", value);
        }

        public string State
        {
            get => addressItem.Value.getSpec("state");
            set => addressBook.Update(addressItem.Key, "state", value);
        }

        public string Zip
        {
            get => addressItem.Value.getSpec("zip");
            set => addressBook.Update(addressItem.Key, "zip", value);
        }

        public string Country
        {
            get => addressItem.Value.getSpec("country");
            set => addressBook.Update(addressItem.Key, "country", value);
        }
        #endregion
    }
}
