using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
        CurrentCapacity = 0;
    }

    public bool Add(int amount)
    {
        if (_capacity < _currentCapacity + amount)
        {
            CurrentCapacity = _capacity;
            return false;
        }
        CurrentCapacity += amount;
        return true;
    }

    public void TakeProduct(Loader loader)
    {
        int availableCapacityInLoader = loader.GetAvailableCapacity();
        int currentCapacityInThread = CurrentCapacity; //потокобезопасно
        int forLoadCapacity;
        if (availableCapacityInLoader >= currentCapacityInThread)
        {
            forLoadCapacity = currentCapacityInThread;
        }
        else
        {
            forLoadCapacity = availableCapacityInLoader;
        }
        bool wasLoaded = loader.Load(forLoadCapacity);
        if (wasLoaded)
        {
            CurrentCapacity -= forLoadCapacity;
        }
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