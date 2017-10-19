using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ConsoleAddressTest
{
    [TestClass]
    public class ControllerTest
    {
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
            {
                var controller = new Controller(new Presenter(writer));
                controller.Print(addresses);

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
