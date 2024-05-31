using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using task4.Model;
using task4.Model.LoaderImpls;
using task4.ViewModel;

namespace task4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        Storage storage = new Storage(1000);
        Farm farm = new Farm(4, storage);
        Mechanic mechanic = new Mechanic();

        KiaLoader kiaLoader = new KiaLoader("Kia", 50, 3000);
        VolvoLoader volvoLoader = new VolvoLoader("Volvo", 70, 4000);
        VwLoader vwLoader = new VwLoader("Volkswagen", 90, 6000);
        
        ViewModelClass vmc = new ViewModelClass(farm, mechanic, storage, kiaLoader, volvoLoader, vwLoader);
        DataContext = vmc;
        
        InitializeComponent();
    }
}