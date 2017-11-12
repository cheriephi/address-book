using System;
using System.Collections.Generic;

namespace ConsoleAddress
{
    /// <summary>
    /// Manages all contact addresses in the address book.
    /// </summary>
    [Serializable]
    public class AddressBook
    {
        private Dictionary<string, Address> addresses;

        /// <summary>
        /// Creates the empty address book.
        /// </summary>
        public AddressBook()
        {
            addresses = new Dictionary<string, Address>() {};
        }

        /// <summary>
        /// Returns the addresses.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Address> GetAll()
        {
            return new Dictionary<string, Address>(addresses);
        }

        /// <summary>
        /// Adds the input address to the address book.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addressToAdd"></param>
        public void Add(string name, Address addressToAdd)
        {
            if (addressToAdd == null) { addressToAdd = new Address(); }

            addresses.Add(name, addressToAdd);
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

            if (addressKey == "name")
            {
                if (addresses.ContainsKey(name))
                {
                    var address = addresses[name];

                    Remove(name);
                    Add(addressValue, address);
                }
                else
                {
                    Add(addressValue, new Address());
                }
            }
                
            else
            {
                // Update the relevant Address property using reflection
                var capitalizedAddressKey = char.ToUpper(addressKey[0]) + addressKey.Substring(1);
                var property = typeof(Address).GetProperty(capitalizedAddressKey);
                property.SetValue(addresses[name], addressValue);
            }
        }

        /// <summary>
        /// Removes the input address from the book if it exists.
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            addresses.Remove(name);
        }
    }
}
