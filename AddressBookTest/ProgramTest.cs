using ConsoleAddress;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ConsoleAddressTest
{
    [TestClass]
    public class ProgramTest
    {
        /// <summary>
        /// Tests we return failure if no args are passed in.
        /// </summary>
        [TestMethod]
        public void NoArgsTest()
        {
            bool success = true;

            using (var writer = new StreamWriter(Console.OpenStandardOutput()))
            {
                var controller = new Controller(new Presenter(writer));
                success = controller.ProcessArgs(new String[] { });
            }

            Assert.IsFalse(success);
        }
    }
}
