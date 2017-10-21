using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ConsoleAddressTest
{
    /// <summary>
    /// Tests for address book controller.
    /// </summary>
    /// <remarks>Work in progress; not fully automated or covered due to project scope.</remarks>
    [TestClass]
    public class ControllerTest
    {
        /// <summary>
        /// Tests we return failure if no args are passed in.
        /// </summary>
        [TestMethod]
        public void NoArgsTest()
        {
            bool success = true;

            using (var presenter = new Presenter())
            {
                var controller = new Controller();
                success = controller.ProcessArgs(new String[] { });
            }

            Assert.IsFalse(success);
        }

        /// <summary>
        /// Tests we return usage data.
        /// </summary>
        [TestMethod]
        public void ShowUsageTest()
        {
            string result;

            // Rather than using Console.Out for the stream, use memory, so we can test it.
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var presenter = new Presenter(writer))
            {
                var controller = new Controller();
                controller.ShowUsage(presenter);

                // Grab what's in memory while we have it.
                stream.Position = 0;
                using (var readStream = new StreamReader(stream))
                {
                    result = readStream.ReadToEnd();
                }
            }

            // Inexact content check.
            Assert.IsTrue(result.StartsWith("Usage"));
        }

        /// <summary>
        /// Tests that the default address book prints a non empty stream.
        /// </summary>
        [TestMethod]
        public void PrintTest()
        {
            var addressBook = new AddressBook();
            var addresses = addressBook.GetAll();

            string result;

            // Rather than using Console.Out for the stream, use memory, so we can test it.
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var presenter = new Presenter(writer))
            {
                var controller = new Controller();
                controller.Print(presenter, addresses);

                // Grab what's in memory while we have it.
                stream.Position = 0;
                using (var readStream = new StreamReader(stream))
                {
                    result = readStream.ReadToEnd();
                }
            }

            // Inexact content check.
            Assert.IsTrue(result.Length > 0);
        }
    }
}
