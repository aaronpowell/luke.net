using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace Luke.Net.Tests.UiTests.Helpers
{
    public static class Helpers
    {
        public static T GetChild<T>(this WpfControl parent) where T : WpfControl
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { parent });
        }

        public static Where<T> Where<T>(this T control) where T : WpfControl
        {
            return new Where<T>(control);
        }
    }

    public class Where<T> where T : WpfControl
    {
        private readonly T _control;
        private Name<T> _name;
        private Text<T> _text;

        public Where(T control)
        {
            _control = control;
        }

        public Name<T> Name
        {
            get
            {
                return _name ?? (_name = new Name<T>(_control));
            }
        }

        public Text<T> Text
        {
            get
            {
                return _text ?? (_text = new Text<T>(_control));
            }
        }
    }

    public class Text <T> where T:WpfControl
    {
        private readonly T _control;

        public Text(T control)
        {
            _control = control;
        }

        public T Is(string text)
        {
            _control.SearchProperties[WpfControl.PropertyNames.Name] = text;
            return _control;
        }
    }

    public class Name<T> where T:WpfControl
    {
        private readonly T _control;

        public Name(T control)
        {
            _control = control;
        }

        public T Is(string controlName)
        {
            _control.SearchProperties[WpfControl.PropertyNames.AutomationId] = controlName;
            return _control;
        }
    }
}
