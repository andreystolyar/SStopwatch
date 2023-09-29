using System.Windows;
using System.Windows.Input;

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
        if (PlayPauseBtn.IsPlaying)
            PlayPauseBtn.IsPlaying = false;
    }

    void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Enter:
            case Key.Space:
                if (e.Key == Key.Enter || e.Key == Key.Space)
                    PlayPauseBtn.IsPlaying = !PlayPauseBtn.IsPlaying;
                break;
            case Key.Escape:
                StopBtn.StopCommand.Execute(null);
                StopButton_StopEvent(null, null);
                break;
        }
    }

}
