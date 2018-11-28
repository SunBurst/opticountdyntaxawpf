using ArtDatabanken;
using ArtDatabanken.Data;
using ExcelDataReader;
using OfficeOpenXml;
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
                    plankton = new Plankton(taxonSearch);
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
                    List<Plankton> resultList = this.DyntaxaSession.MakePlanktonList(result);

                    try
                    {
                        plankton = resultList[0];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e);
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

                this.Samples.Add(new Sample() { Origin = origin, Date = date, FileName = fileName, FilePath = filePath });
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
                "gissel", "copepodit", "adult", "naupliuslarv", "hanne",
                "bentisk"
            };
            List<string> ignores = new List<string> { "um" };

            List<Sample> exportedSamples = new List<Sample>();

            foreach (var inputSample in this.Samples)
            {
                using (var stream = File.Open(inputSample.FilePath, FileMode.Open, FileAccess.Read))
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
                                        Plankton plankton = new Plankton(taxonSpecies, concentration, biovolume, freshweight);
                                        plankton.CleanTaxonSpecies(flags, comments, ignores);
                                        Plankton newPlankton = this.IdentifyPlankton(plankton.TaxonSpecies);
                                        newPlankton.TaxonConcentration = plankton.TaxonConcentration;
                                        newPlankton.TaxonBiovolume = plankton.TaxonBiovolume;
                                        newPlankton.ConvertBiovolume(1e-9);
                                        newPlankton.TaxonFreshweight = plankton.TaxonFreshweight;
                                        newPlankton.TaxonMinSize = plankton.TaxonMinSize;
                                        newPlankton.TaxonMaxSize = plankton.TaxonMaxSize;
                                        newPlankton.TaxonSpeciesFlags = plankton.TaxonSpeciesFlags;
                                        newPlankton.TaxonSpeciesComments = plankton.TaxonSpeciesComments;
                                        newPlankton.TaxonSpeciesIgnores = plankton.TaxonSpeciesIgnores;
                                        newPlankton.TaxonSpeciesMinAndMax = plankton.TaxonSpeciesMinAndMax;
                                        inputSample.AddPlankton(newPlankton);
                                    }
                                }

                                rowNum++;
                            }
                        } while (reader.NextResult());
                    }

                    exportedSamples.Add(inputSample);
                }
                    
            }
            foreach (Sample sample in exportedSamples)
            {
                string FileDir = Path.GetDirectoryName(sample.FilePath);
                string FileName = Path.GetFileNameWithoutExtension(sample.FileName);
                string FileExt = Path.GetExtension(sample.FileName);
                string ExportedFullFileName = $"{FileName}_Exported{FileExt}";
                string ExportedFilePath = Path.Combine(FileDir, ExportedFullFileName);

                var newFile = new FileInfo(ExportedFilePath);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(ExportedFilePath);
                }
                using (var package = new ExcelPackage(newFile))
                {
                    // Add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Compilation");
                    //Add the headers

                    worksheet.Cells[1, 1].Value = "Origin";
                    worksheet.Cells[1, 2].Value = "Date";
                    worksheet.Cells[1, 3].Value = "Sample Number";
                    worksheet.Cells[1, 4].Value = "Phylum";
                    worksheet.Cells[1, 5].Value = "Class";
                    worksheet.Cells[1, 6].Value = "Order";
                    worksheet.Cells[1, 7].Value = "Species";
                    worksheet.Cells[1, 8].Value = "Flags";
                    if (this.phytoSampleIsChecked)
                    {
                        worksheet.Cells[1, 9].Value = "Min Size";
                        worksheet.Cells[1, 10].Value = "Max Size";
                        worksheet.Cells[1, 11].Value = "Comments";
                        worksheet.Cells[1, 12].Value = "Dyntaxa ID";
                        worksheet.Cells[1, 13].Value = "Concentration";
                        worksheet.Cells[1, 14].Value = "Biovolume";
                        worksheet.Cells[1, 15].Value = "Analysis Method";
                        worksheet.Cells[1, 16].Value = "Analysis Laboratory";
                    }
                    else if (this.zooSampleIsChecked)
                    {
                        worksheet.Cells[1, 9].Value = "Comments";
                        worksheet.Cells[1, 10].Value = "Dyntaxa ID";
                        worksheet.Cells[1, 11].Value = "Concentration";
                        worksheet.Cells[1, 12].Value = "Biovolume";
                        worksheet.Cells[1, 13].Value = "Analysis Method";
                        worksheet.Cells[1, 14].Value = "Analysis Laboratory";
                    }
                    
                    for (int i = 0; i < sample.exportedSamples.Count; i++)
                    {
                        Plankton plankton = sample.exportedSamples[i];
                        if (string.IsNullOrEmpty(plankton.TaxonSpecies))
                        {
                            if (string.IsNullOrEmpty(plankton.TaxonGenus))
                            {
                                if (string.IsNullOrEmpty(plankton.TaxonFamily))
                                {
                                    if (string.IsNullOrEmpty(plankton.TaxonOrder))
                                    {
                                        if (string.IsNullOrEmpty(plankton.TaxonClass))
                                        {
                                            if (string.IsNullOrEmpty(plankton.TaxonPhylum))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                plankton.TaxonSpecies = plankton.TaxonPhylum;
                                            }
                                        }
                                        else
                                        {
                                            plankton.TaxonSpecies = plankton.TaxonClass;
                                        }
                                    }
                                    else
                                    {
                                        plankton.TaxonSpecies = plankton.TaxonOrder;
                                    }
                                }
                                else
                                {
                                    plankton.TaxonSpecies = plankton.TaxonFamily;
                                }
                            }
                            else
                            {
                                plankton.TaxonSpecies = plankton.TaxonGenus;
                            }
                        }
                        int row = i + 2;

                        worksheet.Cells[row, 1].Value = sample.Origin;
                        worksheet.Cells[row, 2].Style.Numberformat.Format = "yyyy-mm-dd";
                        worksheet.Cells[row, 2].Formula = $"=DATE({sample.Date.Year},{sample.Date.Month},{sample.Date.Day})";
                        worksheet.Cells[row, 3].Value = sample.SampleNumber;
                        worksheet.Cells[row, 4].Value = plankton.TaxonPhylum;
                        worksheet.Cells[row, 5].Value = plankton.TaxonClass;
                        worksheet.Cells[row, 6].Value = plankton.TaxonOrder;
                        worksheet.Cells[row, 7].Value = plankton.TaxonSpecies;
                        worksheet.Cells[row, 8].Value = plankton.GetTaxonSpeciesFlagsAsString();
                        if (this.phytoSampleIsChecked)
                        {
                            if (plankton.TaxonMinSize == 0)
                            {
                                worksheet.Cells[row, 9].Value = "";
                            }
                            else
                            {
                                worksheet.Cells[row, 9].Value = plankton.TaxonMinSize;
                            }
                            if (plankton.TaxonMaxSize == 0)
                            {
                                worksheet.Cells[row, 10].Value = "";
                            }
                            else
                            {
                                worksheet.Cells[row, 10].Value = plankton.TaxonMaxSize;
                            }
                            worksheet.Cells[row, 11].Value = plankton.GetTaxonSpeciesCommentsAsString();
                            worksheet.Cells[row, 12].Value = plankton.TaxonDyntaxaID;
                            worksheet.Cells[row, 13].Value = plankton.TaxonConcentration;
                            worksheet.Cells[row, 14].Value = plankton.TaxonBiovolume;
                            worksheet.Cells[row, 15].Value = "SS-EN 15204:2006";
                            worksheet.Cells[row, 16].Value = "Erkenlaboratoriet";
                        }
                        else if (this.zooSampleIsChecked)
                        {
                            worksheet.Cells[row, 9].Value = plankton.GetTaxonSpeciesCommentsAsString();
                            worksheet.Cells[row, 10].Value = plankton.TaxonDyntaxaID;
                            worksheet.Cells[row, 11].Value = plankton.TaxonConcentration;
                            worksheet.Cells[row, 12].Value = plankton.TaxonBiovolume;
                            worksheet.Cells[row, 13].Value = "Naturvårdsverkets Djurplankton i sjöar 2003-05-27";
                            worksheet.Cells[row, 14].Value = "Erkenlaboratoriet";
                        }
                        
                    }

                    package.Save();
                }
            }

        }

        #endregion
    }
}
