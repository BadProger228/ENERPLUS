using ENERPLUS.Tests;

namespace ENERPLUS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            IDFTests idfTests = new IDFTests();
            idfTests.Setup();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}