using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConsoleAddressTest
{
    /// <summary>
    /// Test all the fields in the expected object against an object accessor.
    /// </summary>
    class Helper
    {
        internal static void AssertAreEqual(AddressBuilder expected, Address actual)
        {
            Assert.AreEqual(expected.GetName(), actual.Name, "Name");
            Assert.AreEqual(expected.GetStreet(), actual.Street, "Street");
            Assert.AreEqual(expected.GetCity(), actual.City, "City");
            Assert.AreEqual(expected.GetState(), actual.State, "State");
            Assert.AreEqual(expected.GetZip(), actual.Zip, "Zip");
            Assert.AreEqual(expected.GetCountry(), actual.Country, "Country");
        }

        internal static void AssertAreEqual(AddressBookBuilder expected, AddressBook actual, string message)
        {
            Assert.AreEqual(expected.GetAddressBuilders().Count, actual.CountOfAddresses, $"CountOfAddresses {message}");

            // Test that each of the addresses we expect exist inside the addresses that we actually have
            foreach (var builder in expected.GetAddressBuilders())
            {
                var builtAddress = builder.Build();

                bool found = false;
                foreach (var address in actual.GetAll())
                {
                    if (address.Equals(builtAddress))
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found, $"{builder.GetName()} Address is not found {message}");
            }
        }

        internal static void AssertAreEqual(AddressBookBuilder expected, AddressBook actual)
        {
            AssertAreEqual(expected, actual, "");
        }
    }
}
