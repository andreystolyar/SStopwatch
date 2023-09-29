using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            new PropertyMetadata(false, OnIsPlayingChanged));

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

    #region ButtonBrush
    public Brush ButtonBrush
    {
        get { return (Brush)GetValue(ButtonBrushProperty); }
        set { SetValue(ButtonBrushProperty, value); }
    }

    public static readonly DependencyProperty ButtonBrushProperty =
        DependencyProperty.Register(
            "ButtonBrush",
            typeof(Brush),
            typeof(PlayPauseButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
    #endregion

    #region ButtonPressedBrush
    public Brush ButtonPressedBrush
    {
        get { return (Brush)GetValue(ButtonPressedBrushProperty); }
        set { SetValue(ButtonPressedBrushProperty, value); }
    }

    public static readonly DependencyProperty ButtonPressedBrushProperty =
        DependencyProperty.Register(
            "ButtonPressedBrush",
            typeof(Brush),
            typeof(PlayPauseButton),
            new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));
    #endregion

    #region ButtonTransparentBrush
    public Brush ButtonTransparentBrush
    {
        get { return (Brush)GetValue(ButtonTransparentBrushProperty); }
        set { SetValue(ButtonTransparentBrushProperty, value); }
    }

    public static readonly DependencyProperty ButtonTransparentBrushProperty =
        DependencyProperty.Register(
            "ButtonTransparentBrush",
            typeof(Brush),
            typeof(PlayPauseButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
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

    static void OnIsPlayingChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var obj = (PlayPauseButton)d;

        if (obj.IsPlaying)
        {
            obj.PlayCommand.Execute(null);
            return;
        }

        obj.PauseCommand.Execute(null);
    }

}
