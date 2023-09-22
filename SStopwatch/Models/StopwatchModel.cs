using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SStopwatch.Models;

internal class StopwatchModel
{
    //starting value for the stopwatch
    long _startPoint;

    //var for the main time
    TimeSpan _time;

    //save time during a pause
    TimeSpan _savedTime;

    //token source to stop the timer
    CancellationTokenSource? _cts;

    public event EventHandler? TimeChanged = delegate { };

    public bool IsRunning { get; set; }

    public TimeSpan Time
    {
        get => _time;
        private set
        {
            _time = _savedTime + value;
            TimeChanged!(this, EventArgs.Empty);
        }
    }

    public async void StartAsync()
    {
        if (IsRunning) return;

        IsRunning = true;

        using (_cts = new CancellationTokenSource())
            await StopwatchRoutineAsync(_cts.Token);
    }

    public void Pause()
    {
        if (!IsRunning) return;

        _cts?.Cancel();
        _savedTime = Time;
        IsRunning = false;
    }

    public void Stop()
    {
        if (Time == default) return;

        Time = _savedTime = default;

        Pause();
    }

    async Task StopwatchRoutineAsync(CancellationToken token)
    {
        _startPoint = Stopwatch.GetTimestamp();

        while (true)
        {
            try
            {
                await Task.Delay(1000, token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            Time = Stopwatch.GetElapsedTime(_startPoint);
        }
    }
}
