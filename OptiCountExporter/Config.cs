using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OptiCountExporter
{
    public class Config : INotifyPropertyChanged
    {
        private string _siteCell;
        private string _dateCell;
        public event PropertyChangedEventHandler PropertyChanged;

        public string SiteCell
        {
            get { return _siteCell; }
            set
            {
                if (_siteCell != value)
                {
                    _siteCell = value;
                    OnPropertyChanged("SiteCell");
                }
            }
        }

        public string DateCell
        {
            get { return _dateCell; }
            set
            {
                // do not trigger change event if values are the same
                if (Equals(value, _dateCell)) return;
                _dateCell = value;
                OnPropertyChanged("DateCell");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
