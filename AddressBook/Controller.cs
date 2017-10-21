using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        /// Prints help text to the appropriate printer.
        /// </summary>
        /// <param name="presenter"></param>
        internal void ShowUsage(Presenter presenter)
        {
            var usage = new string[]
            {
                "Usage: AddressBook [command]",
                "Where command is one of:",
                "    add [name] [address]                  - add to the addresses.",
                "    update [name] [field] [new value]     - update the address.",
                "    remove [name]                         - remove from the addresses.",
                "    print [file name]?                    - print the addresses",
                "",
                "Where address is a comma and space delimited string:",
                "         [street] [city] [state] [zip] [country]",
                "    example:",
                "         1600 Pennsylvania Ave, Washington, DC, 20500, USA",
                "Where field is [name | street | city | state | zip | country]",
                "",
                "Where file name is a fully qualified .csv or .xml file name",
                "   if [file name] is not specified, this prints to the console window",
                "",

            };

            presenter.Print(string.Join(Environment.NewLine, usage));
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
                using (var printer = new Presenter())
                {
                    Print(printer, addresses);
                }
                success = true;
            }
            else if (args.Length == 2 && args[0] == "print")
            {
                var addresses = book.GetAll();

                var fileName = args[1];
                var fileExtension = Path.GetExtension(fileName);
                switch (fileExtension)
                {
                    case ".csv":
                        using (var printer = new CsvPrinter(fileName))
                        {
                            Print(printer, addresses);
                        }
                        break;
                    case ".xml":
                        using (var printer = new XmlPrinter(fileName))
                        {
                            Print(printer, addresses);
                        }
                        break;
                    default:
                        success = false;
                        break;
                }

                success = true;
            }
            else
            {
                using (var printer = new Presenter())
                {
                    ShowUsage(printer);
                }
            }

            return success;
        }

        /// <summary>
        /// Prints to the appropriate printer. Translates domain specific data to content the printer can understand.
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="addresses"></param>
        internal void Print(IPrinter printer, Dictionary<string, Address> addresses)
        {
            var content = addresses.ToDictionary(k => k.Key, k => k.Value.ToString());

            printer.Print(content);
        }
    }
}
