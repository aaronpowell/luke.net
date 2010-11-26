using System.Reactive.Contrib;
using System.Windows;
using Luke.Net.Features;
using Luke.Net.Features.OpenIndex;
using Luke.Net.Infrastructure;
using Microsoft.Practices.Prism.Events;
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
            Container.RegisterType<IServiceFactory, ServiceFactory>();

            Container.RegisterInstance<IIndexController>(Container.Resolve<IndexController>());

            Container.Resolve<OpenIndexModule>().Initialize();
            Container.Resolve<LukeModule>().Initialize();
        }
    }
}
