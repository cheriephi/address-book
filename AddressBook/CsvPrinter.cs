using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Csv (excel) presenter.
    /// </summary>
    /// <exception cref="System.ArgumentException">When invalid file name passed in (invalid path, not a .csv).</exception>
    /// <remarks>Bubbles up unhandled exceptions, such as no write access to a directory or file lock contention.</remarks>
    class CsvPrinter : IDisposable, IPrinter
    {
        private StreamWriter writer;

        #region Constructors
        internal CsvPrinter(string fullyQualifiedFileName)
        {
            var fileExtension = Path.GetExtension(fullyQualifiedFileName);
            if (fileExtension.ToLower() != ".csv")
            {
                throw new ArgumentException($"Only .csv files, not {fileExtension} files are supported.");
            }

            var path = Path.GetDirectoryName(fullyQualifiedFileName);
            // Path will be empty if the file does not contain directory information.
            // Exists only returns true if the process has permissions to read from it.
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                throw new ArgumentException($"Directory {path} does not exist.");
            }

            var fileStream = File.OpenWrite(fullyQualifiedFileName);
            this.writer = new StreamWriter(fileStream);
        }
        #endregion

        /// <summary>
        /// Prints comma-delimited one line per dictionary item, wrapped in quotes.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        /// <remarks>Currently doesn't handle when data contains data to be escaped,
        /// but this would be the appropriate location to handle it.
        /// (Csvs escape double quotes by using double double quotes).
        /// </remarks>
        public void Print(Dictionary<string, string> dictionary)
        {
            /// Build a string of dictionary items: the key and the associated value.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, string>, string> itemToString = pair => "\"" + pair.Key + "\",\"" + pair.Value + "\"";

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
