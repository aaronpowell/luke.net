using Luke.Net.Tests.UiTests.Controls;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Luke.Net.Tests.UiTests.Windows
{
    class UiMainWindow : WpfWindow
    {
        private UiOpenIndex _openIndex;
        private UiOverviewTab _overviewTab;

        public UiMainWindow(UITestControl parent) : base(parent)
        {
        }

        public UiOpenIndex OpenIndex
        {
            get
            {
                return _openIndex ?? (_openIndex = new UiOpenIndex(this));
            }
        }

        public UiOverviewTab OverviewTab
        {
            get
            {
                return _overviewTab ?? (_overviewTab = new UiOverviewTab(this));
            }
        }
    }
}
