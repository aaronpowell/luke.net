using Luke.Net.Tests.UiTests.Helpers;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Luke.Net.Tests.UiTests.Controls
{
    public class UiOpenIndex : WpfExpander
    {
        public UiOpenIndex(UITestControl parent) : base(parent)
        {
            this.Where().Name.Equals("loadIndexExpander");
        }

        public WpfButton OpenIndexButton
        {
             get
             {
                 return this.FindChild<WpfButton>();
             }
        }
    }
}