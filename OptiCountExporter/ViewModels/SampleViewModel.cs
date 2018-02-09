using ArtDatabanken;
using ArtDatabanken.Data;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows.Input;

namespace OptiCountExporter
{
    /// <summary>
    /// The view model for the application's main sample view
    /// </summary>
    public class SampleViewModel : BaseViewModel
    {

        #region Private Properties

        private bool phytoSampleIsChecked = true;
        private bool zooSampleIsChecked = false;

        private readonly IDialogService dialogService;

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of all samples
        /// </summary>
        public ObservableCollection<Sample> Samples { get; set; }

        /// <summary>
        /// Selected sample
        /// </summary>
        public Sample SelectedSample { get; set; }

        public bool PhytoSampleIsChecked
        {
            get { return this.phytoSampleIsChecked; }
            set {
                this.phytoSampleIsChecked = value;
                this.NotifyPropertyChanged("phytoSampleIsChecked");
            }
        }

        public bool ZooSampleIsChecked
        {
            get { return this.zooSampleIsChecked; }
            set {
                this.zooSampleIsChecked = value;
                this.NotifyPropertyChanged("zooSampleIsChecked");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SampleViewModel(IDialogService dialogService)
        {
            this.Samples = new ObservableCollection<Sample>();
            this.dialogService = dialogService;
        }

        private Plankton GetUserSelectedPlankton(string taxonSearch, TaxonNameList matchingResult)
        {
            var viewModel = new DyntaxaMatchingDialogViewModel($"Found {matchingResult.Count} matches when searching for species: {taxonSearch}", taxonSearch, matchingResult);
            Plankton plankton = null;
            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    plankton = viewModel.SelectedPlankton;
                }
                else
                {
                    // Cancelled
                }
            }
            return plankton;
        }

        private Plankton IdentifyPlankton(string searchText)
        {
            Plankton plankton = new Plankton();
            TaxonNameList result = this.DyntaxaSession.searchTaxa(searchText);
            if (result.IsNotEmpty())
            {
                if (result.Count == 1)
                {
                    ITaxonTreeNode taxonTreeNode = result[0].Taxon.GetParentTaxonTree(this.DyntaxaSession.getUserContext(), true);
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
                }
                else
                {
                    plankton = this.GetUserSelectedPlankton(searchText, result);
                }
            }
            else
            {
                plankton = this.GetUserSelectedPlankton(searchText, result);
            }
            return plankton;

        }

        public void AddFiles(string[] files)
        {
            foreach (string filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string origin = "Unknown";
                DateTime date = new DateTime();

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {

                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx)
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {

                        // Use the reader methods
                        do
                        {
                            int rowNum = 0;
                            int originRow = 1;
                            int dateRow = 2;
                            while (reader.Read() & rowNum < 3)
                            {
                                if (rowNum == originRow)
                                {
                                    origin = reader.GetString(1);
                                }

                                if (rowNum == dateRow)
                                {
                                    var fieldTypeFullName = reader.GetFieldType(1).FullName;
                                    if (fieldTypeFullName == typeof(DateTime).FullName)
                                    {
                                        date = reader.GetDateTime(1);
                                    }
                                    else
                                    {
                                        if (fieldTypeFullName == typeof(Double).FullName)
                                        {
                                            string dateString = reader.GetDouble(1).ToString();
                                            if (dateString.Length == 6)
                                            {
                                                string pattern = "yyMMdd";
                                                DateTime.TryParseExact(dateString, pattern, null,
                                                    DateTimeStyles.None, out date);
                                                //date = Convert.ToDateTime(dateString);
                                            }
                                            else if (dateString.Length == 8)
                                            {
                                                string pattern = "yyyyMMdd";
                                                DateTime.TryParseExact(dateString, pattern, null,
                                                    DateTimeStyles.None, out date);
                                            }
                                            else if (dateString.Length == 10)
                                            {
                                                string pattern = "yyyy-MM-dd";
                                                DateTime.TryParseExact(dateString, pattern, null,
                                                    DateTimeStyles.None, out date);
                                            }
                                            else
                                            {
                                                throw new InvalidDataException("Found date of type 'Double' but received an unexpected length. Valid formats include yyMMdd, yyyyMMdd and yyyy-MM-dd");
                                            }
                                        }
                                        else if (fieldTypeFullName == typeof(String).FullName)
                                        {
                                            string dateString = reader.GetString(1);
                                            if (dateString.Length == 6)
                                            {
                                                string pattern = "yyMMdd";
                                                DateTime.TryParseExact(dateString, pattern, null,
                                                    DateTimeStyles.None, out date);
                                                //date = Convert.ToDateTime(dateString);
                                            }
                                            else if (dateString.Length == 8)
                                            {
                                                string pattern = "yyyyMMdd";
                                                DateTime.TryParseExact(dateString, pattern, null,
                                                    DateTimeStyles.None, out date);
                                            }
                                            else if (dateString.Length == 10)
                                            {
                                                string pattern = "yyyy-MM-dd";
                                                DateTime.TryParseExact(dateString, pattern, null,
                                                    DateTimeStyles.None, out date);
                                            }
                                            else
                                            {
                                                throw new InvalidDataException("Found date of type 'String' but received an unexpected length. Valid formats include yyMMdd, yyyyMMdd and yyyy-MM-dd");
                                            }
                                        }
                                        else
                                        {
                                            throw new InvalidDataException("Invalid date type. Supported types include 'DateTime', 'Double' and 'String' following the format yyMMdd, yyyyMMdd or yyyy-MM-dd");
                                        }
                                    }
                                }
                                rowNum++;
                            }
                        } while (reader.NextResult());
                    }
                }

                this.Samples.Add(new PhytoSample() { Origin = origin, Date = date, FileName = fileName, FilePath = filePath });
            }
        }

