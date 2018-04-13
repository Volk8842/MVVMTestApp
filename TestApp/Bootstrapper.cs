using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;
using TestApp.Views;

namespace TestApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
