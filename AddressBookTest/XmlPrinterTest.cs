using ConsoleAddress;
using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleAddressTest
{
    [TestClass]
    public class XmlPrinterTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("a.foo")] // Invalid file extension
        [DataRow(@"C:\DoesNotExist\a.xml")] // Invalid directory
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidFileNameTest(string fileName)
        {
            using (var printer = new XmlPrinter(fileName)) { }
        }

        /// <summary>
        /// Tests printing to a local xml file.
        /// </summary>
        /// <remarks>The design is awkward. Ideally we don't have to go through the controller for this?
        /// </remarks>
        [TestMethod]
        [TestCategory("Manual")]
        public void ValidLocalFileTest()
        {
            var fileName = "a.xMl"; // Use mixed file extension capitalization to test that also

            if (File.Exists(fileName)) { File.Delete(fileName); }

            var book = new AddressBook();
            var addresses = book.GetAll();

            var controller = new Controller();
            using (var printer = new XmlPrinter(fileName))
            {
                controller.Print(printer, addresses);
                // Print twice to ensure we handle it
                controller.Print(printer, addresses);
            }
            // NOTE: validate data, clean up results.
        }

        /// <summary>
        /// Tests printing to a qualified xml file.
        /// </summary>
        /// <seealso cref="ValidLocalFileTest"/>
        [TestMethod]
        [TestCategory("Manual")]
        public void ValidQualifiedFileTest()
        {
            // Build the file name from the local assembly directory location
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\b.xml";
            var book = new AddressBook();
            var addresses = book.GetAll();

            var controller = new Controller();
            using (var printer = new XmlPrinter(fileName))
            {
                controller.Print(printer, addresses);
            }
        }
    }
}
