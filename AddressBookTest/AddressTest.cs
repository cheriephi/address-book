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

            var street = "123 Main";
            builder.SetStreet(street);
            address.Street = street;

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetCityTest()
        {
            var builder = new AddressBuilder().SetCity("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            var city = "Post Falls";
            builder.SetCity(city);
            address.City = city;

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetStateTest()
        {
            var builder = new AddressBuilder().SetState("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            var state = "WA";
            builder.SetState(state);
            address.State = state;

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetZipTest()
        {
            var builder = new AddressBuilder().SetZip("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            var zip = "12345";
            builder.SetZip(zip);
            address.Zip = zip;

            Helper.AssertAreEqual(builder, address, "After");
        }

        [TestMethod]
        public void SetCountryTest()
        {
            var builder = new AddressBuilder().SetCountry("Before");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address, "Before");

            var country = "Mexico";
            builder.SetCountry(country);
            address.Country = country;

            Helper.AssertAreEqual(builder, address, "After");
        }
        #endregion

        [TestMethod]
        public void ToStringTest()
        {
            var builder = new AddressBuilder().SetName("Joe Schmoe").SetStreet("Five State Street").SetCity("Seattle").SetState("WA").SetZip("98117").SetCountry("USA");
            var address = builder.Build();

            Assert.AreEqual(builder.ToString(), address.ToString());
        }

        //// TODO: Test TryParse
    }
}
