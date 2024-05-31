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

    public ViewModelClass(Farm farm, Mechanic mechanic, Storage storage, KiaLoader kiaLoader, VolvoLoader volvoLoader, VwLoader vwLoader)
    {
        _farm = farm;
        _mechanic = mechanic;
        _storage = storage;
        _kiaLoader = kiaLoader;
        _volvoLoader = volvoLoader;
        _vwLoader = vwLoader;
    }
}