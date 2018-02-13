using ArtDatabanken.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OptiCountExporter
{
    public class DyntaxaMatchingDialogViewModel : DialogViewModel, IDialogRequestClose
    {
        #region Private Properties

        private ICommand okCommand;
        public ICommand CancelCommand { get; }
        private bool canExecute = false;
        private Plankton selectedPlankton = null;

        #endregion

        #region Public Properties

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public ICommand SearchCommand { get; set; }
        public ObservableCollection<Plankton> Result { get; set; }
        public string SearchText { get; set; }
        public Plankton SelectedPlankton
        {
            get
            {
                return this.selectedPlankton;
            }
            set
            {
                if (value == null)
                {
                    CanExecute = false;
                }
                else
                {
                    CanExecute = true;
                }
                this.selectedPlankton = value;
            }
        }

        #endregion

        public DyntaxaMatchingDialogViewModel(string message, string searchText, TaxonNameList search) : base(message)
        {
            SearchText = searchText;
            Result = new ObservableCollection<Plankton>();
            this.UpdateCollection(search);
            this.SearchCommand = new RelayCommand(Search);
            this.OkCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)), param => this.canExecute);
            this.CancelCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return this.okCommand;
            }
            set
            {
                this.okCommand = value;
            }
        }

        private void Search(object obj)
        {
            TaxonNameList newSearch = this.DyntaxaSession.searchTaxa(SearchText);
            int numOfResults = newSearch.Count;
            this.Message = $"Found {numOfResults} matches when searching for species: {SearchText}";
            this.UpdateCollection(newSearch);
        }

        private void UpdateCollection(TaxonNameList search)
        {
            Result.Clear();
            List<Plankton> resultList = this.DyntaxaSession.MakePlanktonList(search);
            foreach (Plankton plankton in resultList)
            {
                Result.Add(plankton);
            }
        }

    }
}
