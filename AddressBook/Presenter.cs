using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Model view controller (MVC) design pattern. The "view" or presenter or "screen presentation".
    /// </summary>
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
        /// <param name="dictionary"></param>
        /// <returns></returns>
        internal void Print(Dictionary<string, string> dictionary)
        {
            // Build a string of addresses: the name and the associated address.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, string>, string> addressBookItemToString = pair => pair.Key + ", " + pair.Value;

            var addressList = string.Join(Environment.NewLine, dictionary.Select(addressBookItemToString).ToArray());

            writer.WriteLine(addressList);
            writer.Flush();
        }
    }
}
