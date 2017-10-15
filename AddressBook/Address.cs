using System;

namespace ConsoleAddress
{
    public class Address
    {
        private string name;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string country;

        public Address(string name, string street, string city, string state, string zip, string country)
        {
            this.name = name;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.country = country;
        }

        public string Name { get => name; }
        public string Street { get => street; }
        public string City { get => city; }
        public string State { get => state; }
        public string Zip { get => zip; }
        public string Country { get => country; }

        /// <summary>
        /// Compares each field (case insensitive, culture insensitive, null == null).
        /// All fields must match.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Address other)
        {
            if (other == null)
            {  // This is required
                return false;
            }

            return this.name.Equals(other.Name) &&
                this.street.Equals(other.Street) &&
                this.city.Equals(other.City) &&
                this.state.Equals(other.State) &&
                this.zip.Equals(other.Zip) &&
                this.country.Equals(other.Country);
        }

        /// <summary>
        /// Returns a concatenated string of the address fields.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var addressFields = new string[] { name, street, city, state, zip, country};
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
            var delimiter = new string[] { ", "};
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
