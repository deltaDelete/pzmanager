namespace ProjectManager.ViewModels;

using System;
using System.Windows.Input;

public class Command<T> : ICommand {
    private readonly Action<T?> _action;
    private readonly Func<T?, bool>? _canExecute;

    public Command(Action<T?> action, Func<T?, bool>? canExecute = null) {
        _action = action;
        _canExecute = canExecute;
    }
    public bool CanExecute(object? parameter) {
        return _canExecute?.Invoke((T?)parameter) ?? true;
    }

    public void Execute(object? parameter) {
        _action.Invoke((T?)parameter);
    }

    public event EventHandler? CanExecuteChanged;
}

public class Command : ICommand {
    private readonly Action _action;
    private readonly Func<bool>? _canExecute;

    public Command(Action action, Func<bool>? canExecute = null) {
        _action = action;
        _canExecute = canExecute;
    }
    public bool CanExecute(object? parameter) {
        return _canExecute?.Invoke() ?? true;
    }

    public void Execute(object? parameter) {
        _action.Invoke();
    }

    public event EventHandler? CanExecuteChanged;
}