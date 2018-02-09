namespace OptiCountExporter
{
    using System;
    using System.Windows.Input;

    public class DialogViewModel : BaseViewModel, IDialogRequestClose
    {
        protected string message;

        public DialogViewModel(string message)
        {
            this.message = message;
            OkCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    this.NotifyPropertyChanged("Message");
                }
            }
        }

    }
}