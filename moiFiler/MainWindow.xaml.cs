using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace moiFiler
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            var openDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            this.Loaded += (sender, e) =>
            {
                this.FileSystems.ItemsSource =
                    openDirectory.EnumerateFileSystemInfos()
                    .Select(fileSystemInfo => fileSystemInfo.Name);
            };
        }
    }
}
