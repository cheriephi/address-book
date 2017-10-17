using ConsoleAddress;

namespace ConsoleAddressTest
{
    internal class AddressBuilder
    {
        string street = "";
        string city = "";
        string state = "";
        string zip = "";
        string country = "";

        internal string GetStreet()
        {
            return street;
        }

        internal AddressBuilder SetStreet(string street)
        {
            this.street = street;
            return this;
        }

        internal string GetCity()
        {
            return city;
        }

        internal AddressBuilder SetCity(string city)
        {
            this.city = city;
            return this;
        }

        internal string GetState()
        {
            return state;
        }

        internal AddressBuilder SetState(string state)
        {
            this.state = state;
            return this;
        }

        internal string GetZip()
        {
            return zip;
        }

        internal AddressBuilder SetZip(string zip)
        {
            this.zip = zip;
            return this;
        }

        internal string GetCountry()
        {
            return country;
        }

        internal AddressBuilder SetCountry(string country)
        {
            this.country = country;
            return this;
        }

        internal Address Build()
        {
            var address = new Address(street, city, state, zip, country);
            return address;
        }

        public override string ToString()
        {
            var address = $"{street}, {city}, {state}, {zip}, {country}";
            return address;
        }
    }
}
