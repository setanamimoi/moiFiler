using System;
using System.Windows;

namespace moiFiler
{
    class EntryPoint
    {
        [STAThread]
        static void Main()
        {
            new Application().Run(new MainWindow());
        }
    }
}
