using System.ComponentModel;

namespace task4.Model;

public abstract class Loader : INotifyPropertyChanged
{
    private int capacity;
    private int transportTime;

    protected Loader(int capacity, int transportTime)
    {
        this.capacity = capacity;
        this.transportTime = transportTime;
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