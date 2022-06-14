using FlaUI.Core.Tools;
using FlaUI.UIA3;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;

namespace BNetzA_SysmonTestingAutomation.UseCases
{
    internal class UseCaseEmail
    {
        public UseCaseEmail() { }

        private FlaUI.Core.Application _theApp;
        private UIA3Automation _automation;
        private FlaUI.Core.AutomationElements.Window _mainWindow;
        private const int BigWaitTimeout = 3000;
        private const int SmallWaitTimeout = 1000;

        public void Setup()
        {
            Console.WriteLine("UseCase wird initiiert. Der PC wird im Anschluss heruntergefahren!");
            RegistryKey registryKeyCLSID = Registry.ClassesRoot.OpenSubKey("Word.Application\\CLSID");
            string keyValueCLSID = (string)registryKeyCLSID.GetValue("");
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("WOW6432Node\\CLSID\\" + keyValueCLSID + "\\Localserver32");
            string pathToWord = (string)registryKey.GetValue("");
            pathToWord = pathToWord.Replace("\"", "");
            pathToWord = pathToWord.Replace(" /Automation", "");
            _theApp = FlaUI.Core.Application.Launch(new ProcessStartInfo(pathToWord));
            _automation = new UIA3Automation();
            _mainWindow = _theApp.GetMainWindow(_automation);
        }

        public void Teardown()
        {
            _automation?.Dispose();
            _theApp?.Close();
        }

        public void Foo()
        {
            // This will wait for the child element or timeout 
            var newDocumentButton = WaitForElement(() =>
                _mainWindow.FindFirstDescendant(cf =>
                    cf.ByAutomationId("TabOfficeStart").And(cf.ByName("Neu"))));

            newDocumentButton.Click();

            var newDocumentCreateButton = WaitForElement(() =>
                _mainWindow.FindFirstDescendant(cf =>
                    cf.ByAutomationId("AIOStartDocument").And(cf.ByName("Leeres Dokument"))));

            newDocumentCreateButton.Click();

            //window.FindFirstDescendant(cf.ByAutomationId("TabOfficeStart").And(cf.ByName("Neu"))).Click();
            //Thread.Sleep(2000);
            //window.FindFirstDescendant(cf.ByAutomationId("AIOStartDocument").And(cf.ByName("Leeres Dokument"))).Click();
            //Thread.Sleep(2000);
            FlaUI.Core.Input.Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_P);
            Thread.Sleep(2000);
            FlaUI.Core.Input.Keyboard.Type(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            Thread.Sleep(2000);
            _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("1001")).Click();
            //FlaUI.Core.Input.Keyboard.Type(GetRandomDocumentName());
            FlaUI.Core.Input.Keyboard.Type(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
        }

        private T WaitForElement<T>(Func<T> getter)
        {
            var retry = Retry.WhileNull<T>(
                () => getter(),
                TimeSpan.FromMilliseconds(BigWaitTimeout));

            if (!retry.Success)
            {
                Console.WriteLine("Fehler beim Finden des Elements. Beende Laufzeit");
                Teardown();
            }

            return retry.Result;
        }
    }
}
