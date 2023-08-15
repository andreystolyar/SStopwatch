using SStopwatch.Models;
using SStopwatch.ViewModels;
using SStopwatch.Views;
using SStopwatch.Utilities;
using System.Windows;

namespace SStopwatch;

public partial class Application : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        SingleInstanceWatcher.Run(this);

        base.OnStartup(e);

        StopwatchViewModel viewmodel = new(new StopwatchModel());

        MainWindow = new StopwatchView
        {
            DataContext = viewmodel
        };

        MainWindow.Show();
    }
}
