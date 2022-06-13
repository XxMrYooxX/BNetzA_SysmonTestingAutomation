using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNetzA_SysmonTestingAutomation.Gateways;

namespace BNetzA_SysmonTestingAutomation.View
{
    internal class Menu
    {
        private static Menu instance;
        private static int menuLow = 0;
        private static int menuHigh = 3;
        private MenuItems[] menuItems;
        private Menu() {
            menuItems = new MenuItems[4];
            menuItems[0] = new MenuItems(0, "Dateierstellung");
            menuItems[1] = new MenuItems(1, "Druckvorgang");
            menuItems[2] = new MenuItems(2, "Email");
            menuItems[3] = new MenuItems(3, "RemoteDesktop");
        }
        public static Menu getInstance()
        {
            if (instance == null)
            {
                instance = new Menu();
            }
            return instance;
        }

        public void Run()
        {
            UseCaseGateway gateway = new UseCaseGateway();
            switch (PrintMenu())
            {
                case 0:
                    Console.WriteLine("\n\n Use Case " + menuItems[0].DESCRIPTION + "gewählt. ID: " + menuItems[0].ID);
                    var usecase0 = gateway.createUseCaseDateierstellung();
                    usecase0.Run();
                    break;
                case 1:
                    Console.WriteLine("\n\n Use Case " + menuItems[1].DESCRIPTION + "gewählt. ID: " + menuItems[1].ID);
                    var usecase1 = gateway.createUseCaseDruckvorgang();
                    usecase1.Run();
                    break;
                case 2:
                    Console.WriteLine("\n\n Use Case " + menuItems[2].DESCRIPTION + "gewählt. ID: " + menuItems[2].ID);
                    var usecase2 = gateway.createUseCaseEmail();
                    usecase2.Run();
                    break;
                case 3:
                    Console.WriteLine("\n\n Use Case " + menuItems[3].DESCRIPTION + "gewählt. ID: " + menuItems[3].ID);
                    var usecase3 = gateway.createRemoteDesktop();
                    usecase3.Run();
                    break;
                default:
                    break;
            }

        }

        public int PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Sysmon UseCase Simulator Tool");
            Console.WriteLine();
            foreach (MenuItems item in menuItems)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Console.Write("Eingabe: ");
            int choice = int.Parse(Console.ReadLine());
            if (choice > menuHigh || choice < menuLow)
            {
                Console.WriteLine("Fehlerhafte Eingabe!");
                PrintMenu();
            }
            return choice;
        }


    }
    internal struct MenuItems
    {
        public MenuItems(int id, string description)
        {
            ID = id;
            DESCRIPTION = description;
        }

        public int ID { get; set; }
        public string DESCRIPTION { get; set; }

        public override string ToString() => $"{ID}: {DESCRIPTION}";

    }
}
