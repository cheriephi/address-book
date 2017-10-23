using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

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
        /// Tests we return failure if invalid args are passed in.
        /// </summary>
        [TestMethod]
        [DataTestMethod]
        [DataRow(new string[] { })] // No args
        [DataRow(new string[] { "print", "a.foo" }) ] // Invalid file extension
        [DataRow(new string[] { "print", @"C:\DoesNotExist\a.csv" })] // Invalid directory
        [DataRow(new string[] { "print", @"C:\DoesNotExist\a.xml" })] // Invalid directory
        public void InvalidArgsTest(string[] args)
        {
            bool success = true;

            var controller = new Controller();
            success = controller.ProcessArgs(args);

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
            {
                var controller = new Controller();
                controller.ShowUsage(writer);

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
        /// <seealso cref="ShowUsageTest">For memory stream information.</seealso>
        [TestMethod]
        public void PrintTest()
        {
            var addressBook = new AddressBook();
            var addresses = addressBook.GetAll();

            string result;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                var controller = new Controller();
                controller.Print(writer, addresses);
                stream.Position = 0;
                using (var readStream = new StreamReader(stream))
                {
                    result = readStream.ReadToEnd();
                }
            }

            // Inexact content check.
            Assert.IsTrue(result.Length > 0);
        }

        #region File tests
        /// <summary>
        /// Tests printing csv and xml data to a stream.
        /// </summary>
        [TestMethod]
        [DataTestMethod]
        [DataRow("a.CSV")]
        [DataRow("a.XML")]
        [TestCategory("Manual")]
        public void PrintFileToStreamTest(string fileName)
        {
            var book = new AddressBook();
            var addresses = book.GetAll();

            string result;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                var controller = new Controller();
                var success = controller.GetFileText(fileName, addresses, out string text);

                controller.Print(writer, text);
                stream.Position = 0;
                using (var readStream = new StreamReader(stream))
                {
                    result = readStream.ReadToEnd();
                }
            }

            // Inexact content check.
            Assert.IsTrue(result.Length > 0);
        }

        /// <summary>
        /// Tests printing to a csv or xml file.
        /// </summary>
        [TestMethod]
        [DataRow("a.cSv")] // Use mixed file extension capitalization to test that also
        [DataRow("a.xMl")]
        [TestCategory("Manual")]
        public void PrintToFileTest(string localFileName)
        {
            // Build the file name from the local assembly directory location
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + localFileName;

            if (File.Exists(fileName)) { File.Delete(fileName); }

            var book = new AddressBook();
            var addresses = book.GetAll();

            using (var writer = new StreamWriter(File.OpenWrite(fileName)))
            {
                var controller = new Controller();
                var success = controller.GetFileText(fileName, addresses, out string text);

                controller.Print(writer, text);
                // Test we can overwrite
                controller.Print(writer, text);
            }

            var file = new FileInfo(fileName);
            Assert.IsTrue(file.Length > 0); // Inexact check; makes sure the file is not empty.

            // NOTE: validate data, clean up results.
            if (File.Exists(fileName)) { File.Delete(fileName); }
        }
        #endregion
    }
}
