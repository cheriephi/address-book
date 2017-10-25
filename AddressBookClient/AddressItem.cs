using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Wraps the domain address book classes into data that can easily bind to a view.
    /// </summary>
    public class AddressItem
    {
        private static AddressBook addressBook;
        private KeyValuePair<string, Address> addressItem;

        #region Constructors
        public AddressItem() : this("", "", "", "", "", "") {}

        public AddressItem(string name, string street, string city, string state, string zip, string country)
        {
            var address = new Address(street, city, state, zip, country);
            addressBook.Add(name, address);

            var addresses = addressBook.GetAll();
            addressItem = addresses.FirstOrDefault(pair => pair.Key == name);
        }

        public AddressItem(KeyValuePair<string, Address> addressItem)
        {
            this.addressItem = addressItem;
        }
        #endregion


        public static AddressBook AddressItemBook { get => addressBook; set => addressBook = value; }

        public string Name { get => addressItem.Key; set => throw new NotImplementedException(); }
        public string Street { get => addressItem.Value.getSpec("street"); set => addressBook.Update(addressItem.Key, "street", value); }            
        public string City { get => addressItem.Value.getSpec("city"); set => addressBook.Update(addressItem.Key, "city", value); }
        public string State { get => addressItem.Value.getSpec("state"); set => addressBook.Update(addressItem.Key, "state", value); }
        public string Zip { get => addressItem.Value.getSpec("zip"); set => addressBook.Update(addressItem.Key, "zip", value); }
        public string Country { get => addressItem.Value.getSpec("country"); set => addressBook.Update(addressItem.Key, "country", value); }
    }
}
