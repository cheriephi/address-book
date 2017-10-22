using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace ConsoleAddress
{
    /// <summary>
    /// Model view controller (MVC) design pattern. The controller. 
    /// "Defines the way the user interface reacts to user input." (GoF)
    /// The application interacts via the command-line. 
    /// But unit testing wants to interact differently.
    /// Designed for testability (presentation layer dependency passed in).
    /// </summary>
    class Controller
    {
        /// <summary>
        /// Prints help text to the appropriate stream.
        /// </summary>
        /// <param name="writer"></param>
        internal void ShowUsage(StreamWriter writer)
        {
            var usage = new string[]
            {
                "Usage: AddressBook [command]",
                "Where command is one of:",
                "    add [name] [address]                  - add to the addresses.",
                "    update [name] [field] [new value]     - update the address.",
                "    remove [name]                         - remove from the addresses.",
                "    print [file name]?                    - print the addresses",
                "    save [file name]                      - saves the addresses",
                "    load [file name]                      - loads saved addresses",
                "",
                "Where address is a comma and space delimited string:",
                "         [street] [city] [state] [zip] [country]",
                "    example:",
                "         1600 Pennsylvania Ave, Washington, DC, 20500, USA",
                "Where field is [name | street | city | state | zip | country]",
                "",
                "Where file name is a fully qualified file name",
                "",
                "Where print prints to a console window (when file name is not specified)",
                "   or a .csv or .xml file",
                "",
            };

            var text = string.Join(Environment.NewLine, usage);

            writer.WriteLine(text);
            writer.Flush();
        }

        /// <summary>
        /// Performs address book functionality.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Whether the action succeeded.</returns>
        /// <remarks>Designed for testability.</remarks>
        internal bool ProcessArgs(string[] args)
        {
            var success = false;
            var book = new AddressBook();

            if (args.Length == 3 && args[0] == "add")
            {
                Address address;
                var isValid = Address.TryParse(args[2], out address);
                if (!isValid)
                {
                    return success;
                }

                book.Add(args[1], address);
                var addresses = book.GetAll();
                success = true;
            }
            else if (args.Length == 4 && args[0] == "update" && Enum.IsDefined(typeof(AddressKey), args[2]))
            {
                book.Update(args[1], args[2], args[3]);
                var addresses = book.GetAll();
                success = true;
            }
            else if (args.Length == 2 && args[0] == "remove")
            {
                book.Remove(args[1]);
                var addresses = book.GetAll();
                success = true;
            }
            else if (args.Length == 1 && args[0] == "print")
            {
                var addresses = book.GetAll();

                using (var writer = new StreamWriter(Console.OpenStandardOutput()))
                {
                    Print(writer, addresses);
                }
                success = true;
            }
            else if (args.Length == 2 && args[0] == "print")
            {
                var fileName = args[1];
                var addresses = book.GetAll();

                if (GetFileText(fileName, addresses, out string text))
                {
                    using (var writer = new StreamWriter(File.OpenWrite(fileName)))
                    {
                        Print(writer, text);
                    }

                    success = true;
                }
            }
            else if (args.Length == 2 && args[0] == "save")
            {
                var fileName = args[1];

                if (File.Exists(fileName)) { File.Delete(fileName); }

                // Validate what we can
                var path = Path.GetDirectoryName(fileName);
                // Path will be empty if the file does not contain directory information.
                // Exists only returns true if the process has permissions to read from it.
                if (string.IsNullOrEmpty(path) || Directory.Exists(path))
                {
                    using (var output = File.Create(fileName))
                    {
                        Save(output, book);
                    }

                    success = true;
                }
            }
            else if (args.Length == 2 && args[0] == "load")
            {
                var fileName = args[1];
                if (File.Exists(fileName))
                { 
                    using (var input = File.OpenRead(fileName))
                    {
                        book = Load(input);
                    }
                }
            }

            if (!success)
            {
                using (var writer = new StreamWriter(Console.OpenStandardOutput()))
                {
                    ShowUsage(writer);
                }
            }

            return success;
        }

        #region Print
        /// <summary>
        /// Prints to the input StreamWriter, one line per address book item.
        /// </summary>
        /// <param name="writer">Console.OpenStandardOutput() creates a stream that writes to the console window.</param>
        /// <param name="addresses"></param>
        /// <remarks>By passing the writer dependency in, the code is more flexible \ easier to test.
        /// It is up to the calling process to construct and dispose of the writer.
        /// </remarks>
        internal void Print(StreamWriter writer, Dictionary<string, Address> addresses)
        {
            // Build a string of dictionary items: the key and the associated value.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, Address>, string> itemToString = pair => pair.Key + ", " + pair.Value;

            var itemList = string.Join(Environment.NewLine, addresses.Select(itemToString).ToArray());

            writer.WriteLine(itemList);
            writer.Flush();
        }

        internal void Print(StreamWriter writer, string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }
        #endregion

        #region Serialization
        /// <summary>
        /// Saves (serializes) the input address book to the output stream.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="addressBook"></param>
        internal void Save(Stream output, AddressBook addressBook)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(output, addressBook);
        }

        /// <summary>
        /// Returns an address book serialized from the input stream.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal AddressBook Load(Stream input)
        {
            var formatter = new BinaryFormatter();
            var addressBook = (AddressBook)formatter.Deserialize(input);
            return addressBook;
        }
        #endregion

        internal bool GetFileText(string fullyQualifiedFileName, Dictionary<string, Address> addresses, out string text)
        {
            var fileExtension = Path.GetExtension(fullyQualifiedFileName).ToLower();
            switch (fileExtension)
            {
                case ".csv":
                    text = ToCsvString(addresses);
                    break;
                case ".xml":
                    text = ToXmlString(addresses);
                    break;
                default:
                    text = "";
                    return false;
            }

            var path = Path.GetDirectoryName(fullyQualifiedFileName);
            // Path will be empty if the file does not contain directory information.
            // Exists only returns true if the process has permissions to read from it.
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                return false;
            }

            return true;
        }

        #region Transform addresses to string
        /// <summary>
        /// Outputs one comma-delimited per address book item, wrapped in quotes.
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns></returns>
        /// <remarks>Currently doesn't handle when data contains data to be escaped,
        /// but this would be the appropriate location to handle it.
        /// (Csvs escape double quotes by using double double quotes).
        /// </remarks>
        private string ToCsvString(Dictionary<string, Address> addresses)
        {
            // Build a string of dictionary items: the key and the associated value.
            // Use a Func (an inline delegate) to make the code more readable and easier to debug.
            Func<KeyValuePair<string, Address>, string> itemToString = pair => "\"" + pair.Key + "\",\"" + pair.Value + "\"";

            var itemList = string.Join(Environment.NewLine, addresses.Select(itemToString).ToArray());

            return itemList;
        }

        /// <summary>
        /// Outpus addresses as an xml string. 
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns>Xml string <root><contact name="key" address="value"></contact></root></returns>
        /// <remarks>Currently doesn't handle when data contains data to be escaped,
        /// but this would be the appropriate location to handle it.
        /// </remarks>
        /// <seealso cref="ToCsvString"/>
        private string ToXmlString(Dictionary<string, Address> addresses)
        {
            var doc = new XDocument();
            doc.Add(
                new XElement("root", addresses.Select(pair =>
                    new XElement
                    (
                        "contact",
                        new XAttribute("name", pair.Key),
                        new XAttribute("address", pair.Value)
                    )
                ))
            );

            return doc.ToString();
        }
        #endregion
    }
}
