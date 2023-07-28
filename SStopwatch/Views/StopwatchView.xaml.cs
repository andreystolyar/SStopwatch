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
}
