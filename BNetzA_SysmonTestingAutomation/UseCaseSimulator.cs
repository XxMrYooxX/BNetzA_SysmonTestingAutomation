using BNetzA_SysmonTestingAutomation.View;
using System;

namespace BNetzA_SysmonTestingAutomation
{
    /// <summary>
    /// Starter-Klasse mit Main-Methode
    /// </summary>
    internal class UseCaseSimulator
    {
        /// <summary>
        /// Main Methode zum Initiieren des Menu Singleton
        /// </summary>
        /// <param name="args">string[]</param>
        static void Main(string[] args)
        {
            try
            {
                Menu menu = Menu.getInstance();
                menu.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
    }
}
