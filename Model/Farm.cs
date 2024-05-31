using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace task4.Model;

public class Farm : INotifyPropertyChanged
{
    private int _maxQuantityPerHour;
    private Storage _storage;
    private int timeToSleep = 1000;
    private bool _isDestroy;

    public bool IsDestroy
    {
        get => _isDestroy;
        set
        {
            _isDestroy = value;
            OnPropertyChanged(nameof(IsDestroy));
        }
    }
    
    

    async private void StartWorking()
    {
        Random random = new Random();
        await Task.Run(() =>
        {
            Thread.Sleep(timeToSleep);

            IsDestroy = random.Next(1, 50) == 1; //шанс поломки 2%

            if (IsDestroy)
            {
                return;
            }
            
            int product = random.Next(1, _maxQuantityPerHour);
            _storage.Add(product);
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}