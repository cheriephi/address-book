using ConsoleAddress;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AddressBookWeb
{
    /// <summary>
    /// Data access for address book.
    /// </summary>
    /// <see href="http://www.vinull.com/2007/04/16/asp-net-gridview-and-objectdatasource-with-custom-objects/"/>
    [DataObject(true)]
    public class AddressDataAccess
    {
        private AddressBook addressBook;

        public AddressDataAccess()
        {
            this.addressBook = new AddressBook();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Address> GetAll()
        {
            var addresses = from addess in addressBook.GetAll()
                            select addess.Value;

            return addresses.ToList();
        }

        
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(Address address)
        {
            addressBook.Add(address);

            addressBook.Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(Address address)
        {
            addressBook.Remove(address.Name);
            addressBook.Add(address);

            addressBook.Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(Address address)
        {
            addressBook.Remove(address.Name);

            addressBook.Save();
        }
    }
}