        public void MoveUp()
        {
            int selectedIndex = this.Samples.IndexOf(this.SelectedSample);

            if (selectedIndex > 0)
            {
                var itemToMoveUp = this.Samples[selectedIndex];
                this.Samples.RemoveAt(selectedIndex);
                this.Samples.Insert(selectedIndex - 1, itemToMoveUp);
                //this.FileList.SelectedIndex = selectedIndex - 1;
            }
        }

        public void MoveDown()
        {
            int selectedIndex = this.Samples.IndexOf(this.SelectedSample);
            if (selectedIndex < this.Samples.Count - 1 & selectedIndex != -1)
            {
                this.Samples.Insert(selectedIndex + 2, this.Samples[selectedIndex]);
                this.Samples.RemoveAt(selectedIndex);
                //this.FileList.SelectedIndex = selectedIndex + 1;

            }
        }

        public void RemoveItem()
        {
            if (this.SelectedSample != null)
                this.Samples.Remove(this.SelectedSample as Sample);
        }

        public void ExportSamples()
        {

            List<string> flags = new List<string> { "cf", "sp", "spp" };
            List<string> comments = new List<string> {
                "avl", "rund", "enstaka", "oval", "runda", "koloni",
                "i", "gele", "ovala", "filament", "gelé", "med", "flagell",
                "stjärnformad", "bandkoloni", "klyftform", "långsmal",
                "gissel"
            };
            List<string> ignores = new List<string> { "um" };
            if (this.PhytoSampleIsChecked == true)
            {
                List<PhytoSample> exportedSamples = new List<PhytoSample>();

                foreach (var sample in this.Samples)
                {
                    PhytoSample phytoSample = new PhytoSample();
                    using (var stream = File.Open(sample.FilePath, FileMode.Open, FileAccess.Read))
                    {

                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                                int rowNum = 0;
                                while (reader.Read())
                                {
                                    // Start of species declarations
                                    if (rowNum >= 13)
                                    {
                                        // End of species declarations
                                        if (reader.GetString(0) == null)
                                        {
                                            break;
                                        }
                                        // Species was counted
                                        if (reader.GetDouble(9) > 0)
                                        {
                                            string taxonSpecies = reader.GetString(0);
                                            Double concentration = reader.GetDouble(12);
                                            Double biovolume = reader.GetDouble(13);
                                            Double freshweight = reader.GetDouble(14);
                                            PhytoPlankton phyto = new PhytoPlankton(taxonSpecies, concentration, biovolume, freshweight);
                                            phyto.CleanTaxonSpecies(flags, comments, ignores);
                                            string newTaxonSpecies = phyto.TaxonSpecies;
                                            PhytoPlankton newPhyto = (PhytoPlankton) IdentifyPlankton(newTaxonSpecies);
                                            phytoSample.AddPhyto(phyto);
                                        }
                                    }

                                    rowNum++;
                                }
                            } while (reader.NextResult());
                        }
                    }
                    exportedSamples.Add(phytoSample);
                }
                foreach (var sample in exportedSamples)
                {
                    sample.PrintSamples();
                }

            }
            else if (this.ZooSampleIsChecked == true)
            {
                List<ZooSample> exportedSample = new List<ZooSample>();
            }

        }

        #endregion
    }
}
