using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleAddress;

namespace ConsoleAddressTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void ListTest()
        { 
            var args = new string[] { "list" };
            string output;

            var program = new PrivateType(typeof(Program));
            var success = Program.addressController(args, out output);

            var builder = new AddressBookBuilder();

            Assert.IsTrue(success);
            Assert.AreEqual(builder.ToString(), output);
        }

        [TestMethod]
        public void AddTest()
        {
            var args = new String[] { "list" };
            string output;
            var success = Program.addressController(args, out output);

            var builder = new AddressBookBuilder();

            Assert.AreEqual(builder.ToString(), output, "Before");


            var addressBuilder = new AddressBuilder().SetName("Michelle Obama").SetStreet("1600 Pennsylvania Ave").SetCity("Washington").SetState("DC").SetZip("20500").SetCountry("USA");
            var address = addressBuilder.Build();
            var addressBuilders = builder.GetAddressBuilders();
            addressBuilders.Add(addressBuilder);

            args = new String[] { "add", address.ToString() };
            success = Program.addressController(args, out output);

            Assert.IsTrue(success);
            Assert.AreEqual(builder.ToString(), output);
        }

        /// <summary>
        /// TODO: Test should not assume default address exists. It fails when executing as a suite.
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            var builder = new AddressBookBuilder();
            var addressBuilders = builder.GetAddressBuilders();
            var oldAddress = addressBuilders[0].Build();
            var newAddressBuilder = new AddressBuilder().SetName("Foo");
            var newAddress = newAddressBuilder.Build();

            addressBuilders.Remove(addressBuilders[0]);
            addressBuilders.Add(newAddressBuilder);

            var args = new String[] { "update", oldAddress.ToString(), newAddress.ToString() };

            string output;
            var success = Program.addressController(args, out output);

            Assert.IsTrue(success);
            Assert.AreEqual(builder.ToString(), output);
        }

        /// <summary>
        /// TODO: Test should not assume default address exists. It fails when executing as a suite.
        /// </summary>
        [TestMethod]
        public void RemoveTest()
        {
            var builder = new AddressBookBuilder();
            var addressBuilders = builder.GetAddressBuilders();
            var address = addressBuilders[0].Build();

            addressBuilders.Remove(addressBuilders[0]);

            var args = new String[] { "remove", address.ToString() };

            string output;
            var success = Program.addressController(args, out output);

            Assert.IsTrue(success);
            Assert.AreEqual(builder.ToString(), output);
        }

        /// <summary>
        /// Tests we output a help message if no args are passed in.
        /// </summary>
        /// <remarks>The help should start with Usage.</remarks>
        [TestMethod]
        public void NoArgsTest()
        {
            var args = new String[] { };
            string output;
            var success = Program.addressController(args, out output);
            Assert.IsFalse(success);
            Assert.IsTrue(output.StartsWith("Usage"));
        }
    }
}
