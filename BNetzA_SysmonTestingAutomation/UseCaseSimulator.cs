using BNetzA_SysmonTestingAutomation.View;

namespace BNetzA_SysmonTestingAutomation
{
    internal class UseCaseSimulator
    {
        static void Main(string[] args)
        {
            Menu menu = Menu.getInstance();
            menu.Run();
        }
    }
}
