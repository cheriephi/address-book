using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleAddress;

namespace ConsoleAddressTest
{
    /// <summary>
    /// Tests the AddressBook class.
    /// </summary>
    /// <remarks>GetAll is implicitly tested via the constructor test.</remarks>
    [TestClass]
    public class AddressBookTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook);
        }

        #region Add tests
        [TestMethod]
        public void AddAddressTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            var addressBuilders = builder.GetAddressBuilders();

            var address = new AddressBuilder().SetName("New address name");
            addressBuilders.Add(address);
            addressBook.Add(address.Build());

            Helper.AssertAreEqual(builder, addressBook, "After");
        }

        [TestMethod]
        public void AddExistingAddressTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            var addressBuilders = builder.GetAddressBuilders();

            var address = new AddressBuilder().SetName("New address name");
            addressBuilders.Add(address);
            addressBook.Add(address.Build());

            Helper.AssertAreEqual(builder, addressBook, "Before");

            addressBook.Add(address.Build());

            Helper.AssertAreEqual(builder, addressBook, "After");
        }

        /// <summary>
        /// We shouldn't do anything if someone tries to add a null address to the address book.
        /// </summary>
        /// <remarks>The example code doesn't handle this; it adds a null item to the address book.</remarks>
        [TestMethod]
        public void AddNullAddressTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            var addressBuilders = builder.GetAddressBuilders();

            var address = new AddressBuilder().SetName("New address name");
            addressBook.Add(null);

            Helper.AssertAreEqual(builder, addressBook, "After");
        }
        #endregion

        #region Update tests
        /// <summary>
        /// Tests we can update an item.
        /// </summary>
        /// <remarks>Modify anything, it doesn't matter; we are testing field by field elsewhere.
        /// TODO: Consider testing other update sceanarios.
        /// </remarks>
        [TestMethod]
        public void UpdateTest()
        {
            var builder = new AddressBookBuilder();
            var addressBuilders = builder.GetAddressBuilders();
            var oldAddress = new AddressBuilder();
            addressBuilders.Add(oldAddress);
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            var newAddress = new AddressBuilder().SetName("New Name");
            addressBuilders.Remove(oldAddress);
            addressBuilders.Add(newAddress);

            addressBook.Update(oldAddress.Build(), newAddress.Build());

            Helper.AssertAreEqual(builder, addressBook, "After");
        }
        #endregion

        #region Remove tests
        [TestMethod]
        public void RemoveTest()
        {
            var builder = new AddressBookBuilder();
            var addressBuilders = builder.GetAddressBuilders();
            var addressBuilder = new AddressBuilder().SetName("Address we will remove");
            addressBuilders.Add(addressBuilder);
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            addressBuilders.Remove(addressBuilder);
            addressBook.Remove(addressBuilder.Build());

            Helper.AssertAreEqual(builder, addressBook, "After");
        }

        /// <summary>
        /// Tests nothing breaks or changes if we try to remove a null address.
        /// </summary>
        [TestMethod]
        public void RemoveNullTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            addressBook.Remove(null);

            Helper.AssertAreEqual(builder, addressBook, "After");
        }

        /// <summary>
        /// Tests nothing changes if we try to  remove an address that doesn't exist.
        /// </summary>
        [TestMethod]
        public void RemoveDoesNotExistTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            var address = new AddressBuilder().SetName("Does not exist").Build();
            addressBook.Remove(address);

            Helper.AssertAreEqual(builder, addressBook, "After");
        }
        #endregion
    }
}
