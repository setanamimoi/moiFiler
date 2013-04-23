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

            this.SearchFilter.TextChanged += (sender, e) =>
            {
                this.FileSystems.Items.Filter = fileSystem =>
                {
                    var fileSystemName = fileSystem as string;

                    var searchExpression = this.SearchFilter.Text.ToUpper();

                    return fileSystemName.ToUpper().Contains(searchExpression);
                };
            };

            var openDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            var clipboardContext = Clipboard.GetText();
            if (string.IsNullOrEmpty(clipboardContext) == false)
            {
                openDirectory = new DirectoryInfo(clipboardContext);
            }

            this.Loaded += (sender, e) =>
            {
                this.FileSystems.ItemsSource =
                    openDirectory.EnumerateFileSystemInfos()
                    .Select(fileSystemInfo => fileSystemInfo.Name);
            };
        }
    }
}
