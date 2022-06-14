using FlaUI.Core.Tools;
using FlaUI.UIA3;
using System;
using System.Diagnostics;
using System.Threading;

namespace BNetzA_SysmonTestingAutomation.UseCases
{
    internal class UseCaseRemoteDesktop
    {
        public UseCaseRemoteDesktop() { }

        private FlaUI.Core.Application _theApp;
        private UIA3Automation _automation;
        private FlaUI.Core.AutomationElements.Window _mainWindow;
        private const int BigWaitTimeout = 30000;

        public void Setup()
        {
            // 64 Bit Problem MSTSC.exe -> https://stackoverflow.com/questions/3101392/starting-remote-desktop-client-no-control-over-pid-kill-pid-changes-after-star
            Console.WriteLine("UseCase wird initiiert. Der PC wird im Anschluss heruntergefahren!");
            _theApp = FlaUI.Core.Application.Launch(new ProcessStartInfo(@"C:\Windows\System32\mstsc.exe"));
            Thread.Sleep(4000);
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
            var newDocumentButton = WaitForElement(() =>
                _mainWindow.FindFirstDescendant(cf =>
                    cf.ByAutomationId("5012").And(cf.ByName("Computer:"))));

            newDocumentButton.Click();
            FlaUI.Core.Input.Keyboard.Type("STUDITNB005");
            FlaUI.Core.Input.Keyboard.Type(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);

            Thread.Sleep(300000);

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
