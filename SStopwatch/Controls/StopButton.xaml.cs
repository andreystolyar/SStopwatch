using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SStopwatch.Controls;

/// <summary>
/// Interaction logic for StopButton.xaml
/// </summary>
public partial class StopButton : UserControl
{
    //Animation for the progress circle around the button.
    readonly DoubleAnimation progressAnimation;

    /*
    Timer to count the time of pressing the stop button.
    You can't rely on the animation.Completed event in this case
    because it doesn't cancels the time when the button is unpressed.
    */
    Timer _timer;

    public StopButton()
    {
        InitializeComponent();

        /*
        Add new handlers for the events. Third parameter "true" is necessary
        for the handlers to work. Another way is to use
        PreviewMouseLeftButtonDown and PreviewMouseLeftButtonUp events.
        */
        Btn.AddHandler(MouseLeftButtonDownEvent,
            new MouseButtonEventHandler(ButtonDown), true);
        Btn.AddHandler(MouseLeftButtonUpEvent,
            new MouseButtonEventHandler(ButtonUp), true);

        /*
        The inner button width is fixed and equal to 50. But the progress
        circle width is customizable. In order the image not to adjust the
        distance between elements when the progress bar appears it is necessary
        to calculate the correct margin beforehand.
        */
        if (ProgressCircleDiameter > 50)
            Btn.Margin = new Thickness((ProgressCircleDiameter - 50) / 2);

        progressAnimation = new DoubleAnimation()
        {
            From = 0d,
            To = 100d,
            Duration = TimeSpan.FromMilliseconds(PressingTime)
        };

        _timer = new()
        {
            Interval = PressingTime,
            AutoReset = true
        };
        _timer.Elapsed += _timer_Elapsed;
    }

