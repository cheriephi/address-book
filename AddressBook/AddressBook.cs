using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace ConsoleAddress
{
    /// <summary>
    /// Manages all contact addresses in the address book.
    /// </summary>
    /// <remarks>Interacts with persistent storage.
    /// Code has no special handling if the user requests an operation that doesn't make sense 
    /// (for example, inserting a record where it already exists).
    /// </remarks>
    public class AddressBook
    {
        private AddressBookContext addressBookContext;
        private PropertyInfo sortProperty;

        /// <summary>
        /// Creates the empty address book.
        /// </summary>
        public AddressBook()
        {
            var connection = ConfigurationManager.ConnectionStrings["AddressBookDataSource"].ConnectionString;
            addressBookContext = new AddressBookContext(connection);

            sortProperty = GetAddressProperty("name"); // Default sorting by the Address.Name.
        }

        /// <summary>
        /// Returns the addresses.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Address> GetAll()
        {
            var orderedAddresses = (from addess in addressBookContext.Addresses.AsEnumerable<Address>()
                                    orderby sortProperty.GetValue(addess, null)
                                    select addess).ToDictionary(address => address.Name);

            return orderedAddresses;
        }

        /// <summary>
        /// Adds the input address to the address book.
        /// </summary>
        /// <param name="addressToAdd"></param>
        public void Add(Address addressToAdd)
        {
            if (addressToAdd == null) { addressToAdd = new Address(); }

            addressBookContext.Addresses.InsertOnSubmit(addressToAdd);
        }

        /// <summary>
        /// Updates the address book entry.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addressKey"></param>
        /// <param name="addressValue"></param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void Update(string name, string addressKey, string addressValue)
        {
            if (!Enum.IsDefined(typeof(AddressKey), addressKey))
            {
                throw new ArgumentOutOfRangeException($"{addressKey} is not a valid AddressKey");
            }

            var addressToUpdate = (from address in addressBookContext.Addresses
                                   where address.Name == name
                                   select address).First();

            var addressProperty = GetAddressProperty(addressKey);
            addressProperty.SetValue(addressToUpdate, addressValue);
        }

        /// <summary>
        /// Returns the associated Address.Property for the input addressKey.
        /// </summary>
        /// <param name="addressKey"></param>
        /// <returns></returns>
        private PropertyInfo GetAddressProperty(string addressKey)
        {
            // Update the relevant Address property using reflection
            var capitalizedAddressKey = char.ToUpper(addressKey[0]) + addressKey.Substring(1);
            var property = typeof(Address).GetProperty(capitalizedAddressKey);
            return property;
        }

        /// <summary>
        /// Removes the input address from the book if it exists.
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            var addressToDelete = (from address in addressBookContext.Addresses
                                   where address.Name == name
                                   select address).First();

            addressBookContext.Addresses.DeleteOnSubmit(addressToDelete);
        }

        /// <summary>
        /// Saves the address book to persistent storage.
        /// </summary>
        public void Save()
        {
            addressBookContext.SubmitChanges();
        }

        /// <summary>
        /// Returns matching address book entries based on the key to search.
        /// </summary>
        /// <param name="addressKey"></param>
        /// <param name="addressValue"></param>
        /// <returns></returns>
        public Dictionary<string, Address> Find(string addressKey, string addressValue)
        {
            var addressProperty = GetAddressProperty(addressKey);
            var matchingAddresses = addressBookContext.Addresses.AsEnumerable<Address>().Where(address => addressProperty.GetValue(address, null).ToString().Contains(addressValue));

            // Return a dictionary.
            return matchingAddresses.ToDictionary(address => address.Name);
        }

        /// <summary>
        /// Sorts the address book by the input key.
        /// </summary>
        /// <param name="addressKey"></param>
        public void Sort(string addressKey)
        {
            sortProperty = GetAddressProperty(addressKey);
        }
    }
}
