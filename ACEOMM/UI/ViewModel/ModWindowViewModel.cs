using ACEOMM.Domain.Model;
using ACEOMM.UI.Commands;
using ACEOMM.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ACEOMM.UI.ViewModel
{
    public class ModWindowViewModel : ViewModel
    {
        public ModWindowViewModel(IView view)
            : base(view)
        {
            _businesses = new List<Business>();
            Businesses = new ListCollectionView(_businesses);
            Businesses.Filter = entity =>
                {
                    return !Current.Businesses.Contains(entity);
                };

            InitializeCommands();
        }

        #region Commands
        private void InitializeCommands()
        {
            AddBusinessToModCommand = new RelayCommand(CanAddBusinessToMod, AddBusinessToMod);
            RemoveBusinessFromModCommand = new RelayCommand(CanRemoveBusinessFromMod, RemoveBusinessFromMod);
        }

        public ICommand AddBusinessToModCommand { get; private set; }
        public ICommand RemoveBusinessFromModCommand { get; private set; }

        private bool CanAddBusinessToMod()
        {
            return SelectedBusiness != null;
        }

        private void AddBusinessToMod()
        {
            Current.Businesses.Add(SelectedBusiness);
            Businesses.Refresh();
            
        }

        private bool CanRemoveBusinessFromMod()
        {
            return SelectedModBusiness != null;
        }

        private void RemoveBusinessFromMod()
        {
            Current.Businesses.Remove(SelectedModBusiness);
            Businesses.Refresh();
        }
        #endregion

        private List<Business> _businesses;
        public ListCollectionView Businesses { get; private set; }

        private Mod _current;
        public Mod Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        private Business _selectedBusiness;
        public Business SelectedBusiness
        {
            get { return _selectedBusiness; }
            set { SetProperty(ref _selectedBusiness, value); }
        }

        private Business _selectedModBusiness;
        public Business SelectedModBusiness
        {
            get { return _selectedModBusiness; }
            set { SetProperty(ref _selectedModBusiness, value); }
        }

        public void Initialize(Mod entity, List<Business> businesses)
        {
            Current = entity;

            _businesses.Clear();
            _businesses.AddRange(businesses);
            Businesses.Refresh();
        }
    }
}
