using System.Windows;

using Models;

namespace TestApp
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await PersonDbInitializer.Initialize();

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
