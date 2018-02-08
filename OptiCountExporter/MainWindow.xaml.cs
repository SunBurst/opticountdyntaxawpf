using System;
using System.Windows;
using System.Windows.Forms;

namespace OptiCountExporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        # region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = new MainViewModel();
        }

        #endregion

        private void MainGrid_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                ((MainViewModel) this.DataContext).sampleViewModel.AddFiles(files);
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
                string[] files = openFileDialog.FileNames;
                ((MainViewModel) this.DataContext).sampleViewModel.AddFiles(files);
            }
        }

        private void exportSamples_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel) this.DataContext).sampleViewModel.ExportSamples();
        }

        private void moveUp_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel) this.DataContext).sampleViewModel.MoveUp();
        }

        private void moveDown_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel) this.DataContext).sampleViewModel.MoveDown();
        }

        private void removeFile_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel) this.DataContext).sampleViewModel.RemoveItem();
        }

    }
}