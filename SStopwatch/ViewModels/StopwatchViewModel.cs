using System;
using System.ComponentModel;
using System.Windows.Input;
using SStopwatch.Models;
using SStopwatch.Utilities;

namespace SStopwatch.ViewModels;

internal class StopwatchViewModel : INotifyPropertyChanged
{
    IStopwatchModel model;

    public ICommand Start { get; }
    public ICommand Pause { get; }
    public ICommand Stop { get; }

    public StopwatchViewModel(IStopwatchModel model)
    {
        this.model = model;
        model.TimeChanged += Model_TimeChanged;

        Start = new CustomCommand(_ => model.StartAsync(), _ => !model.IsRunning);
        Pause = new CustomCommand(_ => model.Pause(), _ => model.IsRunning);
        Stop = new CustomCommand(_ => model.Stop());
    }

    private void Model_TimeChanged(object? sender, EventArgs e)
    {
        Time = model.Time;
    }

    public TimeSpan Time
    {
        get => model.Time;
        set => PropertyChanged?.Invoke
            (this, new PropertyChangedEventArgs(nameof(Time)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
