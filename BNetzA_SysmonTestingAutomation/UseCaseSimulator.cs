using BNetzA_SysmonTestingAutomation.View;
using System;

namespace BNetzA_SysmonTestingAutomation
{
    internal class UseCaseSimulator
    {
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
