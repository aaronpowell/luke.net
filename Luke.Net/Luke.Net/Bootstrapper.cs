using System.Reflection;
using System.Windows;
using Luke.Net.Features;
using Luke.Net.Features.Documents;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Features.Overview;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace Luke.Net
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void InitializeModules()
        {
            var aggregator = new EventAggregator();
            Container.RegisterInstance<IEventAggregator>(aggregator);

            Container.RegisterInstance<IIndexController>(Container.Resolve<IndexController>());

            Container.Resolve<OpenIndexModule>().Initialize();
        }
    }
}
