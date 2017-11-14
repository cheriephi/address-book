using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Manages all contact addresses in the address book.
    /// </summary>
    /// <remarks>Interacts with persistent storage.
    /// Code has no special handling if the user requests an operation that doesn't make sense 
    /// (for example, inserting a record where it already exists).
    /// </remarks>
    [Serializable]
    public class AddressBook
    {
        private AddressBookContext addressBookContext;

        /// <summary>
        /// Creates the empty address book.
        /// </summary>
        public AddressBook()
        {
            var connection = ConfigurationManager.ConnectionStrings["AddressBookDataSource"].ConnectionString;
            addressBookContext = new AddressBookContext(connection);
        }

        /// <summary>
        /// Returns the addresses.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Address> GetAll()
        {
            var addresses = addressBookContext.Addresses.ToDictionary(address => address.Name);
            return addresses;
        }

        /// <summary>
        /// Adds the input address to the address book.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addressToAdd"></param>
        public void Add(string name, Address addressToAdd)
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
                
            // Update the relevant Address property using reflection
            var capitalizedAddressKey = char.ToUpper(addressKey[0]) + addressKey.Substring(1);
            var property = typeof(Address).GetProperty(capitalizedAddressKey);

            property.SetValue(addressToUpdate, addressValue);
        }

        /// <summary>
        /// Removes the input address from the book if it exists.
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            var addressToDelete = (from address in addressBookContext.Addresses
                                   select address).First();

            addressBookContext.Addresses.DeleteOnSubmit(addressToDelete);
        }
    }
}
