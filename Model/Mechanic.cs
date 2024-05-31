using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task4.Model;

public class Mechanic : INotifyPropertyChanged
{
    private int _repairTimeSeconds;
    private Random _random;

    public int RepairTimeSeconds
    {
        get => _repairTimeSeconds;
        set
        {
            _repairTimeSeconds = value;
            OnPropertyChanged(nameof(RepairTimeSeconds));
        }
    }

    public Mechanic()
    {
        RepairTimeSeconds = 0;
        _random = new Random();
    }


    async public void RepairFarm(Farm farm)
    {
        if (farm.IsWorking)
        {
            return;
        }
        RepairTimeSeconds = _random.Next(2, 6);
        await Task.Run(() =>
        {
            while (RepairTimeSeconds != 0)
            {
                Thread.Sleep(1000);
                if (RepairTimeSeconds <= 0)
                {
                    break;
                }
                RepairTimeSeconds -= 1;
            }
            farm.StartWorking();
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}