    void _timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Dispatcher.BeginInvoke(() => StopCommand.Execute(null));
    }

    void ButtonDown(object sender, MouseButtonEventArgs e)
    {
        BeginAnimation(ProgressCirclePercentageProperty, progressAnimation);
        _timer.Start();
    }

    void ButtonUp(object sender, MouseButtonEventArgs e)
    {
        // null stops animation
        BeginAnimation(ProgressCirclePercentageProperty, null);
        _timer.Stop();
    }

    double Angle { get; set; }

    #region Dependency Properties

    #region StopButtonBrush
    public Brush StopButtonBrush
    {
        get { return (Brush)GetValue(StopButtonBrushProperty); }
        set { SetValue(StopButtonBrushProperty, value); }
    }

    public static readonly DependencyProperty StopButtonBrushProperty =
        DependencyProperty.Register(
            "StopButtonBrush",
            typeof(Brush),
            typeof(StopButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
    #endregion

    #region StopButtonPressedBrush
    public Brush StopButtonPressedBrush
    {
        get { return (Brush)GetValue(StopButtonPressedBrushProperty); }
        set { SetValue(StopButtonPressedBrushProperty, value); }
    }

    public static readonly DependencyProperty StopButtonPressedBrushProperty =
        DependencyProperty.Register(
            "StopButtonPressedBrush",
            typeof(Brush),
            typeof(StopButton),
            new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));
    #endregion

    #region StopButtonTransparentBrush
    public Brush StopButtonTransparentBrush
    {
        get { return (Brush)GetValue(StopButtonTransparentBrushProperty); }
        set { SetValue(StopButtonTransparentBrushProperty, value); }
    }

    public static readonly DependencyProperty StopButtonTransparentBrushProperty =
        DependencyProperty.Register(
            "StopButtonTransparentBrush",
            typeof(Brush),
            typeof(StopButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
    #endregion

    #region ProgressCircleDiameter
    public double ProgressCircleDiameter
    {
        get { return (double)GetValue(ProgressCircleDiameterProperty); }
        set { SetValue(ProgressCircleDiameterProperty, value); }
    }

    public static readonly DependencyProperty ProgressCircleDiameterProperty =
        DependencyProperty.Register(
            "ProgressCircleDiameter",
            typeof(double),
            typeof(StopButton),
            new PropertyMetadata(60d, OnCircleDiameterChanged));
    #endregion

    #region ProgressCircleBrush

    public Brush ProgressCircleBrush
    {
        get { return (Brush)GetValue(ProgressCircleBrushProperty); }
        set { SetValue(ProgressCircleBrushProperty, value); }
    }

    public static readonly DependencyProperty ProgressCircleBrushProperty =
        DependencyProperty.Register(
            "ProgressCircleBrush",
            typeof(Brush),
            typeof(StopButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Red),
                new PropertyChangedCallback(OnProgressCircleBrushChanged)));
    #endregion

    #region ProgressCircleLineThickness
    public int ProgressCircleLineThickness
    {
        get { return (int)GetValue(ProgressCircleLineThicknessProperty); }
        set { SetValue(ProgressCircleLineThicknessProperty, value); }
    }

    public static readonly DependencyProperty ProgressCircleLineThicknessProperty =
        DependencyProperty.Register(
            "ProgressCircleLineThickness",
            typeof(int),
            typeof(StopButton),
            new PropertyMetadata(2,
                new PropertyChangedCallback(OnThicknessChanged)));
    #endregion

    #region ProgressCirclePercentage
    public double ProgressCirclePercentage
    {
        get { return (double)GetValue(ProgressCirclePercentageProperty); }
        set { SetValue(ProgressCirclePercentageProperty, value); }
    }

    public static readonly DependencyProperty ProgressCirclePercentageProperty =
        DependencyProperty.Register(
            "ProgressCirclePercentage",
            typeof(double),
            typeof(StopButton),
            new PropertyMetadata(0d, OnProgressCirclePercentageChange));
    #endregion

    #region StopCommand
    public ICommand StopCommand
    {
        get { return (ICommand)GetValue(StopCommandProperty); }
        set { SetValue(StopCommandProperty, value); }
    }

    public static readonly DependencyProperty StopCommandProperty =
        DependencyProperty.Register(
            "StopCommand",
            typeof(ICommand),
            typeof(StopButton),
            new PropertyMetadata(null));
    #endregion

    #region PressingTime
    public double PressingTime
    {
        get { return (double)GetValue(PressingTimeProperty); }
        set { SetValue(PressingTimeProperty, value); }
    }

    public static readonly DependencyProperty PressingTimeProperty =
        DependencyProperty.Register(
            "PressingTime",
            typeof(double),
            typeof(StopButton),
            new PropertyMetadata(1000d));
    #endregion

    #endregion

    static void OnCircleDiameterChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var circle = d as StopButton;
        circle.RenderArc();
    }

    static void OnProgressCircleBrushChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var circle = d as StopButton;
        circle.ProgressCircleRoot.Stroke = (SolidColorBrush)e.NewValue;
    }

    static void OnProgressCirclePercentageChange(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var circle = d as StopButton;

        if (circle.ProgressCirclePercentage > 100)
            circle.ProgressCirclePercentage = 100;

        circle.Angle = circle.ProgressCirclePercentage * 360 / 100;
        circle.RenderArc();
    }

    static void OnThicknessChanged(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var circle = d as StopButton;
        circle.ProgressCircleRoot.StrokeThickness = (int)e.NewValue;
        circle.RenderArc();
    }

    void RenderArc()
    {
        ProgressCircleRoot.Width = ProgressCircleDiameter;
        ProgressCircleRoot.Height = ProgressCircleDiameter;

        /*
        Half of the thickness goes before the center of the line, half goes
        after. In our calculation of points we have to condider only the half
        of the line thickness. If the line thickness is odd we have to add 0.5
        in order to not go outside of the boundaries of the element.
        It's important to compute it before the using, otherwise double value
        might be rounded up unexpectedly causing changing positions of points.
        */
        double halfThickness = (ProgressCircleLineThickness & 1) == 1
            ? ProgressCircleLineThickness / 2 + 0.5
            : ProgressCircleLineThickness / 2;

        /*
        "ProgressCircleDiameter / 2" - half of the given size of the element.
        We have to reduce the radius by the outer half of the line thickness
        for the circle to fit into the given diameter size.
        */
        double radius = ProgressCircleDiameter / 2 - halfThickness;

        /*
        Here we have to add the thickness of the line to correctly locate the
        starting point in order to show all the line thickness and not truncate
        it.
        */
        ProgressCircleFigure.StartPoint =
            new Point(radius + halfThickness, halfThickness);

        var endPoint = ComputeCartesianCoordinate(Angle, radius, halfThickness);

        arcSegment.Point = endPoint;
        arcSegment.Size = new Size(radius, radius);
        arcSegment.IsLargeArc = Angle > 180.0;
    }

    static Point ComputeCartesianCoordinate
        (double angle, double radius, double thickness)
    {
        // convert to radians
        double angleRad = Math.PI / 180.0 * (angle - 90);

        double x = radius * Math.Cos(angleRad) + radius + thickness;
        double y = radius * Math.Sin(angleRad) + radius + thickness;

        return new Point(x, y);
    }
}
