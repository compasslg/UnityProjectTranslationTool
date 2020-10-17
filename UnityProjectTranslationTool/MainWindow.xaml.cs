using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using UnityProjectTranslationTool.TranslationProject;
using UnityProjectTranslationTool.FileData;
namespace UnityProjectTranslationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //UpdateSize();
        }

        public void UpdateSize()
        {
            double width = TextEntryGrid.Width - column_index.ActualWidth - column_line.ActualWidth;
            column_text.Width = width / 2;
            column_translation.Width = width / 2;
        }

        private void OpenUnityProject(object sender, RoutedEventArgs e)
        {

            string path;

            // Get path with CommonFileDialog if supported
            if (CommonFileDialog.IsPlatformSupported)
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.Title = "Open Unity Script Directory";
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() != CommonFileDialogResult.Ok)
                    return;
                path = System.IO.Path.GetFullPath(dialog.FileName);
            }
            // use FolderBrowserDialog otherwise
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK)
                    return;
                path = dialog.SelectedPath;
            }

            // TODO: Open InputProjWindow

            //ProjectManager.PrepareProgress();
            //LoadingWindow loadingWindow = new LoadingWindow();
            //loadingWindow.Show();
            ProjectManager.OpenUnityProject(path, path);
            //ProjectManager.EndProgress();
            Files.ItemsSource = ProjectManager.projectData.files;
        }

        private void OpenTranslationProject(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();
        }

        private void Files_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SingleFileData selected = (SingleFileData)Files.SelectedItem;
            if (selected == null) return;
            TextEntryGrid.ItemsSource = selected.texts;
        }
    }
}
