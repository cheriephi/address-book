using System.Data.Linq;

namespace ConsoleAddress
{
    public class AddressBookContext : DataContext
    {
        public Table<Address> Addresses;

        public AddressBookContext(string connection) : base(connection) {}
    }
}
