using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Model view controller (MVC) design pattern. The command line "view" or presenter or "screen presentation".
    /// Defaults to a console view, but other streams are supported.
    /// </summary>
    class Presenter : IDisposable, IPrinter
    {
        private StreamWriter writer;

        #region Constructors
        internal Presenter()
        {
            this.writer = new StreamWriter(Console.OpenStandardOutput());
        }

        internal Presenter(StreamWriter writer)
        {
            this.writer = writer;
        }
        #endregion

        public void Print(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }

        /// <summary>
        /// Prints one line per dictionary item.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public void Print(Dictionary<string, string> dictionary)
        {
            // Build a string of dictionary items: the key and the associated value.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, string>, string> itemToString = pair => pair.Key + ", " + pair.Value;

            var itemList = string.Join(Environment.NewLine, dictionary.Select(itemToString).ToArray());

            writer.WriteLine(itemList);
            writer.Flush();
        }

        public void Dispose()
        {
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
    }
}
