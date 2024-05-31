using System.Collections.ObjectModel;
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

    private Loader _selectedLoader;

    private bool _isCallMechanicEnabled;

    public ObservableCollection<Loader> Loaders { get; set; }

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

    public Mechanic Mechanic
    {
        get => _mechanic;
        set => _mechanic = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Loader SelectedLoader
    {
        get => _selectedLoader;
        set => _selectedLoader = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool IsCallMechanicEnabled
    {
        get => _isCallMechanicEnabled;
        set
        {
            _isCallMechanicEnabled = value;
            OnPropertyChanged();
        }
    }

    public ViewModelClass(Farm farm, Mechanic mechanic, Storage storage, KiaLoader kiaLoader, VolvoLoader volvoLoader, VwLoader vwLoader)
    {
        _farm = farm;
        _mechanic = mechanic;
        _storage = storage;

        _farm.PropertyChanged += Farm_PropertyChanged;

        Loaders = new ObservableCollection<Loader>();
        Loaders.Add(kiaLoader);
        Loaders.Add(volvoLoader);
        Loaders.Add(vwLoader);

        IsCallMechanicEnabled = false;

        _farm.StartWorking();
    }
    
    private CommandClass _callMechanicCommand;
    public CommandClass CallMechanicCommand
    {
        get
        {
            return (_callMechanicCommand = new CommandClass(o =>
            {
                IsCallMechanicEnabled = false;
                _mechanic.RepairFarm(_farm);
            }));
        }
    }
    
    private CommandClass _loadLoaderCommand;
    public CommandClass LoadLoaderCommand
    {
        get
        {
            return (_loadLoaderCommand = new CommandClass(o =>
            {
                _storage.TakeProduct(SelectedLoader);
            }));
        }
    }
    
    private CommandClass _sendLoaderCommand;
    public CommandClass SendLoaderCommand
    {
        get
        {
            return (_sendLoaderCommand = new CommandClass(o =>
            {
                SelectedLoader.Send();
            }));
        }
    }
    
    private void Farm_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Farm.IsWorking))
        {
            IsCallMechanicEnabled = !Farm.IsWorking;
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}