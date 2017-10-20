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
            var controller = new Controller();
            var success = controller.ProcessArgs(args);

            // Return 0 if success; 1 if failure
            var exitCode = Convert.ToInt32(!success);
            Environment.Exit(exitCode);
        }

    }
}
