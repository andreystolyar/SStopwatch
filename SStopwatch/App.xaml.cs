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

        IStopwatchModel model = new StopwatchModel();

        StopwatchViewModel viewmodel = new StopwatchViewModel(model);

        MainWindow = new StopwatchView
        {
            DataContext = viewmodel
        };

        MainWindow.Show();
    }
}
