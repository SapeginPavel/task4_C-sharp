using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using task4.Model;
using task4.Model.LoaderImpls;

namespace task4.ViewModel;

public class ViewModelClass : INotifyPropertyChanged
{
    private Farm _farm;
    private Mechanic _mechanic;
    private Storage _storage;

    private KiaLoader _kiaLoader;
    private VolvoLoader _volvoLoader;
    private VwLoader _vwLoader;

    public Farm Farm
    {
        get => _farm;
        set => _farm = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Storage Storage
    {
        get => _storage;
        set => _storage = value ?? throw new ArgumentNullException(nameof(value));
    }


    public ViewModelClass(Farm farm, Mechanic mechanic, Storage storage, KiaLoader kiaLoader, VolvoLoader volvoLoader, VwLoader vwLoader)
    {
        _farm = farm;
        _mechanic = mechanic;
        _storage = storage;
        _kiaLoader = kiaLoader;
        _volvoLoader = volvoLoader;
        _vwLoader = vwLoader;

        _farm.PropertyChanged += Farm_PropertyChanged;
        _storage.PropertyChanged += Storage_PropertyChanged;
        
        _farm.StartWorking();
    }
    
    private void Farm_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Farm.IsWorking):
                OnPropertyChanged(nameof(Farm.IsWorking));
                break;
        }
    }
    
    private void Storage_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // switch (e.PropertyName)
        // {
        //     case nameof(Storage.CurrentCapacity):
        //         OnPropertyChanged(nameof(Storage.CurrentCapacity));
        //         break;
        // }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}