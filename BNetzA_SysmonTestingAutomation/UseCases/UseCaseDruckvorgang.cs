using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaUI.UIA3;
using Microsoft.Win32;

namespace BNetzA_SysmonTestingAutomation.UseCases
{
    internal class UseCaseDruckvorgang
    {
        public UseCaseDruckvorgang() { }

        public void Run()
        {
            Console.WriteLine("DEBUG Druckvorgang");
            RegistryKey registryKeyCLSID = Registry.ClassesRoot.OpenSubKey("Word.Application\\CLSID");
            string keyValueCLSID = (string)registryKeyCLSID.GetValue("");
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("WOW6432Node\\CLSID\\" + keyValueCLSID + "\\Localserver32");
            string pathToWord = (string) registryKey.GetValue("");
            pathToWord = pathToWord.Replace("\"", "");
            pathToWord = pathToWord.Replace(" /Automation", "");
            var app = FlaUI.Core.Application.Launch(pathToWord);
            using(var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                Console.Write(window.Title);
            }

        }
    }
}
