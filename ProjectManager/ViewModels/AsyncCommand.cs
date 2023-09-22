using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.ViewModels;

public class AsyncCommand<T> : ICommand {
    private bool _busy;
    private Func<T?, Task> _action; 

    private bool Busy {
        get => _busy;
        set {
            _busy = value;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public AsyncCommand(Func<T?, Task> action) {
        _action = action;
    }
    
    public bool CanExecute(object? parameter) => !_busy;

    public async void Execute(object? parameter) {
        if (Busy) return;
        try {
            Busy = true;
            await _action((T?)parameter);
        }
        finally {
            Busy = false;
        }
    }

    public event EventHandler? CanExecuteChanged;
}