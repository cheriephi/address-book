using ConsoleAddress;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAddressTest
{
    internal class AddressBookBuilder
    {
        private Dictionary<string, AddressBuilder> defaultAddressBuilders = new Dictionary<string, AddressBuilder>()
        {
            {"Joe Bloggs", new AddressBuilder().SetStreet("1 New St.").SetCity("Birmingham").SetState("England").SetZip("B01 3TN").SetCountry("UK") },
            {"John Doe", new AddressBuilder().SetStreet("16 S 31st St.").SetCity("Boulder").SetState("CO").SetZip("80304").SetCountry("USA") },
            {"Brent Leroy", new AddressBuilder().SetStreet("Corner Gas").SetCity("Dog River").SetState("SK").SetZip("S0G 4H0").SetCountry("CANADA") }
        };

        Dictionary<string, AddressBuilder> addressBuilders = new Dictionary<string, AddressBuilder>();

        internal AddressBookBuilder()
        {
            foreach (var builder in defaultAddressBuilders)
            {
                addressBuilders.Add(builder.Key, builder.Value);
            }
        }

        internal Dictionary<string, AddressBuilder> GetAddressBuilders()
        {
            return addressBuilders;
        }

        internal AddressBookBuilder SetAddressBuilders(Dictionary<string, AddressBuilder> addressBuilders)
        {
            this.addressBuilders = addressBuilders;
            return this;
        }

        internal AddressBook Build()
        {
            var book = new AddressBook();

            // Add any non default addresses into the address book
            foreach (var builder in addressBuilders)
            {
                if (!defaultAddressBuilders.ContainsKey(builder.Key))
                {
                    book.Add(builder.Key, builder.Value.Build());
                }
            }

            return book;
        }

        public override string ToString()
        {
            string addressListing = string.Join(Environment.NewLine, addressBuilders.Select(x => x.ToString()).ToArray());
            return addressListing;
        }
    }
}
