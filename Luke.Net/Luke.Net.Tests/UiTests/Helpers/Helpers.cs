using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Luke.Net.Tests.UiTests.Helpers
{
    public static class Helpers
    {
        public static T FindChild<T>(
            this WpfControl parent, 
            string propertyName,
            string propertyValue) where T : WpfControl
        {
            var control = FindChild<T>(parent);
            control.SearchProperties[propertyName] = propertyValue;
            return control;
        }

        public static T FindChildByName<T>(this WpfControl parent, string controlName) where T : WpfControl
        {
            return (T)FindChild<T>(parent).Where().Name.Equals(controlName);
        }

        public static T FindChild<T>(this WpfControl parent) where T : WpfControl
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { parent });
        }

        public static Where Where(this WpfControl control)
        {
            return new Where(control);
        }
    }

    public class Where
    {
        private readonly WpfControl _control;
        private Name _name;

        public Where(WpfControl control)
        {
            _control = control;
        }

        public Name Name
        {
            get
            {
                return _name ?? (_name = new Name(_control));
            }
        }
    }

    public class Name
    {
        private readonly WpfControl _control;

        public Name(WpfControl control)
        {
            _control = control;
        }

        public WpfControl Equals(string controlName)
        {
            _control.SearchProperties[WpfControl.PropertyNames.AutomationId] = controlName;
            return _control;
        }
    }
}
