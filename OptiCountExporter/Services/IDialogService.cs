namespace OptiCountExporter
{
    public interface IDialogService
    {
        void Register<TViewModel, Tview>() where TViewModel : IDialogRequestClose
            where Tview : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;
    }
}
