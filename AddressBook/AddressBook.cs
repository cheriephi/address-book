using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAddress
{
    public class AddressBook
    {
        private Dictionary<string, Address> addresses;

        /// <summary>
        /// Creates the address book with default entries.
        /// </summary>
        public AddressBook()
        {
            addresses = new Dictionary<string, Address>()
            {
                {"Joe Bloggs", new Address("1 New St.", "Birmingham", "England", "B01 3TN", "UK") },
                {"John Doe", new Address("16 S 31st St.", "Boulder", "CO", "80304", "USA")},
                {"Brent Leroy", new Address("Corner Gas", "Dog River", "SK", "S0G 4H0", "CANADA")}
            };
        }

        /// <summary>
        /// Returns the addresses.
        /// </summary>
        /// <returns></returns
        public Dictionary<string, Address> GetAll()
        {
            return new Dictionary<string, Address>(addresses);
        }

        /// <summary>
        /// Returns matching address book entries based on the key to search.
        /// </summary>
        /// <param name="addressKey"></param>
        /// <param name="addressValue"></param>
        /// <returns></returns>
        public Dictionary<string, Address> Find(string addressKey, string addressValue)
        {
            IEnumerable<KeyValuePair<string, Address>> matchingAddresses;

            if (addressKey == "name")
            {
                // Find matching addresses by using a Where predicate lambda expression
                matchingAddresses = addresses.Where(pair => pair.Key.Contains(addressValue));
            }
            else
            {
                matchingAddresses = addresses.Where(pair => pair.Value.getSpec(addressKey).Contains(addressValue));
            }

            // Return a dictionary; convert it from the KeyValuePair we have.
            return matchingAddresses.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Adds the input address to the address book.
        /// </summary>
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
        public void Update(string name, string addressKey, string addressValue)
        {
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
                addresses[name].setSpec(addressKey, addressValue);
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

        /// <summary>
        /// Sorts the address book by the input key.
        /// </summary>
        /// <param name="addressKey"></param>
        public void Sort(string addressKey)
        {
            IOrderedEnumerable<KeyValuePair<string, Address>> sortedAddresses;

            if (addressKey == "name")
            {
                // Find matching addresses by using a Where predicate lambda expression
                sortedAddresses = addresses.OrderBy(pair => pair.Key);
            }
            else
            {
                sortedAddresses = addresses.OrderBy(pair => pair.Value.getSpec(addressKey));
            }

            // Return a dictionary; convert it from the KeyValuePair we have.
            addresses = sortedAddresses.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Returns a string with one line per address.
        /// </summary>
        /// <param name="addressDictionary"></param>
        /// <returns></returns>
        public static string ToString(Dictionary<string, Address> addressDictionary)
        {
            // Build a string of addresses: the name and the associated address.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, Address>, string> addressBookItemToString = pair => pair.Key + ", " + pair.Value.ToString();

            var addressList = string.Join(Environment.NewLine, addressDictionary.Select(addressBookItemToString).ToArray());

            return addressList;
        }
    }
}
