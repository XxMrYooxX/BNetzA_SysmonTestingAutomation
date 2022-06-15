﻿using BNetzA_SysmonTestingAutomation.Gateways;
using System;

namespace BNetzA_SysmonTestingAutomation.View
{
    /// <summary>
    /// Menu-Klasse als Benutzerinterface
    /// Implementiert als Singleton Klasse
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// Singleton Menu Instance Objekt
        /// </summary>
        private static Menu instance;
        /// <summary>
        /// Array mit Menüinhalten
        /// </summary>
        private readonly MenuItems[] menuItems;
        /// <summary>
        /// Konstruktor des Menu
        /// </summary>
        private Menu()
        {
            menuItems = new MenuItems[4];
            menuItems[0] = new MenuItems(0, "Dateierstellung");
            menuItems[1] = new MenuItems(1, "Druckvorgang");
            menuItems[2] = new MenuItems(2, "Email");
            menuItems[3] = new MenuItems(3, "RemoteDesktop");
        }
        /// <summary>
        /// getInstance Methode als Getter für Singleton Instanz
        /// </summary>
        /// <returns>Menu</returns>
        public static Menu getInstance()
        {
            if (instance == null)
            {
                instance = new Menu();
            }
            return instance;
        }
        /// <summary>
        /// Run-Methode zum Ausführen des Menüs
        /// </summary>
        public void Run()
        {
            try
            {
                UseCaseGateway gateway = new UseCaseGateway();
                switch (PrintMenu())
                {
                    case 0:
                        Console.WriteLine("\n\n Use Case " + menuItems[0].DESCRIPTION + "gewählt. ID: " + menuItems[0].ID);
                        var usecase0 = gateway.createUseCaseDateierstellung();
                        usecase0.Setup();
                        usecase0.Foo();
                        usecase0.Teardown();
                        break;
                    case 1:
                        Console.WriteLine("\n\n Use Case " + menuItems[1].DESCRIPTION + "gewählt. ID: " + menuItems[1].ID);
                        var usecase1 = gateway.createUseCaseDruckvorgang();
                        usecase1.Setup();
                        usecase1.Foo();
                        usecase1.Teardown();
                        break;
                    case 2:
                        Console.WriteLine("\n\n Use Case " + menuItems[2].DESCRIPTION + "gewählt. ID: " + menuItems[2].ID);
                        var usecase2 = gateway.createUseCaseEmail();
                        usecase2.Setup();
                        usecase2.Foo();
                        usecase2.Teardown();
                        break;
                    case 3:
                        Console.WriteLine("\n\n Use Case " + menuItems[3].DESCRIPTION + "gewählt. ID: " + menuItems[3].ID);
                        var usecase3 = gateway.createUseCaseRemoteDesktop();
                        usecase3.Setup();
                        usecase3.Foo();
                        usecase3.Teardown();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Print Menu Methode zum Ausgeben des Menüinhalts und Abfrage der Auswahl
        /// </summary>
        /// <returns>int</returns>
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
            if (choice > menuItems.Length || choice < 0)
            {
                Console.WriteLine("Fehlerhafte Eingabe!");
                PrintMenu();
            }
            return choice;
        }


    }
    /// <summary>
    /// Struct zur Datenhaltung von Menüinhalten
    /// </summary>
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
