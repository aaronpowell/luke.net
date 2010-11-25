using System;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Luke.Net.Tests.UiTests.Helpers;

namespace Luke.Net.Tests.UiTests.Controls
{
    internal class UiOverviewTab : WpfTabPage
    {
        private UiFieldsView _uiFieldsView;

        public UiOverviewTab(WpfControl parent)
            : base(parent)
        {
            this.Where().Name.Is("Overview");
        }

        public UiFieldsView FieldsView
        {
            get
            {
                return _uiFieldsView ?? (_uiFieldsView = new UiFieldsView(this));
            }
        }
    }
}