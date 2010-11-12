using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Automation;
using Luke.Net.Tests.UiTests.Helpers;
using Luke.Net.Tests.UiTests.Windows;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Luke.Net.Tests.UiTests
{
    [CodedUITest]
    public class OpenIndexTests
    {
        static UiApplication _app;

        [ClassInitialize]
        public static void Initialise(TestContext context)
        {
            _app = new UiApplication();
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            if (_app != null)
                _app.Dispose();
        }

        [TestMethod]
        public void CheckCanOpenIndex()
        {
            // Arrange
            var mainWindow = _app.MainWindow;
            var openIndex = mainWindow.OpenIndex;
            
            // Act
            openIndex.Expanded = true;
            Mouse.Click(openIndex.OpenIndexButton);

            // Assert
            // check views are added and that a few of the index info are available!
        }

    }
}

