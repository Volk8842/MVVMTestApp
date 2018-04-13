using System.Windows;

using Repositories;

using TestApp.ViewModels;

namespace TestApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void MainWindowOnLoaded(object sender, RoutedEventArgs e)
        {
            var personRepository = new PersonRepository();
            var mainViewModel = new MainWindowViewModel(personRepository);

            this.DataContext = mainViewModel;
        }
    }
}
