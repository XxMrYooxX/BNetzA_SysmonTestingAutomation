using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNetzA_SysmonTestingAutomation.UseCases
{
    abstract class UseCase
    {
        private Application     _app;
        private UIA3Automation  _automation;
        private Window          _mainWindow;

        public abstract void Setup();
        public abstract void Teardown();
        public abstract void Foo();
        public abstract T WaitForElement<T>(Func<T> getter);
    }
}
