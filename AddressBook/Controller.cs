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
                "    add [address file] [name] [address]                - add to the addresses.",
                "    update [address file] [name] [field] [new value]   - update the address.",
                "    remove [address file] [name]                       - remove from the addresses.",
                "    print [address file] [output file]?                - print the addresses",
                "",
                "Where address is a comma and space delimited string:",
                "         [street] [city] [state] [zip] [country]",
                "    example:",
                "         1600 Pennsylvania Ave, Washington, DC, 20500, USA",
                "Where field is [name | street | city | state | zip | country]",
                "",
                "Where file is a fully qualified file name",
                "   the address file is the persistent storage",
                "",
                "Where print prints to a console window (when output file is not specified)",
                "   or a .csv or .xml output file",
                "",
            };

            var text = string.Join(Environment.NewLine, usage);

            writer.WriteLine(text);
            writer.Flush();
        }

        private bool IsAddressFileValid(string addressFileName)
        {
            if (File.Exists(addressFileName)) { return true; }

            // Check we can create the file if it doesn't exist
            var path = Path.GetDirectoryName(addressFileName);

            // Path will be empty if the file does not contain directory information.
            // Exists only returns true if the process has permissions to read from it.
            return (string.IsNullOrEmpty(path) || Directory.Exists(path));
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

            if (args.Length < 2) { goto InvalidArgs; }
            var command = args[0];
            var addressFileName = args[1];

            var isAddressFileValid = IsAddressFileValid(addressFileName);
            if (!isAddressFileValid) { goto InvalidArgs; }

            var addressBook = new AddressBook();


            if (args.Length == 4 && command == "add")
            {
                // Load the address file if it exists
                if (File.Exists(addressFileName))
                {
                    using (var input = File.OpenRead(addressFileName))
                    {
                        addressBook = Load(input);
                    }
                }


                // Add the address
                var name = args[2];
                var isValidAddress = Address.TryParse(args[3], out Address address);
                if (!isValidAddress)
                {
                    goto InvalidArgs;
                }
                addressBook.Add(name, address);


                // Save
                using (var output = File.Create(addressFileName))
                {
                    Save(output, addressBook);
                }

                success = true;
            }
            else if (args.Length == 5 && command == "update" && Enum.IsDefined(typeof(AddressKey), args[3]))
            {
                // Load the address file if it exists
                if (!File.Exists(addressFileName)) { goto InvalidArgs; }
                using (var input = File.OpenRead(addressFileName))
                {
                    addressBook = Load(input);
                }
                

                // Update the address
                var name = args[2];
                var addressKey = args[3];
                var addressValue = args[4];

                addressBook.Update(name, addressKey, addressValue);


                // Save
                using (var output = File.Create(addressFileName))
                {
                    Save(output, addressBook);
                }

                success = true;
            }
            else if (args.Length == 3 && command == "remove")
            {
                // Load the address file if it exists
                if (!File.Exists(addressFileName)) { goto InvalidArgs; }
                using (var input = File.OpenRead(addressFileName))
                {
                    addressBook = Load(input);
                }


                // Remove the address
                var name = args[2];
                addressBook.Remove(name);


                // Save
                using (var output = File.Create(addressFileName))
                {
                    Save(output, addressBook);
                }

                success = true;
            }
            else if (args.Length == 2 && command == "print")
            {
                // Load the address file if it exists
                if (!File.Exists(addressFileName)) { goto InvalidArgs; }

                using (var input = File.OpenRead(addressFileName))
                {
                    addressBook = Load(input);
                }
                var addresses = addressBook.GetAll();


                // Print
                using (var writer = new StreamWriter(Console.OpenStandardOutput()))
                {
                    Print(writer, addresses);
                }
                success = true;
            }
            else if (args.Length == 3 && command == "print")
            {
                // Load the address file if it exists
                if (!File.Exists(addressFileName)) { goto InvalidArgs; }

                using (var input = File.OpenRead(addressFileName))
                {
                    addressBook = Load(input);
                }
                var addresses = addressBook.GetAll();

                // Print
                var outputFileName = args[2];
                if (GetFileText(outputFileName, addresses, out string text))
                {
                    using (var writer = new StreamWriter(File.OpenWrite(outputFileName)))
                    {
                        Print(writer, text);
                    }

                    success = true;
                }
            }

            InvalidArgs:
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
