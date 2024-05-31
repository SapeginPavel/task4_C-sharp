using System.Collections.ObjectModel;
using System.Windows;
using task4.Model;
using task4.Model.LoaderImpls;

namespace task4.ViewModel;

public class ViewModelClass
{
    private Farm _farm;
    private Mechanic _mechanic;
    private Storage _storage;

    private KiaLoader _kiaLoader;
    private VolvoLoader _volvoLoader;
    private VwLoader _vwLoader;
    private Loader _selectedLoader;

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

    public ViewModelClass(Farm farm, Mechanic mechanic, Storage storage, KiaLoader kiaLoader, VolvoLoader volvoLoader, VwLoader vwLoader)
    {
        _farm = farm;
        _mechanic = mechanic;
        _storage = storage;
        _kiaLoader = kiaLoader;
        _volvoLoader = volvoLoader;
        _vwLoader = vwLoader;

        Loaders = new ObservableCollection<Loader>();
        
        Loaders.Add(kiaLoader);
        Loaders.Add(volvoLoader);
        Loaders.Add(vwLoader);

        _farm.StartWorking();
    }
    
    private CommandClass _callMechanicCommand;
    public CommandClass CallMechanicCommand
    {
        get
        {
            return (_callMechanicCommand = new CommandClass(o =>
            {
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
}