using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task4.Model;

public abstract class Loader : INotifyPropertyChanged
{
    private String name;
    private int _capacity;
    private int _currentCapacity;
    private int _transportTime;
    private bool _isMoving;
    private int _waitingTimeSeconds;
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

    public int Capacity
    {
        get => _capacity;
        private set => _capacity = value;
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

    public int WaitingTimeSeconds
    {
        get => _waitingTimeSeconds;
        set
        {
            _waitingTimeSeconds = value;
            OnPropertyChanged(nameof(WaitingTimeSeconds));
        }
    }

    public string Name
    {
        get => name;
        private set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    protected Loader(String _name, int capacity, int transportTime)
    {
        this.name = _name;
        this._capacity = capacity;
        this._transportTime = transportTime;
        _currentCapacity = 0;
        WaitingTimeSeconds = 0;
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

        WaitingTimeSeconds = _transportTime / 1000;
        await Task.Run(() =>
        {
            // Thread.Sleep(_transportTime);
            while (WaitingTimeSeconds != 0)
            {
                Thread.Sleep(_transportTime);
                WaitingTimeSeconds -= 1;
            }
            {
                
            }
            
            unloader();
            
            WaitingTimeSeconds = _transportTime / 1000;
            while (WaitingTimeSeconds != 0)
            {
                Thread.Sleep(_transportTime);
                WaitingTimeSeconds -= 1;
            }
            
            IsMoving = false;
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}