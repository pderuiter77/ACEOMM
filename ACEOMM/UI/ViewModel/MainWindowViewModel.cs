using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using ACEOMM.Services;
using ACEOMM.UI.Commands;
using ACEOMM.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace ACEOMM.UI.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private IMainView _view;
        private IDataService _dataService;

        protected MainWindowViewModel(IMainView view, IDataService dataService)
            : base(view)
        {
            _view = view;
            _dataService = dataService;
            AvailableGroupings = new List<string>
            {
                "None",
                "Author",
                "Class",
                "Country",
                "Status",
                "Type",
                "Version"
            };
            InitializeCommands();
            InitializeData();
            LoadData();
        }

        public MainWindowViewModel(IMainView view)
            :this(view, new XmlDataService())
        { }

        private bool IsModified()
        {
            return _businesses.Any(x => x.Status != EntityStatus.Unchanged) || 
                   _products.Any(x => x.Status != EntityStatus.Unchanged) ||
                   _mods.Any(x => x.Status != EntityStatus.Unchanged);
        }

        private void InitializeData()
        {
            _countries = new List<Country>();
            Countries = new ListCollectionView(_countries);

            _mods = new List<Mod>();
            Mods = new ListCollectionView(_mods);

            _businesses = new List<Business>();
            Businesses = new ListCollectionView(_businesses);

            Businesses.Filter = entity =>
            {
                var business = (Business)entity;
                var result = true;
                if (!string.IsNullOrWhiteSpace(NameFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Name, NameFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(DescriptionFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Description, DescriptionFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(CeoFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.CEO, CeoFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(CountryFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Country.Name, CountryFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(AuthorFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Author, AuthorFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(VersionFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Version, VersionFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(StatusFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Status.ToString(), StatusFilter, CompareOptions.IgnoreCase) >= 0;
                if (!string.IsNullOrWhiteSpace(TypeFilter))
                    result = result && CultureInfo.CurrentUICulture.CompareInfo.IndexOf(business.Type.ToString(), TypeFilter, CompareOptions.IgnoreCase) >= 0;
                return result;
            };
            
            _products = new List<Product>();
            Products = new ListCollectionView(_products);
        }

        private List<Country> _countries;
        public ListCollectionView Countries { get; private set; }
        private List<Mod> _mods;
        public ListCollectionView Mods { get; private set; }
        private List<Business> _businesses;
        public ListCollectionView Businesses { get; private set; }
        private List<Product> _products;
        public ListCollectionView Products { get; private set; }

        private Business _currentBusiness;
        public Business CurrentBusiness
        {
            get { return _currentBusiness; }
            set { SetProperty(ref _currentBusiness, value); }
        }

        private Product _currentProduct;
        public Product CurrentProduct
        {
            get { return _currentProduct; }
            set { SetProperty(ref _currentProduct, value); }
        }

        private Entity _currentTreeEntity;
        public Entity CurrentTreeEntity
        {
            get { return _currentTreeEntity; }
            set
            {
                SetProperty(ref _currentTreeEntity, value);
                if (value is Business)
                    CurrentBusiness = (Business)value;
            }
        }

        private Entity _parentTreeEntity;
        public Entity ParentTreeEntity
        {
            get { return _parentTreeEntity; }
            set
            {
                SetProperty(ref _parentTreeEntity, value);
            }
        }

        private string _selectedGrouping;
        public string SelectedGrouping
        {
            get { return _selectedGrouping; }
            set { SetProperty(ref _selectedGrouping, value); GroupBusinesses(value); }
        }

        private List<string> _availableGroupings;
        public List<string> AvailableGroupings
        {
            get { return _availableGroupings; }
            set { SetProperty(ref _availableGroupings, value); }
        }

        #region Status update
        private string _currentAction;
        public string CurrentAction
        {
            get { return _currentAction; }
            set { SetProperty(ref _currentAction, value); }
        }

        private int _progressBarMaximum;
        public int ProgressBarMaximum
        { 
            get { return _progressBarMaximum; }
            set { SetProperty(ref _progressBarMaximum, value); }
        }

        private int _progressBarCurrentValue;
        public int ProgressBarCurrentValue
        { 
            get { return _progressBarCurrentValue; }
            set { SetProperty(ref _progressBarCurrentValue, value); }
        }

        private void UpdateProgress(int total, int current, string text)
        {
            ProgressBarMaximum = total;
            ProgressBarCurrentValue = current;
            CurrentAction = text;
            Dispatcher.CurrentDispatcher.Invoke(delegate { }, DispatcherPriority.Render);
            
        }
        #endregion

        public void EditEntity(Entity entity)
        {
            if (entity is Business)
            {
                CurrentBusiness = (Business)entity;
                EditBusiness();
            }
            if (entity is Mod)
                EditMod();
            if (entity is Product)
            {
                CurrentProduct = (Product)entity;
                EditProduct();
            }
        }

        #region Commands
        private void InitializeCommands()
        {
            LoadDataCommand = new RelayCommand(LoadData);
            SaveDataCommand = new RelayCommand(SaveData);
            ImportDataCommand = new RelayCommand(ImportData);
            ExitCommand = new RelayCommand(Exit);
            AddBusinessCommand = new RelayCommandOfT<BusinessType>(AddBusiness);
            EditBusinessCommand = new RelayCommand(CanEditBusiness, EditBusiness);
            RemoveBusinessCommand = new RelayCommand(CanRemoveBusiness, RemoveBusiness);
            AddModCommand = new RelayCommand(AddMod);
            EditEntityCommand = new RelayCommandOfT<Entity>(CanEditEntity, EditEntity);
            RemoveModCommand = new RelayCommand(CanRemoveEntity, RemoveEntity);
            InstallModCommand = new RelayCommand(CanInstall, Install);
            UninstallModCommand = new RelayCommand(CanUninstall, Uninstall);
            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(CanEditProduct, EditProduct);
            RemoveProductCommand = new RelayCommand(CanRemoveProduct, RemoveProduct);
            DownloadImagesCommand = new RelayCommand(DownloadImages);
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand SaveDataCommand { get; set; }
        public ICommand ImportDataCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand<BusinessType> AddBusinessCommand { get; set; }
        public ICommand EditBusinessCommand { get; set; }
        public ICommand RemoveBusinessCommand { get; set; }
        public ICommand AddModCommand { get; set; }
        public ICommand<Entity> EditEntityCommand { get; set; }
        public ICommand RemoveModCommand { get; set; }
        public ICommand InstallModCommand { get; set; }
        public ICommand UninstallModCommand { get; set; }
        public ICommand DownloadImagesCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand RemoveProductCommand { get; set; }

        public void CheckForUpdates()
        {
            if (UpdateService.CheckForUpdates())
            {
                _view.ShowMessage("A newer version is available");
            }
        }

        private void LoadData()
        {
            CurrentAction = "Loading data";
            _dataService.Load(@".\Data");

            _countries.Clear();
            _countries.AddRange(_dataService.GetCountries().OrderBy(x => x.Name));

            _products.Clear();
            _products.AddRange(_dataService.GetProducts());

            _businesses.Clear();
            _businesses.AddRange(_dataService.GetBusinesses());

            _mods.Clear();
            _mods.AddRange(_dataService.GetMods());

            CurrentAction = "Loaded data";
        }

        private void GroupBusinesses(string property)
        {
            Businesses.GroupDescriptions.Clear();
            if (property == "None")
                return;
            Businesses.GroupDescriptions.Add(new PropertyGroupDescription(property));
        }

        private void SaveData()
        {
            CurrentAction = "Saving data";
            _dataService.Save(@".\Data", _products, _businesses, _mods);
            CurrentAction = "Saved data";
        }

        private async void ImportData()
        {
            var svc = new ImportService(_businesses, _products, _mods, _countries, UpdateProgress);
            await svc.ImportProducts("ShopProducts.txt");
            await svc.ImportProducts("RestaurantProducts.txt");
            await svc.ImportAirlines("Airlines.txt");
            await svc.ImportAVFuelSuppliers("AviationFuelSuppliers.txt");
            await svc.ImportBanks("Banks.txt");
            await svc.ImportContractors("Contractors.txt");
            await svc.ImportFranchises("Restaurants.txt");
            await svc.ImportFranchises("Shops.txt");
            CurrentAction = "Updating visuals";
            await Task.Delay(1);
            Products.Refresh();
            Businesses.Refresh();
            Mods.Refresh();
            CurrentAction = "Import done";
        }

        public bool CanExit()
        {
            if (IsModified())
            {
                var result = View.AskYesNoCancelConfirmation("Save changes. Yes = Save, No = Discard, Cancel = Don't exit");
                if (!result.HasValue)
                    return false;
                if (result.Value)
                {
                    SaveData();
                    return true;
                }

                return true;
            }
            return true;
        }

        private void Exit()
        {

            CanExit();
            App.Current.Shutdown();
        }

        private void AddBusiness(BusinessType type)
        {
            CurrentAction = "Adding business";
            Business entity = null;
            switch (type)
            { 
                case BusinessType.Airline:
                    entity = new Airline();
                    entity.Name = "<New airline>";
                    break;
                case BusinessType.AviationFuelSupplier:
                    entity = new AviationFuelSupplier();
                    entity.Name = "<New aviation fuel supplier>";
                    break;
                case BusinessType.Bank:
                    entity = new Bank();
                    entity.Name = "<New bank>";
                    break;
                case BusinessType.Contractor:
                    entity = new Contractor();
                    entity.Name = "<New contractor>";
                    break;
                case BusinessType.Franchise:
                    entity = new Franchise();
                    entity.Name = "<New franchise>";
                    break;
                default:
                    HandleError("Unknown business type. Cannot add");
                    break;
            }

            _businesses.Add(entity);
            Businesses.Refresh();
            CurrentBusiness = entity;
            EditBusiness();
            CurrentAction = "Added business";
        }

        private bool CanRemoveBusiness()
        {
            return CurrentBusiness != null;
        }

        private void RemoveBusiness()
        {
            if (View.AskOkCancelConfirmation(string.Format("Remove '{0}'?", CurrentBusiness.Name)))
            {
                CurrentBusiness.Status = EntityStatus.Deleted;
                Businesses.Remove(CurrentBusiness);
            }
        }

        private void AddMod()
        {
            var mod = new Mod { Name = "<new mod>" };

            _mods.Add(mod);
            Mods.Refresh();
            CurrentTreeEntity = mod;
            EditMod();
        }

        private bool CanRemoveEntity()
        {
            return CurrentTreeEntity != null;
        }

        private void RemoveEntity()
        {
            if (CurrentTreeEntity is Mod)
            {
                if (View.AskOkCancelConfirmation(string.Format("Remove '{0}'?", CurrentTreeEntity.Name)))
                {
                    CurrentTreeEntity.Status = EntityStatus.Deleted;
                    Mods.Remove(CurrentTreeEntity as Mod);
                }
            }
            if (CurrentTreeEntity is Business)
            {
                if (View.AskOkCancelConfirmation(string.Format("Remove '{0}' from '{1}'?", CurrentTreeEntity.Name, ParentTreeEntity.Name)))
                {
                    ((Mod)ParentTreeEntity).Businesses.Remove(CurrentTreeEntity as Business);
                }
            }
            if (CurrentTreeEntity is Product)
            {
                if (View.AskOkCancelConfirmation(string.Format("Remove '{0}' from '{1}'?", CurrentTreeEntity.Name, ParentTreeEntity.Name)))
                {
                    ((Franchise)ParentTreeEntity).Products.Remove(CurrentTreeEntity as Product);
                }
            }
        }

        private bool CanEditBusiness()
        {
            return CurrentBusiness != null;
        }

        public void EditBusiness()
        {
            _view.EditBusiness(CurrentBusiness, _products, _countries);
        }

        private bool CanEditEntity(Entity entity)
        {
            return entity != null;
        }

        private void EditMod()
        {
            if (CurrentTreeEntity is Mod && CurrentTreeEntity != Mod.UnknownMod)
                _view.EditMod(CurrentTreeEntity as Mod, _businesses);
        }

        private bool CanEditProduct()
        {
            return CurrentProduct != null;
        }

        private void EditProduct()
        {
            _view.EditProduct(CurrentProduct, _businesses.Where(x => x.Type == BusinessType.Franchise).Cast<Franchise>().ToList());
        }

        private void AddProduct()
        {
            var product = new Product { Name = "<new product>" };

            _products.Add(product);
            Products.Refresh();
            CurrentProduct = product;
            EditProduct();
        }

        private bool CanRemoveProduct()
        {
            return CurrentProduct != null;
        }

        private void RemoveProduct()
        {
            var _businessesHavingProduct = _businesses.Where(x => x.Type == BusinessType.Franchise).Cast<Franchise>().Where(x => x.Products.Contains(CurrentProduct)).ToList();

            if (View.AskOkCancelConfirmation(string.Format("Remove '{0}' (is used by {1} franchises)?", CurrentProduct.Name, _businessesHavingProduct.Count)))
            {
                CurrentProduct.Status = EntityStatus.Deleted;                
                _businessesHavingProduct.ForEach(x => x.Products.Remove(CurrentProduct));
                Products.Remove(CurrentProduct);
                Businesses.Refresh();
            }
        }

        private bool CanInstall()
        {
            return CurrentTreeEntity != null && CurrentTreeEntity is Mod;
        }

        private void Install()
        {
            try
            {
                var selectedMod = CurrentTreeEntity as Mod;
                if (selectedMod != null && selectedMod != Mod.UnknownMod)
                    InstallService.Install(selectedMod);
                else
                {
                    foreach(Mod mod in Mods)
                        InstallService.Install(mod);
                }
            }
            catch (Exception ex)
            {
                View.ShowError(ex.Message);
            }
        }

        private bool CanUninstall()
        {
            return CurrentTreeEntity != null && CurrentTreeEntity is Mod;
        }

        private void Uninstall()
        {
            var selectedMod = CurrentTreeEntity as Mod;
            if (selectedMod != null && selectedMod != Mod.UnknownMod)
                InstallService.Uninstall(selectedMod);
            else
            {
                foreach (Mod mod in Mods)
                    InstallService.Uninstall(mod);
            }
        }

        private void DoDownload(List<Business> businessWorklist, List<Product> productWorklist, Action<int, int, string> updateCallback, Action<string> ErrorCallback)
        {
            var total = businessWorklist.Count + productWorklist.Count;
            var counter = 0;
            foreach (var business in businessWorklist)
            {
                counter++;
                updateCallback(total, counter, string.Format("Downloading image for '{0}' [{1}/{2}]", business.Name, counter, total));
                var result = DownloadService.DownloadBusinessLogo(business, @".\Data\Images\");
                if (!string.IsNullOrWhiteSpace(result))
                    ErrorCallback(result);
            }
            foreach (var product in productWorklist)
            {
                counter++;
                updateCallback(total, counter, string.Format("Downloading image for '{0}' [{1}/{2}]", product.Name, counter, total));
                var result = DownloadService.DownloadProductLogo(product, @".\Data\Images\");
                if (!string.IsNullOrWhiteSpace(result))
                    ErrorCallback(result);
            }
        }

        private async void DownloadImages()
        { 
            var businessWorklist = new List<Business>();
            var productWorklist = new List<Product>();
            if (CurrentTreeEntity is Mod)
            {
                var mod = (Mod)CurrentTreeEntity;
                if (mod == Mod.UnknownMod)
                    businessWorklist = _businesses;
                else
                    businessWorklist = mod.Businesses.ToList();
            }
            if (CurrentTreeEntity is Business)
            {
                var business = (Business)CurrentTreeEntity;
                businessWorklist = new List<Business> { business };
            }
            if (CurrentTreeEntity is Product)
            {
                businessWorklist = new List<Business>();
                var product = (Product)CurrentTreeEntity;
                productWorklist = new List<Product> { product };
            }
            else
            {
                var franchises = businessWorklist.Where(x => x.Type == BusinessType.Franchise).Cast<Franchise>().ToList();
                foreach(var franchise in franchises)
                {
                    foreach(var product in franchise.Products)
                    {
                        if (!productWorklist.Contains(product))
                            productWorklist.Add(product);

                    }
                }
            }
            if (businessWorklist == null || productWorklist == null)
                return;
            await Task.Run(() => DoDownload(businessWorklist, productWorklist, UpdateProgress, View.ShowError));
        }

        #endregion

        #region Filter

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set 
            { 
                SetProperty(ref _nameFilter, value);
                Businesses.Refresh();
            }
        }

        private string _descriptionFilter;
        public string DescriptionFilter
        {
            get { return _descriptionFilter; }
            set
            {
                SetProperty(ref _descriptionFilter, value);
                Businesses.Refresh();
            }
        }

        private string _ceoFilter;
        public string CeoFilter
        {
            get { return _ceoFilter; }
            set
            {
                SetProperty(ref _ceoFilter, value);
                Businesses.Refresh();
            }
        }

        private string _countryFilter;
        public string CountryFilter
        {
            get { return _countryFilter; }
            set
            {
                SetProperty(ref _countryFilter, value);
                Businesses.Refresh();
            }
        }

        private string _authorFilter;
        public string AuthorFilter
        {
            get { return _authorFilter; }
            set
            {
                SetProperty(ref _authorFilter, value);
                Businesses.Refresh();
            }
        }

        private string _versionFilter;
        public string VersionFilter
        {
            get { return _versionFilter; }
            set
            {
                SetProperty(ref _versionFilter, value);
                Businesses.Refresh();
            }
        }

        private string _statusFilter;
        public string StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                SetProperty(ref _statusFilter, value);
                Businesses.Refresh();
            }
        }

        private string _typeFilter;
        public string TypeFilter
        {
            get { return _typeFilter; }
            set
            {
                SetProperty(ref _typeFilter, value);
                Businesses.Refresh();
            }
        }

        #endregion
    }
}
