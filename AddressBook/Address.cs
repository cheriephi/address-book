using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Manages contact addresses.
    /// </summary>
    [Serializable]
    public class Address
    {
        Dictionary<string, string> address;

        #region Constructors
        /// <summary>
        /// Creates an empty address.
        /// </summary>
        public Address() : this("", "", "", "", "") {}

        /// <summary>
        /// Creates an address from the data passed in.
        /// </summary>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="country"></param>
        public Address(string street, string city, string state, string zip, string country)
        {
            address = new Dictionary<string, string>();

            address.Add("street", street);
            address.Add("city", city);
            address.Add("state", state);
            address.Add("zip", zip);
            address.Add("country", country);
        }
        #endregion

        /// <summary>
        /// Returns corresponding address value for the key passed in.
        /// </summary>
        /// <param name="addressKey"></param>
        /// <returns></returns>
        /// <see cref="AddressKey">The name key is not relevant.</see>
        public string getSpec(string addressKey)
        {
            return address[addressKey];
        }

        /// <summary>
        /// Updates corresponding address value for the key passed in.
        /// </summary>
        /// <param name="addressKey"></param>
        /// <param name="addressValue"></param>
        /// <see cref="AddressKey">The name key is not relevant.</see>
        /// <exception cref="ArgumentOutOfRangeException">If addressKey is not a Address.AddressKey.</exception>
        public void setSpec(string addressKey, string addressValue)
        {
            // Throw an error if an invalid address key is passed in
            if (!Enum.IsDefined(typeof(AddressKey), addressKey) || addressKey == "name")
            {
                throw new ArgumentOutOfRangeException();
            }

            address[addressKey] = addressValue;
        }

        /// <summary>
        /// Returns a concatenated string of the address fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Join(", ", address.Select(x => x.Value).ToArray());
        }

        /// <summary>
        /// Converts the string representation of an Address to its Address equivalent.
        /// </summary>
        /// <param name="addressFields"></param>
        /// <param name="result"></param>
        /// <returns>Whether the conversion succeeded.</returns>
        /// <remarks>Patterned after Int32.TryParse.</remarks>
        public static bool TryParse(string addressFields, out Address result)
        {
            var delimiter = new string[] { ", " };
            var fields = addressFields.Split(delimiter, StringSplitOptions.None);
            if (fields.Length != 5)
            {
                result = null;
                return false;
            }
            result = new Address(fields[0], fields[1], fields[2], fields[3], fields[4]);

            return true;
        }
    }
}
