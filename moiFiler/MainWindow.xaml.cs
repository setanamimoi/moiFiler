using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

            var clipboardContext = Clipboard.GetText();
            if (string.IsNullOrEmpty(clipboardContext) == false)
            {
                openDirectory = new DirectoryInfo(clipboardContext);
            }

            this.SearchFilter.TextChanged += (sender, e) =>
            {
                this.FileSystems.Items.Filter = fileSystem =>
                {
                    var fileSystemName = fileSystem as string;

                    var searchExpression = this.SearchFilter.Text.ToUpper();

                    return fileSystemName.ToUpper().Contains(searchExpression);
                };
            };

            this.FileSystems.PreviewKeyDown += (sender, e) =>
            {
                if (Keyboard.IsKeyDown(Key.Enter) == true)
                {
                    e.Handled = true;

                    if (this.FileSystems.SelectedIndex == -1)
                    {
                        return;
                    }

                    var fileSystemName = this.FileSystems.SelectedItem as string;

                    using (Process.Start(Path.Combine(openDirectory.FullName, fileSystemName)))
                    {
                    }
                }
            };

            this.Loaded += (sender, e) =>
            {
                this.FileSystems.ItemsSource =
                    openDirectory.EnumerateFileSystemInfos()
                    .Select(fileSystemInfo => fileSystemInfo.Name);
            };
        }
    }
}
