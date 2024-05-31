using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task4.Model;

public class Storage : INotifyPropertyChanged
{
    private int _capacity;
    private int _currentCapacity;

    public int CurrentCapacity
    {
        get => _currentCapacity;
        set
        {
            _currentCapacity = value;
            OnPropertyChanged(nameof(CurrentCapacity));
        }
    }

    public int Capacity
    {
        get => _capacity;
        private set => _capacity = value;
    }

    public Storage(int capacity)
    {
        this._capacity = capacity;
        _currentCapacity = 0;
    }

    public bool Add(int amount)
    {
        if (_capacity < _currentCapacity + amount)
        {
            _currentCapacity = _capacity;
            return false;
        }
        _currentCapacity += amount;
        return true;
    }

    public int TakeAmount(int amount)
    {
        if (CurrentCapacity >= amount)
        {
            CurrentCapacity -= amount;
        }
        else
        {
            amount = CurrentCapacity;
            CurrentCapacity = 0;
        }

        return amount;
    }

    public bool isEmpty()
    {
        return CurrentCapacity == 0;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}