using System.Windows;
using Luke.Net.Features.OpenIndex;

namespace Luke.Net
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Oooooo, now this sucks big time
        // ToDo: have to find a better way soon
        public static OpenIndexModel OpenIndexModel { get; set; }

        protected override void  OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
