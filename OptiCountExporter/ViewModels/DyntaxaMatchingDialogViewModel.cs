using ArtDatabanken.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OptiCountExporter
{
    public class DyntaxaMatchingDialogViewModel : DialogViewModel
    {
        #region Public Properies

        public ICommand SearchCommand { get; set; }
        public ObservableCollection<Plankton> Result { get; set; }
        public Plankton SelectedPlankton { get; set; }
        public string SearchText { get; set; }

        #endregion

        public DyntaxaMatchingDialogViewModel(string message, string searchText, TaxonNameList search) : base(message)
        {
            SearchText = searchText;
            Result = new ObservableCollection<Plankton>();
            this.UpdateCollection(search);
            this.SearchCommand = new RelayCommand(Search);
        }

        private void Search()
        {
            TaxonNameList newSearch = this.DyntaxaSession.searchTaxa(SearchText);
            int numOfResults = newSearch.Count;
            this.Message = $"Found {numOfResults} matches when searching for species: {SearchText}";
            this.UpdateCollection(newSearch);
        }

        private void UpdateCollection(TaxonNameList search)
        {
            Result.Clear();
            foreach (TaxonName match in search)
            {
                Plankton plankton = new Plankton();
                ITaxonTreeNode taxonTreeNode = match.Taxon.GetParentTaxonTree(this.DyntaxaSession.getUserContext(), true);
                while (taxonTreeNode != null)
                {
                    string categoryName = taxonTreeNode.Taxon.Category.Name;
                    switch (categoryName)
                    {
                        case "Species":
                            plankton.TaxonSpecies = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        case "Genus":
                            plankton.TaxonGenus = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        case "Class":
                            plankton.TaxonClass = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        case "Family":
                            plankton.TaxonFamily = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        case "Order":
                            plankton.TaxonOrder = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        case "Phylum":
                            plankton.TaxonPhylum = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        case "Organism group":
                            plankton.TaxonOrganismGroup = taxonTreeNode.Taxon.ScientificName;
                            if (!(plankton.TaxonDyntaxaIDIsInitialized))
                                plankton.TaxonDyntaxaID = taxonTreeNode.Taxon.Id;
                            break;
                        default: break;
                    }
                    if (taxonTreeNode.Parents == null)
                    {
                        break;
                    }
                    taxonTreeNode = taxonTreeNode.Parents[0];
                }
                Result.Add(plankton);
            }
        }

    }
}
