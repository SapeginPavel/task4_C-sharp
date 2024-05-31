using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task4.Model;

public abstract class Loader : INotifyPropertyChanged
{
    private int _capacity;
    private int _currentCapacity;
    private int _transportTime;
    private bool _isMoving;
    delegate void Unloader();

    public int CurrentCapacity
    {
        get => _currentCapacity;
        set
        {
            _currentCapacity = value;
            OnPropertyChanged(nameof(CurrentCapacity));
        }
    }

    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            OnPropertyChanged(nameof(IsMoving));
        }
    }

    protected Loader(int capacity, int transportTime)
    {
        this._capacity = capacity;
        this._transportTime = transportTime;
        _currentCapacity = 0;
    }

    public void Load(ref int amount)
    {
        if (_capacity - _currentCapacity < amount)
        {
            amount = amount - (_capacity - _currentCapacity);
            CurrentCapacity = _capacity;
        }
        else
        {
            CurrentCapacity += amount;
            amount = 0;
        }
    }
    
    public void Unload()
    {
        CurrentCapacity = 0;
    }

    async public void Send()
    {
        IsMoving = true;
        Unloader unloader = Unload;
        await Task.Run(() =>
        {
            Thread.Sleep(_transportTime);
            unloader();
            Thread.Sleep(_transportTime);
            IsMoving = false;
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}