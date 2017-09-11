using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ACEOMM.Domain.Model
{
    public class Entity : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName != "Status" && Status == EntityStatus.Unchanged)
                Status = EntityStatus.Modified;

            field = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public Entity()
        {
            Id = Guid.NewGuid();
            Status = EntityStatus.New;
            IsEditAllowed = true;
        }

        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private bool _isEditEAllowed;
        public bool IsEditAllowed
        {
            get { return _isEditEAllowed; }
            set { SetProperty(ref _isEditEAllowed, value); }
        }

        private EntityStatus _status;
        public EntityStatus Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { SetProperty(ref _author, value); }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }

        private string _notes;
        public string Notes 
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }
    }
}
