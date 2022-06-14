using FlaUI.UIA3;
using System;
using System.Diagnostics;
using FlaUI.Core.WindowsAPI;
using FlaUI.Core.Input;
using FlaUI.Core.AutomationElements;
using System.IO;
using System.Text;

namespace BNetzA_SysmonTestingAutomation.UseCases
{
    internal class UseCaseDateierstellung
    {
        public UseCaseDateierstellung() { }

        private FlaUI.Core.Application _theApp;
        private UIA3Automation _automation;
        private Window _mainWindow;
        private const int BigWaitTimeout = 3000;

        public void Setup()
        {
            Console.WriteLine("UseCase wird initiiert. Der PC wird im Anschluss heruntergefahren!");
            //_theApp = FlaUI.Core.Application.Launch(new ProcessStartInfo(@"C:\Windows\explorer.exe"));
            //_automation = new UIA3Automation();
            //_mainWindow = _theApp.GetMainWindow(_automation);
        }

        public void Teardown()
        {
            //_automation?.Dispose();
            //_theApp?.Close();
        }

        public void Foo()
        {
            //WaitForElement(() =>
            //        _mainWindow.FindFirstDescendant(cf =>
            //            cf.ByAutomationId("41477"))).Click();
            //Keyboard.Type("%appdata%\\Microsoft\\Windows\\Start Menu\\Programs\\Startup");
            //Keyboard.Type(VirtualKeyShort.ENTER);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\Test.bat";

            try
            {
                using (FileStream fs = File.Create(@path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Beispielhafter Eintrag.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private T WaitForElement<T>(Func<T> getter)
        {
            var retry = FlaUI.Core.Tools.Retry.WhileNull<T>(
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
