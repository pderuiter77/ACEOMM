using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ACEOMM.Domain.Model
{
    public class Logo : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public Logo()
        {
            LocalFilename = string.Empty;
        }
        private string _remoteUrl;
        public string RemoteUrl
        {
            get { return _remoteUrl; }
            set { SetProperty(ref _remoteUrl, value); }
        }

        private string _localFilename;
        public string LocalFilename
        {
            get { return _localFilename; }
            set { SetProperty(ref _localFilename, value); }
        }

        public bool HasUrl()
        {
            return !string.IsNullOrWhiteSpace(RemoteUrl);
        }

        public bool HasFilename()
        {
            return !string.IsNullOrWhiteSpace(LocalFilename);
        }
    }
}
