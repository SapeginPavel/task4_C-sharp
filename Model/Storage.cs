using System.ComponentModel;

namespace task4.Model;

public class Storage : INotifyPropertyChanged
{
    private int capacity;
    private int busyCapacity;

    public Storage(int capacity)
    {
        this.capacity = capacity;
        busyCapacity = 0;
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