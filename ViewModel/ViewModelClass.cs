using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using task4.Model;
using task4.Model.LoaderImpls;

namespace task4.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        private Farm _farm;
        private Mechanic _mechanic;
        private Storage _storage;

        private Loader _selectedLoader;

        private bool _isCallMechanicEnabled;
        private bool _isLoadLoaderEnabled;
        private bool _isSendLoaderEnabled;

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
            set
            {
                if (_selectedLoader != value)
                {
                    if (_selectedLoader != null)
                    {
                        _selectedLoader.PropertyChanged -= LoaderMoving_PropertyChanged;
                    }

                    _selectedLoader = value;
                    OnPropertyChanged();

                    if (_selectedLoader != null)
                    {
                        _selectedLoader.PropertyChanged += LoaderMoving_PropertyChanged;
                    }

                    UpdateButtonStates();
                }
            }
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

        public bool IsLoadLoaderEnabled
        {
            get => _isLoadLoaderEnabled;
            set
            {
                _isLoadLoaderEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsSendLoaderEnabled
        {
            get => _isSendLoaderEnabled;
            set
            {
                _isSendLoaderEnabled = value;
                OnPropertyChanged();
            }
        }

        public ViewModelClass(Farm farm, Mechanic mechanic, Storage storage, KiaLoader kiaLoader, VolvoLoader volvoLoader, VwLoader vwLoader)
        {
            _farm = farm;
            _mechanic = mechanic;
            _storage = storage;

            _farm.PropertyChanged += Farm_PropertyChanged;

            Loaders = new ObservableCollection<Loader>
            {
                kiaLoader,
                volvoLoader,
                vwLoader
            };

            IsCallMechanicEnabled = false;
            IsLoadLoaderEnabled = true;
            IsSendLoaderEnabled = true;

            _farm.StartWorking();
        }

        private ICommand _callMechanicCommand;
        public ICommand CallMechanicCommand => _callMechanicCommand ??= new RelayCommand(o =>
        {
            IsCallMechanicEnabled = false;
            _mechanic.RepairFarm(_farm);
        });

        private ICommand _loadLoaderCommand;
        public ICommand LoadLoaderCommand => _loadLoaderCommand ??= new RelayCommand(o =>
        {
            _storage.TakeProduct(SelectedLoader);
        });

        private ICommand _sendLoaderCommand;
        public ICommand SendLoaderCommand => _sendLoaderCommand ??= new RelayCommand(o =>
        {
            SelectedLoader.Send();
        });

        private void Farm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Farm.IsWorking))
            {
                IsCallMechanicEnabled = !Farm.IsWorking;
            }
        }

        private void LoaderMoving_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Loader.IsMoving))
            {
                UpdateButtonStates();
            }
        }

        private void UpdateButtonStates()
        {
            if (SelectedLoader != null)
            {
                IsLoadLoaderEnabled = !SelectedLoader.IsMoving;
                IsSendLoaderEnabled = !SelectedLoader.IsMoving;
            }
            else
            {
                IsLoadLoaderEnabled = false;
                IsSendLoaderEnabled = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
