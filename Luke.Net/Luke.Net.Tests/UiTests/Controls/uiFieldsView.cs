using Luke.Net.Tests.UiTests.Helpers;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Luke.Net.Tests.UiTests.Controls
{
    internal class UiFieldsView : WpfControl
    {
        private WpfTable _fieldsGrid;

        public UiFieldsView(WpfControl parent) : base(parent)
        {
            this.Where().Name.Is("fieldsView");
        }

        public WpfTable FieldsGrid
        {
            get
            {
                return _fieldsGrid ?? (_fieldsGrid = this.GetChild<WpfTable>());
            }
        }
    }
}