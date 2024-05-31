using System.ComponentModel;

namespace task4.Model;

public class Mechanic : INotifyPropertyChanged
{
    private int repairTime;

    public Mechanic(int repairTime)
    {
        this.repairTime = repairTime;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}