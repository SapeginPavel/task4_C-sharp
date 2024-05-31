using System.Windows.Input;

namespace task4.ViewModel;

public class CommandClass : ICommand
{
    private Action<object> execute;
    private Func<object, bool> canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public CommandClass(Action<object> execute, Func<object, bool> canExecute = null) // =null - это необязательный параметр (будет null)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return this.canExecute == null || this.canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        this.execute(parameter);
    }
}