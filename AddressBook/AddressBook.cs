using System;
using System.Linq;

namespace ConsoleAddress
{
    public class AddressBook
    {
        private Address[] addresses;
        public int CountOfAddresses { get => addresses.Length; }

        /// <summary>
        /// Creates the address book with default entries.
        /// </summary>
        public AddressBook()
        {
            addresses = new Address[]
            {
                new Address("Joe Bloggs", "1 New St.", "Birmingham", "England", "B01 3TN", "UK"),
                new Address("John Doe", "16 S 31st St.", "Boulder", "CO", "80304", "USA")
            };
        }

        /// <summary>
        /// Finds the input address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>The address index, else -1 if not found.</returns>
        private int findIndex(Address address)
        {
            for (int index = 0; index < addresses.Length; ++index)
            {
                if (addresses[index].Equals(address))
                { 
                    return index;
                }
            }
            return -1;  // not found, returning an "impossible?" index
        }

        /// <summary>
        /// Returns the addresses.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Someone could change the addresses array outside of this AddressBook class.</remarks>
        public Address[] GetAll()
        {
            return addresses;
        }

        /// <summary>
        /// Adds the input address to the address book.
        /// </summary>
        /// <param name="addressToAdd"></param>
        public void Add(Address addressToAdd)
        {
            if (addressToAdd == null) { return; }

            var index = findIndex(addressToAdd);
            if (!(index >= 0 && index < addresses.Length))
            {
                // create a new larger array
                var revisedAddresses = new Address[addresses.Length + 1];
                // copy existing addresses into the new array
                for (int i = 0; i < addresses.Length; i++)
                {
                    revisedAddresses[i] = addresses[i];
                }
                // copy the new address into the array
                revisedAddresses[addresses.Length] = addressToAdd;
                // replace the old array with the new array
                addresses = revisedAddresses;
            }
        }

        /// <summary>
        /// Adds the new address, replacing the old one if it exists.
        /// Updates the reference identity if the old and new addresses have the same values, but different identities.
        /// </summary>
        /// <param name="oldAddress"></param>
        /// <param name="newAddress"></param>
        public void Update(Address oldAddress, Address newAddress)
        {
            Remove(oldAddress);
            Add(newAddress);
        }

        /// <summary>
        /// Removes the input address from the book if it exists.
        /// </summary>
        /// <param name="addressToRemove"></param>
        public void Remove(Address addressToRemove)
        {
            var index = findIndex(addressToRemove);
            if (index >= 0)
            {
                // create a new smaller array
                var revisedAddresses = new Address[addresses.Length - 1];
                var revisedIndex = 0;
                // copy existing addresses into the new array
                for (int i = 0; i < addresses.Length; i++)
                {
                    if (i != index)
                    {
                        revisedAddresses[revisedIndex] = addresses[i];
                        revisedIndex++;
                    }
                }
                // replace the old array with the new array
                addresses = revisedAddresses;
            }
        }

        /// <summary>
        /// Returns a string with one line per address.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Takes the addresses array, gets the ToString() for each of those addresses into its
            // own string array, then combines them with a new line per string.
            var addressList = string.Join(Environment.NewLine, addresses.Select(x => x.ToString()).ToArray());
            return addressList;
        }
    }
}
