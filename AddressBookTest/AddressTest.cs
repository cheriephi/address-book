using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConsoleAddressTest
{
    /// <summary>
    /// Tests the Address class.
    /// </summary>
    [TestClass]
    public class AddressTest
    {
        #region Constructor tests
        [TestMethod]
        public void DefaultConstructorTest()
        {
            var builder = new AddressBuilder();
            var address = new Address();

            Helper.AssertAreEqual(builder, address);
        }

        /// <summary>
        /// Test the constructor through its public get accessors.
        /// </summary>
        [TestMethod]
        public void ConstructorTest()
        {
            var builder = new AddressBuilder().SetStreet("One Main Street").SetCity("Seattle").SetState("WA").SetZip("98117").SetCountry("USA");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address);
        }
        #endregion

        #region Address part set tests
        [TestMethod]
        public void SetStreetTest()
        {
            var builder = new AddressBuilder().SetStreet("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            builder.SetStreet("After");
            address.setSpec("street", "After");

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetCityTest()
        {
            var builder = new AddressBuilder().SetCity("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            builder.SetCity("After");
            address.setSpec("city", "After");

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetStateTest()
        {
            var builder = new AddressBuilder().SetState("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            builder.SetState("After");
            address.setSpec("state", "After");

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetZipTest()
        {
            var builder = new AddressBuilder().SetZip("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            builder.SetZip("After");
            address.setSpec("zip", "After");

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetCountryTest()
        {
            var builder = new AddressBuilder().SetCountry("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            builder.SetCountry("After");
            address.setSpec("country", "After");

            Helper.AssertAreEqual(builder, address, "After");
        }
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetInvalidAddressTypeTest()
        {
            var builder = new AddressBuilder();
            var address = builder.Build();
            address.setSpec("foo", "bar");
        }

        [TestMethod]
        public void ToStringTest()
        {
            var builder = new AddressBuilder().SetStreet("Five State Street").SetCity("Seattle").SetState("WA").SetZip("98117").SetCountry("USA");
            var address = builder.Build();

            Assert.AreEqual(builder.ToString(), address.ToString());
        }

        //// TODO: Test TryParse
    }
}
