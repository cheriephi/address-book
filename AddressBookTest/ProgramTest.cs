using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConsoleAddressTest
{
    [TestClass]
    public class ProgramTest
    {
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
