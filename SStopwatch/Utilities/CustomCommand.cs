using System;
using System.Windows.Input;

namespace SStopwatch.Utilities
{
    /// <summary>
    /// Represents the command which logic should be supplied as constructor arguments
    /// </summary>
    internal class CustomCommand : ICommand
    {
        //what action the command should make
        readonly Action<object> _execute;

        //the logic to check if the command is available for execution
        readonly Predicate<object>? _canExecute;

        public CustomCommand(Action<object> execute,
                            Predicate<object>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        //if the logic wasn't specified the command is always available
        public bool CanExecute(object? parameter) =>
            _canExecute == null ? true : _canExecute(parameter!);

        public void Execute(object? parameter) => _execute(parameter!);
    }
}
