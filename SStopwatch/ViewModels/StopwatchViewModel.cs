using System;
using System.ComponentModel;
using System.Windows.Input;
using SStopwatch.Models;
using SStopwatch.Utilities;

namespace SStopwatch.ViewModels;

internal class StopwatchViewModel : INotifyPropertyChanged
{
    readonly StopwatchModel model;

    public ICommand Start { get; }
    public ICommand Pause { get; }
    public ICommand Stop { get; }

    public StopwatchViewModel(StopwatchModel model)
    {
        this.model = model;

        model.TimeChanged +=
            delegate { PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Time))); };

        Start = new CustomCommand(_ => model.StartAsync());
        Pause = new CustomCommand(_ => model.Pause());
        Stop = new CustomCommand(_ => model.Stop());
    }

    public TimeSpan Time => model.Time;

    public event PropertyChangedEventHandler? PropertyChanged = delegate { };
}
