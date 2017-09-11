using ACEOMM.Domain.Model.Businesses;
using ACEOMM.Services;
using ACEOMM.UI.Commands;
using ACEOMM.UI.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace ACEOMM.UI.ViewModel
{
    public class ProductWindowViewModel : ViewModel
    {
        public ProductWindowViewModel(IView view)
            : base(view)
        {
            _franchises = new List<Franchise>();
            Franchises = new ListCollectionView(_franchises);
            Franchises.Filter = entity =>
            {
                return !ProductFranchises.Contains(entity);
            };

            _productFranchises = new List<Franchise>();
            ProductFranchises = new ListCollectionView(_productFranchises);

            Franchises.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            ProductFranchises.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            _franchiseTypes = Enum.GetValues(typeof(FranchiseType)).Cast<FranchiseType>().ToList();
            FranchiseTypes = new ListCollectionView(_franchiseTypes);

            InitializeCommands();
        }

        private List<FranchiseType> _franchiseTypes;
        public ListCollectionView FranchiseTypes { get; private set; }

        private Product _current;
        public Product Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        private Franchise _selectedProductFranchise;
        public Franchise SelectedProductFranchise
        {
            get { return _selectedProductFranchise; }
            set { SetProperty(ref _selectedProductFranchise, value); }
        }

        private Franchise _selectedFranchise;
        public Franchise SelectedFranchise
        {
            get { return _selectedFranchise; }
            set { SetProperty(ref _selectedFranchise, value); }
        }

        private List<Franchise> _franchises;
        public ListCollectionView Franchises { get; private set; }

        private List<Franchise> _productFranchises;
        public ListCollectionView ProductFranchises { get; private set; }

        private string _logoPath;
        public string LogoPath
        {
            get { return _logoPath; }
            set { SetProperty(ref _logoPath, value); }
        }

        private string _imageBasePath;

        #region Commands
        private void InitializeCommands()
        {
            AddProductToFranchiseCommand = new RelayCommand(CanAddProductToFranchise, AddProductToFranchise);
            RemoveProductFromFranchiseCommand = new RelayCommand(CanRemoveProductFromFranchise, RemoveProductFromFranchise);
            DownloadImageCommand = new RelayCommand(CanDownloadImage, DownloadImage);
            SelectImageCommand = new RelayCommand(SelectImage);
            OpenUrlCommand = new RelayCommand(CanOpenUrl, OpenUrl);
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

        private bool CanAddProductToFranchise()
        {
            return SelectedFranchise != null;
        }

        private void AddProductToFranchise()
        {
            if (Current.Type != FranchiseType.Unknown)
            {
                if (SelectedFranchise.FranchiseType != Current.Type)
                {
                    View.ShowMessage(string.Format("This product is for a {0}, but this franchise is a {1}", Current.Type, SelectedFranchise.FranchiseType));
                    return;
                }
            }

            SelectedFranchise.Products.Add(Current);
            _productFranchises.Add(SelectedFranchise);
            ProductFranchises.Refresh();
            Franchises.Refresh();
        }

        private bool CanRemoveProductFromFranchise()
        {
            return SelectedProductFranchise != null;
        }

        private void RemoveProductFromFranchise()
        {
            SelectedProductFranchise.Products.Remove(Current);
            ProductFranchises.Remove(SelectedProductFranchise);
            ProductFranchises.Refresh();
            Franchises.Refresh();
        }

        private bool CanDownloadImage()
        {
            return Current.Logo.HasUrl();
        }

        private void DownloadImage()
        {
            try
            {
                DownloadService.DownloadProductLogo(Current, @".\Data\Images\");
            }
            catch (Exception ex)
            {
                View.ShowError(ex.Message);
            }
            UpdateLogoPath();
        }

        public ICommand AddProductToFranchiseCommand { get; private set; }
        public ICommand RemoveProductFromFranchiseCommand { get; private set; }
        public ICommand DownloadImageCommand { get; private set; }
        public ICommand SelectImageCommand { get; private set; }
        public ICommand OpenUrlCommand { get; private set; }
        #endregion

        private void UpdateLogoPath()
        {
            LogoPath = Path.GetFullPath(Path.Combine(_imageBasePath, Current.Logo.LocalFilename));
        }

        public void Initialize(Product entity, IEnumerable<Franchise> franchises)
        {
            _imageBasePath = Path.GetFullPath(@".\Data\Images\Products\");
            Current = entity;
            _productFranchises.Clear();
            _productFranchises.AddRange(franchises.Where(x => x.Products.Contains(entity)).ToList());
            ProductFranchises.Refresh();
            _franchises.Clear();
            _franchises.AddRange(franchises);
            Franchises.Refresh();
            UpdateLogoPath();
        }
    }
}
