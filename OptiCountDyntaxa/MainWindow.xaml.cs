using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace OptiCountDyntaxa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
            bool connected = GlobalVariables.setupDyntaxaService();
            if (connected)
            {
                //connectionStatus.Fill = Brushes.Green;
            }
        }

        private void MainGrid_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                foreach(string fileName in files)
                {
                    FileList.Items.Add(fileName);
                }
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
                foreach (string filename in openFileDialog.FileNames)
                {
                    FileList.Items.Add(System.IO.Path.GetFileName(filename));
                }
            }
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
            int selectedIndex = myListBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                myListBox.Items.Insert(selectedIndex - 1, myListBox.Items[selectedIndex]);
                myListBox.Items.RemoveAt(selectedIndex + 1);
                myListBox.SelectedIndex = selectedIndex - 1;
            }
        }

        private void MoveDown(System.Windows.Controls.ListBox myListBox)
        {
            int selectedIndex = myListBox.SelectedIndex;
            if (selectedIndex < myListBox.Items.Count - 1 & selectedIndex != -1)
            {
                myListBox.Items.Insert(selectedIndex + 2, myListBox.Items[selectedIndex]);
                myListBox.Items.RemoveAt(selectedIndex);
                myListBox.SelectedIndex = selectedIndex + 1;

            }
        }

        private void RemoveItem(System.Windows.Controls.ListBox myListBox)
        {
            int selectedIndex = myListBox.SelectedIndex;
            if (selectedIndex < myListBox.Items.Count & selectedIndex != -1)
            {
                myListBox.Items.RemoveAt(selectedIndex);
                if (selectedIndex == myListBox.Items.Count)
                {
                    myListBox.SelectedIndex = myListBox.Items.Count - 1;
                }
                else
                {
                    myListBox.SelectedIndex = selectedIndex;
                }
            }
        }
    }
}
