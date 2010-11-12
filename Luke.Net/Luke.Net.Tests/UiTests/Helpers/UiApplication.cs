using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Luke.Net.Tests.UiTests.Windows;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Luke.Net.Tests.UiTests.Helpers
{
    class UiApplication : IDisposable
    {
        ApplicationUnderTest _app;
        private UiMainWindow _mainWindow;

        public void Dispose()
        {
            if(_app != null)
                _app.Close();
        }

        public UiMainWindow MainWindow
        {
            get
            {
                return _mainWindow ?? (_mainWindow = new UiMainWindow(ApplicationUnderTest));
            }
        }

        private ApplicationUnderTest ApplicationUnderTest
        {
            get
            {
                return _app ?? (_app = ApplicationUnderTest.FromProcess(GetAppProcess()));
            }
        }

        private Process GetAppProcess()
        {
            const string lukeNet = "Luke.Net";
            var processes = Process.GetProcessesByName(lukeNet);

            if (processes.Length == 1)
                return processes[0];

            if (processes.Length == 0)
            {
                var runningPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var appPath = Path.Combine(runningPath, "Luke.Net.exe");
                return Process.Start(appPath);
            }

            Console.WriteLine(@"More than one process with the name {0} was found:", lukeNet);
            foreach (var process in processes)
            {
                Console.WriteLine(@"Name -> " + process.ProcessName);
                Console.WriteLine(@"File Name -> " + process.StartInfo.FileName);
                Console.WriteLine(@"Site Name -> " + process.Site.Name);
                Console.WriteLine(@"Window Title -> " + process.MainWindowTitle);
            }

            return processes[0];
        }
    }
}
