using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SStopwatch.Controls;

public partial class PlayPauseButton : UserControl
{
    public PlayPauseButton()
    {
        InitializeComponent();
    }

    #region Dependency Properties

    #region IsPlaying
    public bool IsPlaying
    {
        get { return (bool)GetValue(IsPlayingProperty); }
        set { SetValue(IsPlayingProperty, value); }
    }

    public static readonly DependencyProperty IsPlayingProperty =
        DependencyProperty.Register(
            "IsPlaying",
            typeof(bool),
            typeof(PlayPauseButton),
            new PropertyMetadata(false));

    #endregion

    #region PlayCommand
    public ICommand PlayCommand
    {
        get { return (ICommand)GetValue(PlayCommandProperty); }
        set { SetValue(PlayCommandProperty, value); }
    }

    public static readonly DependencyProperty PlayCommandProperty =
        DependencyProperty.Register(
            "PlayCommand",
            typeof(ICommand),
            typeof(PlayPauseButton),
            new PropertyMetadata(null));

    #endregion

    #region PauseCommand
    public ICommand PauseCommand
    {
        get { return (ICommand)GetValue(PauseCommandProperty); }
        set { SetValue(PauseCommandProperty, value); }
    }

    public static readonly DependencyProperty PauseCommandProperty =
        DependencyProperty.Register(
            "PauseCommand",
            typeof(ICommand),
            typeof(PlayPauseButton),
            new PropertyMetadata(null));

    #endregion

    #endregion

    void Button_Click(object sender, RoutedEventArgs e)
    {
        if (IsPlaying)
        {
            IsPlaying = false;
            PauseCommand.Execute(null);
            return;
        }

        IsPlaying = true;
        PlayCommand.Execute(null);
    }
}
