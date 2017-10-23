using System;

// Namespace avoids collisions with its class names.
// see https://blogs.msdn.microsoft.com/ericlippert/2010/03/09/do-not-name-a-class-the-same-as-its-namespace-part-one/ 
namespace ConsoleAddress
{
    /// <summary>
    /// Address \ contact functionality.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Performs the specified address book action.
        /// </summary>
        /// <param name="args"></param>
        /// <see cref="Controller.ShowUsage"/>
        static void Main(String[] args)
        {
            int exitCode = 1;

            try
            {
                var controller = new Controller();
                var success = controller.ProcessArgs(args);

                exitCode = Convert.ToInt32(!success);
            }
            catch (Exception e)
            {
                exitCode = 1;
                Console.WriteLine(e.ToString());
            }
            finally
            {
                // Return 0 if success; 1 if failure
                Environment.Exit(exitCode);
            }
        }

    }
}
