using System;
using System.Data.Linq.Mapping;

namespace ConsoleAddress
{
    /// <summary>
    /// Manages contact addresses.
    /// </summary>
    [Table(Name = "Address")]
    public class Address
    {
        private string name;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string country;

        #region Constructors
        /// <summary>
        /// Creates an empty address.
        /// </summary>
        public Address() : this("", "", "", "", "", "") {}

        /// <summary>
        /// Creates an address from the data passed in.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zip"></param>
        /// <param name="country"></param>
        public Address(string name, string street, string city, string state, string zip, string country)
        {
            this.name = name;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.country = country;
        }
        #endregion

        #region Accessors
        [Column(Name = "FullName", Storage = "name")]
        public string Name { get => name; set => name = value; }

        [Column(Storage = "street")]
        public string Street { get => street; set => street = value; }

        [Column(Storage = "city")]
        public string City { get => city; set => city = value; }

        [Column(Name = "StateCode", Storage = "state")]
        public string State { get => state; set => state = value; }

        [Column(Storage = "zip")]
        public string Zip { get => zip; set => zip = value; }

        [Column(Storage = "country")]
        public string Country { get => country; set => country = value; }
        #endregion

        /// <summary>
        /// Returns a concatenated string of the address fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var addressFields = new string[] { name, street, city, state, zip, country };
            return String.Join(", ", addressFields);
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
            if (fields.Length != 6)
            {
                result = null;
                return false;
            }
            result = new Address(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5]);

            return true;
        }
    }
}
