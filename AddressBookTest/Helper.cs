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
        internal static void AssertAreEqual(AddressBuilder expected, Address actual, string message)
        {
            Assert.AreEqual(expected.GetName(), actual.Name, $"Name {message}");
            Assert.AreEqual(expected.GetStreet(), actual.Street, $"Street {message}");
            Assert.AreEqual(expected.GetCity(), actual.City, $"City {message}");
            Assert.AreEqual(expected.GetState(), actual.State, $"State {message}");
            Assert.AreEqual(expected.GetZip(), actual.Zip, $"Zip {message}");
            Assert.AreEqual(expected.GetCountry(), actual.Country, $"Country {message}");
        }

        internal static void AssertAreEqual(AddressBuilder expected, Address actual)
        {
            AssertAreEqual(expected, actual, "");
        }

        internal static void AssertAreEqual(AddressBookBuilder expected, AddressBook actual, string message)
        {
            var expectedBuilders = expected.GetAddressBuilders();
            var actualAddresses = actual.GetAll();

            Assert.AreEqual(expectedBuilders.Count, actualAddresses.Count, $"Count {message}");

            // Test that each of the addresses we expect exist inside the addresses that we actually have
            foreach (var builder in expectedBuilders)
            {
                bool found = false;
                foreach (var address in actualAddresses)
                {
                    if (builder.Key == address.Key)
                    {
                        AssertAreEqual(builder.Value, address.Value);

                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found, $"{builder.Key} Address is not found {message}");
            }
        }

        internal static void AssertAreEqual(AddressBookBuilder expected, AddressBook actual)
        {
            AssertAreEqual(expected, actual, "");
        }
    }
}
