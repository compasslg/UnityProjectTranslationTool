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
using System.Windows.Shapes;
using System.Threading;
using UnityProjectTranslationTool.TranslationProject;
namespace UnityProjectTranslationTool
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
            StartLoading();
        }

        public void StartLoading()
        {
            Thread.Sleep(1000);
            ProgressText.Text = "";
            Task.Run(() =>
            {
                while (ProjectManager.onProgress)
                {
                    if (ProjectManager.progressQueue.Count != 0)
                    {
                        string text = ProjectManager.progressQueue.Dequeue();
                        ProgressText.AppendText(text);
                    }
                    Thread.Sleep(50);
                }
            });
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
