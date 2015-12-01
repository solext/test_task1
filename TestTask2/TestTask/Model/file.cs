using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TestTask.ViewModel;

namespace TestTask.Model
{

    public class file : ViewModelBase
    {

        string _name = string.Empty;
        string _hash = string.Empty;
        string _path = string.Empty;
        string _dateCreated = string.Empty;
        string _dateModified = string.Empty;
        string _extension = string.Empty;
        string _signature = string.Empty;
        bool _ischeck = false;
        BitmapSource _icon;

        public file()
        {

        }
        public file(string name, string path, string dateModofied, string dateCreated, string extension, bool ischek, string hash = "", BitmapSource icon = null, string signature = "")
        {
            this._name = name;
            this._hash = hash;
            this._icon = icon;
            this._path = path;
            this._dateModified = dateModofied;
            this._dateCreated = dateCreated;
            this._extension = extension;
            this._signature = signature;
            this._ischeck = ischek;
        }
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Hash
        {
            get
            {
                return _hash;
            }
            set
            {
                _hash = value;
                OnPropertyChanged("Hash");
            }
        }
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }
        public BitmapSource Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged("Icon");
            }
        }
        public string DateModified
        {
            get
            {
                return _dateModified;
            }
            set
            {
                _dateModified = value;
                OnPropertyChanged("DateModified");
            }
        }
        public string DateCreated
        {
            get
            {
                return _dateCreated;
            }
            set
            {
                _dateCreated = value;
                OnPropertyChanged("DateCreated");
            }
        }
        public string Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
                OnPropertyChanged("Extension");
            }
        }
        public string Signature
        {
            get
            {
                return _signature;
            }
            set
            {
                _signature = value;
                OnPropertyChanged("Signature");
            }
        }
        public bool ischeck
        {
            get
            {
                return _ischeck;
            }
            set
            {
                _ischeck = value;
                OnPropertyChanged("ischeck");
            }
        }
        public class DelegateCommand : ICommand
        {

            Predicate<object> canExecute;
            Action<object> execute;

            public DelegateCommand(Predicate<object> _canexecute, Action<object> _execute)
                : this()
            {
                canExecute = _canexecute;
                execute = _execute;
            }

            public DelegateCommand()
            {

            }

            public bool CanExecute(object parameter)
            {
                return canExecute == null ? true : canExecute(parameter);
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                execute(parameter);
            }
        }
    }
}
