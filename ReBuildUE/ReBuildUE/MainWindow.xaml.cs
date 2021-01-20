using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using System.Windows.Diagnostics;
using Microsoft.Win32;


namespace ReBuildUE
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool usablePath = false;

        string[] directoryToDelete = { ".vs", "Binaries", "DerivedDataCache", "Intermediate", "Saved" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickResearchButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _dialog = new OpenFileDialog();

            _dialog.Filter = "UE Project (*.uproject)|*.uproject";

            _dialog.FileOk += (s, c) => GetFile(s, c, _dialog.FileName);

            _dialog.ShowDialog();
        }

        private void GetFile(object sender, System.ComponentModel.CancelEventArgs e, string _text)
        {
            PathBox.Text = _text;
            PathBox.Opacity = 1;
            usablePath = true;
        }

        private void OnClickCancelButton(object sender, RoutedEventArgs e)
        {
            PathBox.Text = "Path Project...";
            PathBox.Opacity = 0.5f;
            usablePath = false;
        }

        private void OnClickConfirmButton(object sender, RoutedEventArgs e)
        {
            if (!usablePath) return;

            string _currentPathDirectory = Directory.GetParent(PathBox.Text).FullName;

            DeleteDirectories(_currentPathDirectory);

            DeleteFiles(_currentPathDirectory);

            System.Diagnostics.Process.Start(PathBox.Text);

            Application.Current.MainWindow.Close();
        }

        void DeleteDirectories(string _path)
        {
            for (int i = 0; i < directoryToDelete.Length; i++)
            {
                string _currentPath = System.IO.Path.Combine(_path, directoryToDelete[i]);

                if (Directory.Exists(_currentPath))
                {
                    Directory.Delete(_currentPath, true);
                }
            }
        }

        void DeleteFiles(string _path)
        {
            string[] _filesSln = Directory.GetFiles(_path, "*.sln");
            
            for (int i = 0; i < _filesSln.Length; i++)
            {
                if (File.Exists(_filesSln[i]))
                    File.Delete(_filesSln[i]);
            }
        }

    }
}
