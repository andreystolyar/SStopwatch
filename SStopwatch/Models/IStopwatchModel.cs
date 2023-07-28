using System;

namespace SStopwatch.Models
{
    internal interface IStopwatchModel
    {
        bool IsRunning { get; set; }
        TimeSpan Time { get; }

        event EventHandler? TimeChanged;

        void StartAsync();
        void Pause();
        void Stop();
    }
}