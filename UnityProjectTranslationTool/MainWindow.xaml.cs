using System.Windows;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using UnityProjectTranslationTool.TranslationProject;
using UnityProjectTranslationTool.FileData;
using UnityProjectTranslationTool.DataElement;
using UnityProjectTranslationTool.AssemblyData;
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
            double width = TextEntryGrid.Width;
            column_text.Width = width / 2;
            column_translation.Width = width / 2;
        }
        private void Event_OpenAssembly(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C# dll (*.dll)|*.dll|C# Executable (*.exe)|*.exe|All File (*.*)|*.*";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = System.IO.Path.GetFileName(openFileDialog.FileName);
                ProjectManager.OpenUnityGameAssembly(filename, openFileDialog.FileName);
                Files.ItemsSource = ProjectManager.projectData.children;
            }
            OnCurProjectStateChange(ProjectManager.projectData != null);

        }
        private void Event_OpenSourceCodeFolder(object sender, RoutedEventArgs e)
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
            Files.ItemsSource = ProjectManager.projectData.children;
            OnCurProjectStateChange(ProjectManager.projectData != null);
        }

        private void Event_OpenTranslationProject(object sender, RoutedEventArgs e)
        {
            //LoadingWindow loadingWindow = new LoadingWindow();
            //loadingWindow.Show();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Unity Translation Project Files (*.utp) | *.utp";
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectManager.OpenTranslationProject(openFileDialog.FileName);
                Files.ItemsSource = ProjectManager.projectData.children;
            }
            OnCurProjectStateChange(ProjectManager.projectData != null);
        }

        private void Event_Save(object sender, RoutedEventArgs e)
        {
            if(ProjectManager.curProjPath == null)
            {
                Event_SaveAs(sender, e);
                return;
            }
            ProjectManager.SaveTranslationProject(ProjectManager.curProjPath);
        }

        private void Event_SaveAs(object sender, RoutedEventArgs e)
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
        private void Event_ApplyTranslation(object sender, RoutedEventArgs e)
        {
            ProjectData proj = ProjectManager.projectData;
            string curProjPath = proj.path;
            ProjectManager.ApplyTranslation(curProjPath, proj);
        }

        private void OnSelectedFileChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Files.SelectedItem is SingleFileData singleFileData)
            {
                TextEntryGrid.ItemsSource = singleFileData.texts;
                System.Diagnostics.Debug.WriteLine(singleFileData.texts.Count);
            }
            else if (Files.SelectedItem is AssemblyMethodData assemblyMethodData)
            {
                TextEntryGrid.ItemsSource = assemblyMethodData.texts;
                System.Diagnostics.Debug.WriteLine(assemblyMethodData.texts.Count);
            }
        }

        private void OnCurProjectStateChange(bool enable)
        {
            Menu_Save.IsEnabled = enable;
            Menu_SaveAs.IsEnabled = enable;
            Menu_Apply.IsEnabled = enable;
        }


    }
}
