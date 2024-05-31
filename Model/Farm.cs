using System.ComponentModel;

namespace task4.Model;

public class Farm : INotifyPropertyChanged
{
    private int quantityPerHour;

    public Farm(int quantityPerHour)
    {
        this.quantityPerHour = quantityPerHour;
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