using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace task4.Model;

public class Mechanic : INotifyPropertyChanged
{
    private int repairTime;

    public Mechanic(int repairTime)
    {
        this.repairTime = repairTime;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}