using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleAddress
{
    class Presenter
    {
        private StreamWriter writer;
        internal Presenter(StreamWriter writer)
        {
            this.writer = writer;
        }

        internal void Print(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }

        /// <summary>
        /// Prints one line per address.
        /// </summary>
        /// <param name="addressDictionary"></param>
        /// <returns></returns>
        internal void Print(Dictionary<string, Address> addressDictionary)
        {
            // Build a string of addresses: the name and the associated address.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, Address>, string> addressBookItemToString = pair => pair.Key + ", " + pair.Value.ToString();

            var addressList = string.Join(Environment.NewLine, addressDictionary.Select(addressBookItemToString).ToArray());

            writer.WriteLine(addressList);
            writer.Flush();
        }
    }
}
