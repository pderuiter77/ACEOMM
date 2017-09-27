using ACEOMM.Domain.Model;
using ACEOMM.UI.Commands;
using ACEOMM.UI.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            return SelectedBusinesses != null && SelectedBusinesses.Count > 0;
        }

        private void AddBusinessToMod()
        {
            for (var i = SelectedBusinesses.Count - 1; i >= 0; i--)
                Current.Businesses.Add((Business)SelectedBusinesses[i]);
            Businesses.Refresh();
            
        }

        private bool CanRemoveBusinessFromMod()
        {
            return SelectedModBusinesses != null && SelectedModBusinesses.Count > 0;
        }

        private void RemoveBusinessFromMod()
        {
            for (var i = SelectedModBusinesses.Count - 1; i >= 0; i--)
                Current.Businesses.Remove((Business)SelectedModBusinesses[i]);
            Businesses.Refresh();
        }
        #endregion

        private IList _selectedModBusinesses;
        public IList SelectedModBusinesses
        {
            get { return _selectedModBusinesses; }
            set { SetProperty(ref _selectedModBusinesses, value); }
        }

        private IList _selectedBusinesses;
        public IList SelectedBusinesses
        {
            get { return _selectedBusinesses; }
            set { SetProperty(ref _selectedBusinesses, value); }
        }

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
