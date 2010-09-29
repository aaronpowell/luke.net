using System.Windows;
using Luke.Net.Contollers;
using Magellan;
using Magellan.Framework;
using Luke.Net.Models.Binders;
using Luke.Net.Models;

namespace Luke.Net
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var controllerFactory = new ControllerFactory();
            controllerFactory.Register("LuceneIndex", () => new LuceneIndexController());
            controllerFactory.Register("Popup", () => new PopupController());

            // Setup routes
            var routes = new ControllerRouteCatalog(controllerFactory);
            routes.MapRoute("{controller}/{action}");

            // Show the main window
            var main = new MainWindow();

            var navigation = new NavigatorFactory(routes);
            var navigator = navigation.CreateNavigator(main.MainFrame);

            navigator.Navigate("magellan://Popup/OpenIndex");

            //model binders
            routes.ModelBinders.Add(typeof(ActiveIndexModel), new ActiveIndexModelBinder());

            main.Show();

            base.OnStartup(e);
        }
    }
}
