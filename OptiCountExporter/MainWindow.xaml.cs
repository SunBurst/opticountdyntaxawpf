using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ExcelDataReader;
using System.Globalization;

namespace OptiCountExporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static ObservableCollection<Sample> items = new ObservableCollection<Sample>();

        public MainWindow()
        {
            InitializeComponent();
            bool connected = GlobalVariables.setupDyntaxaService();
            if (connected)
            {
                connectionStatusIcon.Fill = System.Windows.Media.Brushes.Green;
                connectionStatusText.Content = "Connected to Dyntaxa";
                
            }
        }

        private void MainGrid_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                foreach(string filePath in files)
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
                                            else {
                                                throw new InvalidDataException("Invalid date type. Supported types include 'DateTime', 'Double' and 'String' following the format yyMMdd, yyyyMMdd or yyyy-MM-dd");
                                            }
                                        }
                                    }
                                    rowNum++;
                                }
                            } while (reader.NextResult());
                        }
                    }

                    items.Add(new Sample() { Site = origin, Date = date, FileName = fileName, FilePath = filePath });
                }
                FileList.ItemsSource = items;
            }
        }

        private void addFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 1997-2003 files (*.xls)|*.xls";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
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
                    items.Add(new Sample() { Site = origin, Date = date, FileName = fileName, FilePath = filePath });
                }
            }
            FileList.ItemsSource = items;
        }

        private void moveUp_Click(object sender, RoutedEventArgs e)
        {
            MoveUp(FileList);
        }

        private void moveDown_Click(object sender, RoutedEventArgs e)
        {
            MoveDown(FileList);
        }

        private void removeFile_Click(object sender, RoutedEventArgs e)
        {
            RemoveItem(FileList);
        }

        private void MoveUp(System.Windows.Controls.ListBox myListBox)
        {
            int selectedIndex = this.FileList.SelectedIndex;

            if (selectedIndex > 0)
            {
                var itemToMoveUp = items[selectedIndex];
                items.RemoveAt(selectedIndex);
                items.Insert(selectedIndex - 1, itemToMoveUp);
                this.FileList.SelectedIndex = selectedIndex - 1;
            }
        }

        private void MoveDown(System.Windows.Controls.ListBox myListBox)
        {
            int selectedIndex = this.FileList.SelectedIndex;
            if (selectedIndex < items.Count - 1 & selectedIndex != -1)
            {
                items.Insert(selectedIndex + 2, items[selectedIndex]);
                items.RemoveAt(selectedIndex);
                this.FileList.SelectedIndex = selectedIndex + 1;

            }
        }

        private void RemoveItem(System.Windows.Controls.ListBox myListBox)
        {
            if (myListBox.SelectedItem != null)
                items.Remove(myListBox.SelectedItem as Sample);
        }

        private void ExportSamples(object sender, RoutedEventArgs e)
        {
            foreach (var sample in items)
            {
                Console.WriteLine(sample.FileName);
                Console.WriteLine(sample.Site);
                Console.WriteLine(sample.Date);
                Console.WriteLine(sample.FilePath);
            }
        }
    }
}
