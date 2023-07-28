using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SStopwatch.Utilities;

/// <summary>
/// Provides a method to watch that the only one instance of the application is running.
/// </summary>
class SingleInstanceWatcher
{
    /// <summary>
    /// Starts the monitoring for another instance of the application to run.
    /// </summary>
    /// <param name="app">The application to watch for.</param>
    public static void Run(Application app)
    {
        // event to synchronize different instances of the application
        EventWaitHandle? eventWaitHandle;
        const string EventName = "SStopwatchSynchronizationEvent";

        try
        {
            // if the event already exists then another instance is running
            // otherwise OpenExisting() will throw an exception
            eventWaitHandle = EventWaitHandle.OpenExisting(EventName);

            // Notify other instance so it could bring itself to foreground.
            eventWaitHandle.Set();

            // Terminate this instance.
            app.Shutdown();
        }
        catch (WaitHandleCannotBeOpenedException)
        {
            // create the event
            eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, EventName);
        }

        // the task to watch for the event to fire
        new Task(() =>
        {
            while (eventWaitHandle.WaitOne())
            {
                app.Dispatcher.BeginInvoke(() =>
                {
                    // save the initial window state
                    var mw = app.MainWindow;
                    var winState = mw.WindowState;

                    // if it was minimized then restore
                    if (winState == WindowState.Minimized)
                    {
                        mw.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        // the workaround to focus on the already existed window
                        mw.WindowState = WindowState.Minimized;
                        mw.WindowState = winState;
                    }
                });
            }
        })
        .Start();
    }
}
