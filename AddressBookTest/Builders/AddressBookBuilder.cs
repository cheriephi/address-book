using ConsoleAddress;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAddressTest
{
    internal class AddressBookBuilder
    {
        List<AddressBuilder> addressBuilders = new List<AddressBuilder>
        {
            new AddressBuilder().SetName("Joe Bloggs").SetStreet("1 New St.").SetCity("Birmingham").SetState("England").SetZip("B01 3TN").SetCountry("UK"),
            new AddressBuilder().SetName("John Doe").SetStreet("16 S 31st St.").SetCity("Boulder").SetState("CO").SetZip("80304").SetCountry("USA")
        };

        internal List<AddressBuilder> GetAddressBuilders()
        {
            return addressBuilders;
        }

        internal AddressBookBuilder SetAddressBuilders(List<AddressBuilder> addressBuilders)
        {
            this.addressBuilders = addressBuilders;
            return this;
        }

        internal AddressBook Build()
        {
            var book = new AddressBook();

            // Somehow get the addresses in the addressBuilders into the address book
            foreach (var builder in addressBuilders)
            {
                book.Add(builder.Build());
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
