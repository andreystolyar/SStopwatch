using SStopwatch.Controls;
using System.Windows;

namespace SStopwatch.Views;

public partial class StopwatchView : Window
{
    public StopwatchView()
    {
        InitializeComponent();
    }

    private void PinButton_Click(object sender, RoutedEventArgs e)
    {
        Topmost = !Topmost;
    }

    void StopButton_StopEvent(object sender, System.EventArgs e)
    {
        if (PlayPause.IsPlaying)
            PlayPause.IsPlaying = false;
    }
}
