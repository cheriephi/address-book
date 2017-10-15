using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleAddressTest
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var builder = new AddressBuilder().SetName("Cherie Warren").SetStreet("7721 11th Ave NW").SetCity("Seattle").SetState("WA").SetZip("98117").SetCountry("USA");
            var address = builder.Build();

            Helper.AssertAreEqual(builder, address);
        }

        #region Equals tests
        [TestMethod]
        public void EqualsNullTest()
        {
            var builder = new AddressBuilder();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(null));
            Assert.IsTrue(address.Equals(expectedEqual));
        }

        [TestMethod]
        public void EqualsNameTest()
        {
            var builder = new AddressBuilder().SetName("a");

            var expectedNotEqual = new AddressBuilder().SetName("A").Build();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(expectedNotEqual));
            Assert.IsTrue(address.Equals(expectedEqual));
        }

        [TestMethod]
        public void EqualsStreetTest()
        {
            var builder = new AddressBuilder().SetStreet("a");

            var expectedNotEqual = new AddressBuilder().SetStreet("A").Build();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(expectedNotEqual));
            Assert.IsTrue(address.Equals(expectedEqual));
        }

        [TestMethod]
        public void EqualsCityTest()
        {
            var builder = new AddressBuilder().SetCity("a");

            var expectedNotEqual = new AddressBuilder().SetCity("A").Build();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(expectedNotEqual));
            Assert.IsTrue(address.Equals(expectedEqual));
        }

        [TestMethod]
        public void EqualsStateTest()
        {
            var builder = new AddressBuilder().SetState("a");

            var expectedNotEqual = new AddressBuilder().SetState("A").Build();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(expectedNotEqual));
            Assert.IsTrue(address.Equals(expectedEqual));
        }

        [TestMethod]
        public void EqualsZipTest()
        {
            var builder = new AddressBuilder().SetZip("a");

            var expectedNotEqual = new AddressBuilder().SetZip("A").Build();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(expectedNotEqual));
            Assert.IsTrue(address.Equals(expectedEqual));
        }

        [TestMethod]
        public void EqualsCountryTest()
        {
            var builder = new AddressBuilder().SetCountry("a");

            var expectedNotEqual = new AddressBuilder().SetCountry("A").Build();
            var expectedEqual = builder.Build();

            var address = builder.Build();

            Assert.IsFalse(address.Equals(expectedNotEqual));
            Assert.IsTrue(address.Equals(expectedEqual));
        }
        #endregion

        [TestMethod]
        public void ToStringTest()
        {
            var builder = new AddressBuilder().SetName("Cherie Warren").SetStreet("7721 11th Ave NW").SetCity("Seattle").SetState("WA").SetZip("98117").SetCountry("USA");
            var address = builder.Build();

            Assert.AreEqual(builder.ToString(), address.ToString());
        }

        // TODO: Test TryParse
    }
}
