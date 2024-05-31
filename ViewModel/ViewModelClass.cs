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


    public ViewModelClass(Farm farm, Mechanic mechanic, Storage storage, KiaLoader kiaLoader, VolvoLoader volvoLoader, VwLoader vwLoader)
    {
        _farm = farm;
        _mechanic = mechanic;
        _storage = storage;
        _kiaLoader = kiaLoader;
        _volvoLoader = volvoLoader;
        _vwLoader = vwLoader;

        _farm.StartWorking();
    }
    
    private CommandClass _callMechanicCommand;
    public CommandClass CallMechanicCommand
    {
        get
        {
            return (_callMechanicCommand = new CommandClass(o =>
            {
                MessageBox.Show("Механик вызван");
                _mechanic.RepairFarm(_farm);
            }));
        }
    }
}