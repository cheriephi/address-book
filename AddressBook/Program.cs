using System;
using System.Collections.Generic;

/// <summary>
/// Address \ contact functionality.
/// </summary>
/// <remarks>Namespace avoids collisions with its class names.</remarks>
/// <see href="https://blogs.msdn.microsoft.com/ericlippert/2010/03/09/do-not-name-a-class-the-same-as-its-namespace-part-one/"/>
namespace ConsoleAddress
{
    class Program
    {
        private static AddressBook book = new AddressBook();

        /// <summary>
        /// Entry point. Performs the specified address book action.
        /// </summary>
        /// <param name="args"></param>
        /// <see cref="Usage"/>
        static void Main(String[] args)
        {
            string output;
            var result = addressController(args, out output);
            Console.WriteLine(output);

            // Return 0 if success; 1 if failure
            var exitCode = Convert.ToInt32(!result);
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// Help text.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Designed for testability (no presentation layer).</remarks>
        private static string getUsage()
        {
            var usage = new string[]
            {
                "Usage: AddressBook [command]",
                "Where command is one of: ",
                "    list                                  - list the addresses.",
                "    add [address]                         - add to the addresses.",
                "    update [old address] [new address]    - update the addresses.",
                "    remove [address]                      - remove from the addresses.",
                "",
                "Where address is a comma and space delimited string:",
                "         [name] [street] [city] [state] [zip] [country]",
                "    example:",
                "         Michelle Obama, 1600 Pennsylvania Ave, Washington, DC, 20500, USA"
            };
            
            return String.Join(Environment.NewLine, usage);
        }

        /// <summary>
        /// Extracts addresses from input arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <remarks>Ignores other arguments, such as the command to perform.</remarks>
        private static Address[] getAddressesFromArgs(string[] args)
        {
            var addressList = new List<Address>();
            foreach (var addressArg in args)
            {
                Address address;
                var isValid = Address.TryParse(addressArg, out address);
                if (isValid) { addressList.Add(address); }
            }

            return addressList.ToArray();
        }

        /// <summary>
        /// Performs address book functionality, minus presentation layer.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns>Whether the action succeeded.</returns>
        /// <remarks>Designed for testability.</remarks>
        internal static bool addressController(string[] args, out string result)
        {
            var addresses = getAddressesFromArgs(args);

            if (args.Length == 1 && args[0] == "list")
            {
                result = book.ToString();
                return true;
            }
            else if (args.Length == 2 && args[0] == "add" && addresses.Length == 1)
            {
                book.Add(addresses[0]);
                result = book.ToString();
                return true;
            }
            else if (args.Length == 3 && args[0] == "update" && addresses.Length == 2)
            {
                book.Update(addresses[0], addresses[1]);
                result = book.ToString();
                return true;
            }
            else if (args.Length == 2 && args[0] == "remove" && addresses.Length == 1)
            {
                book.Remove(addresses[0]);
                result = book.ToString();
                return true;
            }
            else
            {
                result = getUsage();
                return false;
            }
        }
    }
}
