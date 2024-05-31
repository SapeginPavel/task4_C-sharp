using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task4.Model;

public class Storage : INotifyPropertyChanged
{
    private int capacity;
    private int currentCapacity;

    private int CurrentCapacity
    {
        get => currentCapacity;
        set
        {
            currentCapacity = value;
            OnPropertyChanged(nameof(CurrentCapacity));
        }
    }

    public Storage(int capacity)
    {
        this.capacity = capacity;
        currentCapacity = 0;
    }

    public void Add(int amount)
    {
        currentCapacity += amount;
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
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}