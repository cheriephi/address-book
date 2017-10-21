using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Xml presenter.
    /// </summary>
    /// <exception cref="System.ArgumentException">When invalid file name passed in (invalid path, not a .csv).</exception>
    /// <remarks>Bubbles up unhandled exceptions, such as no write access to a directory or file lock contention.</remarks>
    class XmlPrinter : IDisposable, IPrinter
    {
        private StreamWriter writer;

        #region Constructors
        internal XmlPrinter(string fullyQualifiedFileName)
        {
            var fileExtension = Path.GetExtension(fullyQualifiedFileName);
            if (fileExtension.ToLower() != ".xml")
            {
                throw new ArgumentException($"Only .xml files, not {fileExtension} files are supported.");
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
        /// Prints dictionary to xml. <root><contact name="key" address="value"></contact></root>
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        /// <remarks>Currently doesn't handle when data contains data to be escaped,
        /// but this would be the appropriate location to handle it.
        /// The design needs refinement: the xml attribute names are domain specific, but the input is not.
        /// </remarks>
        /// <seealso cref="CsvPrinter.Print(Dictionary{string, string})"/>
        public void Print(Dictionary<string, string> dictionary)
        {
            var doc = new XDocument();
            doc.Add(
                new XElement("root", dictionary.Select(pair =>
                    new XElement
                    (
                        "contact",
                        new XAttribute("name", pair.Key),
                        new XAttribute("address", pair.Value)
                    )
                ))
            );

            writer.WriteLine(doc.ToString());
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
