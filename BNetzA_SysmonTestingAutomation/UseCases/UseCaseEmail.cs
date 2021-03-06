using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace BNetzA_SysmonTestingAutomation.UseCases
{
    internal class UseCaseEmail : UseCase
    {
        public UseCaseEmail() { }

        private FlaUI.Core.Application _app;
        private UIA3Automation _automation;
        private Window _mainWindow;
        private const int WaitTimeout = 3000;

        public override void Setup()
        {
            Console.WriteLine("UseCase wird initiiert. Der PC wird im Anschluss heruntergefahren!");
            RegistryKey outlookPath = Registry.LocalMachine.OpenSubKey(
                "SOFTWARE\\Microsoft\\Office\\16.0\\Outlook\\InstallRoot");
            string pathToOutlookExe = (string)outlookPath.GetValue("Path") + "OUTLOOK.EXE";
            _app = FlaUI.Core.Application.Launch(new ProcessStartInfo(@pathToOutlookExe));
            _automation = new UIA3Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            Thread.Sleep(5000);
        }

        public override void Teardown()
        {
            _automation?.Dispose();
            _app?.Close();
        }

        public override void Foo()
        {
            FlaUI.Core.Input.Keyboard.TypeSimultaneously(
                FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL,
                FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_N);

            Thread.Sleep(5000);


            //Change Window  
            using (_automation)
            {
                var emailWindows = _app.GetAllTopLevelWindows(_automation).ToList();
                var emailWindow = emailWindows[0];

                WaitForElement(() =>
                emailWindow.FindFirstDescendant(cf =>
                    cf.ByAutomationId("4117"))).Click();
                FlaUI.Core.Input.Keyboard.Type("ki.marcel.hesselbach@htwsaar.de");

                WaitForElement(() =>
                    emailWindow.FindFirstDescendant(cf =>
                        cf.ByAutomationId("4101"))).Click();
                FlaUI.Core.Input.Keyboard.Type("Sysmon Email Use Case Test Mail");

                WaitForElement(() =>
                    emailWindow.FindFirstDescendant(cf =>
                        cf.ByAutomationId("Body"))).Click();
                FlaUI.Core.Input.Keyboard.Type(".");


                var newDocumentButton = WaitForElement(() =>
                    emailWindow.FindFirstDescendant(cf =>
                        cf.ByAutomationId("4256")));

                newDocumentButton.Click();
            }
            Thread.Sleep(10000);
        }

        public override T WaitForElement<T>(Func<T> getter)
        {
            var retry = Retry.WhileNull<T>(
                () => getter(),
                TimeSpan.FromMilliseconds(WaitTimeout));

            if (!retry.Success)
            {
                Console.WriteLine("Fehler beim Finden des Elements. Beende Laufzeit");
                Teardown();
            }

            return retry.Result;
        }
    }
}
