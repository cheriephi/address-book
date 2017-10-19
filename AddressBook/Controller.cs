using System;
using System.Collections.Generic;

namespace ConsoleAddress
{
    /// <summary>
    /// Designed for testability (presentation layer dependency passed in).
    /// </summary>
    class Controller
    {
        private Presenter presenter;

        internal Controller(Presenter presenter)
        {
            this.presenter = presenter;
        }

        /// <summary>
        /// Help text.
        /// </summary>
        /// <returns></returns>
        internal void ShowUsage()
        {
            var usage = new string[]
            {
                "Usage: AddressBook [command]",
                "Where command is one of:",
                "    add [name] [address]                  - add to the addresses.",
                "    update [name] [field] [new value]     - update the address.",
                "    remove [name]                         - remove from the addresses.",
                "    print                                 - print the addresses",
                "",
                "Where field is [name | street | city | state | zip | country]",
                "",
                "Where address is a comma and space delimited string:",
                "         [street] [city] [state] [zip] [country]",
                "    example:",
                "         1600 Pennsylvania Ave, Washington, DC, 20500, USA",
            };

            presenter.Print(string.Join(Environment.NewLine, usage));
        }

        /// <summary>
        /// Performs address book functionality, minus presentation layer.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="result"></param>
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
                Print(addresses);
                success = true;
            }

            return success;
        }

        internal void Print(Dictionary<string, Address> addresses)
        {
            presenter.Print(addresses);
        }
    }
}
