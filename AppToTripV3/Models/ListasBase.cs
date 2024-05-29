using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppToTripV3.Models
{
    public class ListasBase : INotifyPropertyChanged
    {

        private string Id_;
        private string url_;
        private string Nombre_;
        private string Descripcion_;

        public string Id
        {
            get { return Id_; }
            set { Id_ = value; OnPropertyChanged(nameof(Id_)); }
        }

        public string Url
        {
            get { return url_; }
            set { url_ = value; OnPropertyChanged(nameof(Url)); }
        }

        public string Nombre
        {
            get { return Nombre_; }
            set { Nombre_ = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public string Descripcion
        {
            get { return Descripcion_; }
            set { Descripcion_ = value; OnPropertyChanged(nameof(Descripcion)); }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
