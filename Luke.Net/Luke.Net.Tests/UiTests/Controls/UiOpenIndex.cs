using Luke.Net.Tests.UiTests.Helpers;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Luke.Net.Tests.UiTests.Controls
{
    public class UiOpenIndex : WpfExpander
    {
        public UiOpenIndex(UITestControl parent) : base(parent)
        {
            this.Where().Name.Is("loadIndexExpander");
        }

        public WpfButton OpenIndexButton
        {
             get
             {
                 return this.GetChild<WpfButton>().Where().Text.Is("Open Index");
             }
        }
    }
}