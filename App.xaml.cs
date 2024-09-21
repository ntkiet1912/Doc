using System.CodeDom;
using System.Configuration;
using System.Data;
using System.Windows;
using docghifile.ViewModel;

namespace docghifile
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindow view = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            view.DataContext = viewModel;
            view.Show();
        }
    }

}
