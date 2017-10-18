using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleAddress;
using System.Collections.Generic;

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
            var addressBookBuilder = new AddressBookBuilder();
            var addressBook = addressBookBuilder.Build();

            Helper.AssertAreEqual(addressBookBuilder, addressBook);
        }

        #region Add tests
        [TestMethod]
        public void AddAddressTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBook = addressBookBuilder.Build();

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            var addressBuilders = addressBookBuilder.GetAddressBuilders();

            var addressBuilder = new AddressBuilder();
            var name = "New address name";
            addressBuilders.Add(name, addressBuilder);
            addressBook.Add(name, addressBuilder.Build());

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "After");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddExistingAddressTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBook = addressBookBuilder.Build();

            var addressBuilders = addressBookBuilder.GetAddressBuilders();

            var addressBuilder = new AddressBuilder();
            var name = "New address name";
            addressBuilders.Add(name, addressBuilder);
            addressBook.Add(name, addressBuilder.Build());

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            addressBook.Add(name, addressBuilder.Build());

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "After");
        }

        /// <summary>
        /// Tests that an empty address gets added to the book when null is passed in.
        /// </summary>
        [TestMethod]
        public void AddNullAddressTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBook = addressBookBuilder.Build();

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            var addressBuilders = addressBookBuilder.GetAddressBuilders();

            var addressBuilder = new AddressBuilder();
            var name = "New address name";
            addressBuilders.Add(name, addressBuilder);
            addressBook.Add(name, null);

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "After");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullNameAddressTest()
        {
            var addressBook = new AddressBookBuilder().Build();

            var address = new AddressBuilder().Build();
            addressBook.Add(null, address);
        }
        #endregion

        #region Update tests
        /// <summary>
        /// Tests we can update an item.
        /// </summary>
        /// <remarks>Modify anything, it doesn't matter; we are testing field by field elsewhere.
        /// </remarks>
        [TestMethod]
        public void UpdateTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBuilders = addressBookBuilder.GetAddressBuilders();
            var addressBuilder = new AddressBuilder();
            var name = "Jake";
            addressBuilders.Add(name, addressBuilder);
            var addressBook = addressBookBuilder.Build();

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            var street = "One Haight Street";
            var newAddress = new AddressBuilder().SetStreet(street);

            addressBuilders.Remove(name);
            addressBuilders.Add(name, newAddress);

            addressBook.Update(name, "street", street);

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "After");
        }

        [TestMethod]
        public void UpdateNameTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBuilders = addressBookBuilder.GetAddressBuilders();
            var addressBuilder = new AddressBuilder().SetCountry("Ireland");
            var oldName = "Paul Hewson";
            addressBuilders.Add(oldName, addressBuilder);
            var addressBook = addressBookBuilder.Build();

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            var newName = "Bono";

            addressBuilders.Remove(oldName);
            addressBuilders.Add(newName, addressBuilder);
            addressBookBuilder.SetAddressBuilders(addressBuilders);

            addressBook.Update(oldName, "name", newName);

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "After");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateKeyDoesNotExistTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBook = addressBookBuilder.Build();

            var addressBuilders = addressBookBuilder.GetAddressBuilders();

            var addressBuilder = new AddressBuilder();
            var name = "Sally";
            addressBuilders.Add(name, addressBuilder);
            addressBook.Add(name, addressBuilder.Build());

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            addressBook.Update(name, "foo", "bar");
        }

        /// <summary>
        /// Tests a new empty address book entry added if none exists for update.
        /// </summary>
        [TestMethod]
        public void UpdateNameDoesNotExistTest()
        {
            var addressBookBuilder = new AddressBookBuilder();
            var addressBook = addressBookBuilder.Build();

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "Before");

            var addressBuilders = addressBookBuilder.GetAddressBuilders();

            var addressBuilder = new AddressBuilder();
            var name = "New address name";
            addressBuilders.Add(name, addressBuilder);

            addressBook.Update(name, "name", name);

            Helper.AssertAreEqual(addressBookBuilder, addressBook, "After");
        }
        #endregion

        #region Remove tests
        [TestMethod]
        public void RemoveTest()
        {
            var builder = new AddressBookBuilder();
            var addressBuilders = builder.GetAddressBuilders();
            var name = "Address we will remove";
            var addressBuilder = new AddressBuilder();
            addressBuilders.Add(name, addressBuilder);
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            addressBuilders.Remove(name);
            addressBook.Remove(name);

            Helper.AssertAreEqual(builder, addressBook, "After");
        }

        /// <summary>
        /// Tests nothing changes if we try to remove an address that doesn't exist.
        /// </summary>
        [TestMethod]
        public void RemoveDoesNotExistTest()
        {
            var builder = new AddressBookBuilder();
            var addressBook = builder.Build();

            Helper.AssertAreEqual(builder, addressBook, "Before");

            addressBook.Remove("Does not exist");

            Helper.AssertAreEqual(builder, addressBook, "After");
        }
        #endregion

        // TODO: Test GetAll doesn't break encapsulation. Can data be changed outside method under test?
    }
}
