using ACEOMM.Domain.Model;
using ACEOMM.Domain.Model.Businesses;
using ACEOMM.Services;
using ACEOMM.UI.Commands;
using ACEOMM.UI.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace ACEOMM.UI.ViewModel
{
    public class BusinessWindowViewModel : ViewModel
    {
        public BusinessWindowViewModel(IBusinessView view)
            : base(view)
        {
            _products = new List<Product>();
            Products = new ListCollectionView(_products);
            Products.Filter = entity =>
                {
                    var franchise = Current as Franchise;
                    if (franchise == null)
                        return false;

                    return !franchise.Products.Contains(entity);
                };

            _countries = new List<Country>();
            Countries = new ListCollectionView(_countries);

            _classes = Enum.GetValues(typeof(BusinessClass)).Cast<BusinessClass>().ToList();
            Classes = new ListCollectionView(_classes);

            _bankTypes = Enum.GetValues(typeof(BankType)).Cast<BankType>().ToList();
            BankTypes = new ListCollectionView(_bankTypes);

            _franchiseTypes = Enum.GetValues(typeof(FranchiseType)).Cast<FranchiseType>().ToList();
            FranchiseTypes = new ListCollectionView(_franchiseTypes);

            InitializeCommands();
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        private Product _selectedFranchiseProduct;
        public Product SelectedFranchiseProduct
        {
            get { return _selectedFranchiseProduct; }
            set { SetProperty(ref _selectedFranchiseProduct, value); }
        }

        private string _logoPath;
        public string LogoPath
        {
            get { return _logoPath; }
            set { SetProperty(ref _logoPath, value); }
        }

        private List<FranchiseType> _franchiseTypes;
        public ListCollectionView FranchiseTypes { get; private set; }

        private List<BankType> _bankTypes;
        public ListCollectionView BankTypes { get; private set; }

        private List<BusinessClass> _classes;
        public ListCollectionView Classes { get; private set; }

        private List<Product> _products;
        public ListCollectionView Products { get; private set; }

        private List<Country> _countries;
        public ListCollectionView Countries { get; private set; }

        private Business _current;
        public Business Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        #region Filter
        private string _productNameFilter;
        public string ProductNameFilter
        {
            get { return _productNameFilter; }
            set
            {
                SetProperty(ref _productNameFilter, value);
                Products.Refresh();
            }
        }
        #endregion

        #region Commands
        private void InitializeCommands()
        {
            SelectImageCommand = new RelayCommand(SelectImage);
            OpenUrlCommand = new RelayCommand(CanOpenUrl, OpenUrl);
            DownloadImageCommand = new RelayCommand(CanDownloadImage, DownloadImage);
            AddProductToFranchiseCommand = new RelayCommand(CanAddProductToFranchise, AddProductToFranchise);
            RemoveProductFromFranchiseCommand = new RelayCommand(CanRemoveProductFromFranchise, RemoveProductFromFranchise);
        }

        public ICommand AddProductToFranchiseCommand { get; private set; }
        public ICommand RemoveProductFromFranchiseCommand { get; private set; }
        public ICommand SelectImageCommand { get; private set; }
        public ICommand OpenUrlCommand { get; private set; }
        public ICommand DownloadImageCommand { get; private set; }

        private string _imageBasePath;

        private bool CanAddProductToFranchise()
        {
            return SelectedProduct != null;
        }

        private void AddProductToFranchise()
        {
            var franchise = Current as Franchise;
            if (franchise == null)
                return;
            if (SelectedProduct.Type != FranchiseType.Unknown)
            {
                if (franchise.FranchiseType != SelectedProduct.Type)
                {
                    View.ShowMessage(string.Format("This product is for a {0}, but this franchise is a {1}", SelectedProduct.Type, franchise.FranchiseType));
                    return;
                }
            }
            franchise.Products.Add(SelectedProduct);
            Products.Refresh();
            
        }

        private bool CanRemoveProductFromFranchise()
        {
            return SelectedFranchiseProduct != null;
        }

        private void RemoveProductFromFranchise()
        {
            var franchise = Current as Franchise;
            if (franchise == null)
                return;

            franchise.Products.Remove(SelectedFranchiseProduct);
            Products.Refresh();
        }

        private void SelectImage()
        {
            var dlg = new OpenFileDialog()
            {
                DefaultExt = ".png",
                Filter = "PNG Files (*.png)|*.png",
                InitialDirectory = _imageBasePath
            };

            if (dlg.ShowDialog().Value)
            {
                if (Path.GetDirectoryName(dlg.FileName) != dlg.InitialDirectory)
                    File.Copy(dlg.FileName, Path.Combine(dlg.InitialDirectory, Path.GetFileName(dlg.FileName)));
                
                Current.Logo.LocalFilename = Path.GetFileName(dlg.FileName);
                UpdateLogoPath();
            }
        }

        private bool CanOpenUrl()
        {
            return Current.Logo.HasUrl();
        }

        private void OpenUrl()
        {
            try
            {
                Process.Start(Current.Logo.RemoteUrl);
            }
            catch (Exception ex)
            {
                View.ShowError(ex.Message);
            }
        }

        private bool CanDownloadImage()
        {
            return Current.Logo.HasUrl();
        }

        private void DownloadImage()
        {
            try
            {
                DownloadService.DownloadBusinessLogo(Current, @".\Data\Images\");
            }
            catch (Exception ex)
            {
                View.ShowError(ex.Message);
            }
            UpdateLogoPath();
        }
        #endregion

        #region Business specific toggles
        public bool IsAirline
        {
            get { return Current.Type == BusinessType.Airline; }
        }

        public bool IsBank
        {
            get { return Current.Type == BusinessType.Bank; }
        }

        public bool IsFranchise
        {
            get { return Current.Type == BusinessType.Franchise; }
        }
        #endregion

        private void UpdateLogoPath()
        {
            LogoPath = Path.GetFullPath(Path.Combine(_imageBasePath, Current.Logo.LocalFilename));
        }

        public void Initialize(Business entity, List<Product> products, List<Country> countries)
        {
            Current = entity;
            _imageBasePath = Path.GetFullPath(string.Format(@".\Data\Images\Businesses\{0}\", Current.Type.ToString()));
            UpdateLogoPath();

            _products.Clear();
            _products.AddRange(products);
            Products.Refresh();

            _countries.Clear();
            _countries.AddRange(countries);
        }
    }
}
