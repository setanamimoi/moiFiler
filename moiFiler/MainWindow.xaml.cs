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
            if (Directory.Exists(clipboardContext) == true)
            {
                openDirectory = new DirectoryInfo(clipboardContext);
            }

            this.Loaded += (sender, e) =>
            {
                this.FileSystems.ItemsSource =
                    openDirectory.EnumerateFileSystemInfos();
            };

            this.SearchFilter.TextChanged += (sender, e) =>
            {
                this.FileSystems.Items.Filter = fileSystem =>
                {
                    var fileSystemInfo = fileSystem as FileSystemInfo;

                    var searchExpression = this.SearchFilter.Text.ToUpper();

                    return fileSystemInfo.Name.ToUpper().Contains(searchExpression);
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

                    var fileSystemInfo = this.FileSystems.SelectedItem as FileSystemInfo;

                    using (Process.Start(fileSystemInfo.Name))
                    {
                    }
                }
                
                if (Keyboard.IsKeyDown(Key.Up) == true || Keyboard.IsKeyDown(Key.Down))
                {
                    return;
                }

                this.SearchFilter.Focus();

            };
        }
    }
}
