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
            OnCurProjectStateChange(ProjectManager.projectData != null);
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
            ProjectManager.OpenUnityProject("Untitled", path);
            //ProjectManager.EndProgress();
            Files.ItemsSource = ProjectManager.projectData.files;
            OnCurProjectStateChange(ProjectManager.projectData != null);
        }

        private void OpenTranslationProject(object sender, RoutedEventArgs e)
        {
            //LoadingWindow loadingWindow = new LoadingWindow();
            //loadingWindow.Show();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Unity Translation Project Files (*.utp) | *.utp";
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectManager.OpenTranslationProject(openFileDialog.FileName);
                Files.ItemsSource = ProjectManager.projectData.files;
            }
            OnCurProjectStateChange(ProjectManager.projectData != null);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(ProjectManager.curProjPath == null)
            {
                SaveAs(sender, e);
                return;
            }
            ProjectManager.SaveTranslationProject(ProjectManager.curProjPath);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Unity Translation Project Files (*.utp) | *.utp";
            saveFileDialog.DefaultExt = "utp";
            
            if(saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectManager.curProjPath = saveFileDialog.FileName;
                ProjectManager.SaveTranslationProject(saveFileDialog.FileName);
            }
        }

        private void OnSelectedFileChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SingleFileData selected = (SingleFileData)Files.SelectedItem;
            if (selected == null) return;
            TextEntryGrid.ItemsSource = selected.texts;
        }

        private void OnCurProjectStateChange(bool enable)
        {
            Menu_Save.IsEnabled = enable;
            Menu_SaveAs.IsEnabled = enable;
            Menu_Apply.IsEnabled = enable;
        }
    }
}
