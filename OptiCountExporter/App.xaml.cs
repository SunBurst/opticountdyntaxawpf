using System.Windows;

namespace OptiCountExporter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<DialogViewModel, DialogWindow>();
            dialogService.Register<DyntaxaMatchingDialogViewModel, DyntaxaSearchDialogWindow>();

            var viewModel = new MainViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();
        }
    }
}
