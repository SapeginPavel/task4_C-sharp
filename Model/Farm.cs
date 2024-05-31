using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace task4.Model;

public class Farm : INotifyPropertyChanged
{
    private int _maxQuantityPerHour;
    private Storage _storage;
    private int _timeToSleep;
    private bool _isWorking;

    public bool IsWorking
    {
        get => _isWorking;
        set
        {
            _isWorking = value;
            OnPropertyChanged(nameof(IsWorking));
        }
    }

    public int MaxQuantityPerHour
    {
        get => _maxQuantityPerHour;
        private set => _maxQuantityPerHour = value;
    }

    public Farm(int maxQuantityPerHour, Storage storage)
    {
        _maxQuantityPerHour = maxQuantityPerHour;
        _storage = storage;
        _isWorking = false;
        _timeToSleep = 1000;
    }

    async public void StartWorking()
    {
        IsWorking = true;
        Random random = new Random();
        await Task.Run(() =>
        {
            while (IsWorking)
            {
                Thread.Sleep(_timeToSleep);

                bool work = random.Next(1, 13) != 1; //шанс поломки 1/12

                if (!work)
                {
                    IsWorking = false;
                    return;
                }
            
                int product = random.Next(1, _maxQuantityPerHour + 1);
                bool wasAdded = _storage.Add(product);

                if (!wasAdded) //кончилось место
                {
                    IsWorking = false;
                }
            }
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}