using System;

/// <summary>
/// Address \ contact functionality.
/// </summary>
/// <remarks>Namespace avoids collisions with its class names.</remarks>
/// <see href="https://blogs.msdn.microsoft.com/ericlippert/2010/03/09/do-not-name-a-class-the-same-as-its-namespace-part-one/"/>
namespace ConsoleAddress
{
    class Program
    {
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
                "Where command is one of:",
                "    add [name] [address]                  - add to the addresses.",
                "    update [name] [field] [new value]     - update the address.",
                "    remove [name]                         - remove from the addresses.",
                "",
                "Where field is [name | street | city | state | zip | country]",
                "",
                "Where address is a comma and space delimited string:",
                "         [street] [city] [state] [zip] [country]",
                "    example:",
                "         1600 Pennsylvania Ave, Washington, DC, 20500, USA",
            };
            
            return String.Join(Environment.NewLine, usage);
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
            var book = new AddressBook();


            if (args.Length == 3 && args[0] == "add")
            {
                Address address;
                var isValid = Address.TryParse(args[2], out address);
                if (!isValid)
                {
                    result = getUsage();
                    return false;
                }

                book.Add(args[1], address);
                var addresses = book.GetAll();
                result = AddressBook.ToString(addresses);
                return true;
            }
            else if (args.Length == 4 && args[0] == "update" && Enum.IsDefined(typeof(AddressKey), args[2]))
            {
                book.Update(args[1], args[2], args[3]);
                var addresses = book.GetAll();
                result = AddressBook.ToString(addresses);
                return true;
            }
            else if (args.Length == 2 && args[0] == "remove")
            {
                book.Remove(args[1]);
                var addresses = book.GetAll();
                result = AddressBook.ToString(addresses);
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
