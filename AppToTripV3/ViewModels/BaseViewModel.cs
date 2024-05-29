using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AppToTripV3.Interface;
using AppToTripV3.Views;

namespace AppToTripV3.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /*
               * @brief Me permite mostrar una página de detalle, mostrando la flecha para devolverse
               */
        public NavigationPage RootMainPage { get => Application.Current.MainPage as NavigationPage; }

        /*
        * @brief Me permite mostrar una página principal
        */
        public MasterHomePage RootPage { get => RootMainPage.RootPage as MasterHomePage; }

        //string _idioma = DependencyService.Get<ILenguaje>().Id_Lenguaje(); //"es";
        string _idioma = "es";

        string _cultura = "es-ES";  //DependencyService.Get<ILenguaje>().Id_Cultura();

        public string _NombreUsuario;

        public string _CorreoUsuario;

        public string _EstadoUsuario;

        bool isBusy = false;

        string title = string.Empty;

        private bool _stateconexion;

        public bool StateConexion
        {
            get { return _stateconexion; }
            set { _stateconexion = value; OnPropertyChanged(nameof(StateConexion)); }
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public string IdiomaBase
        {
            get { return _idioma; }
            set { SetProperty(ref _idioma, value); }
        }

        public string CulturaBase
        {
            get { return _cultura; }
            set { SetProperty(ref _cultura, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        public string NombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; OnPropertyChanged(nameof(NombreUsuario)); }
        }
        public string CorreoUsuario
        {
            get { return _CorreoUsuario; }
            set { _CorreoUsuario = value; OnPropertyChanged(nameof(CorreoUsuario)); }
        }
        public string EstadoUsuario
        {
            get { return _EstadoUsuario; }
            set { _EstadoUsuario = value; OnPropertyChanged(nameof(EstadoUsuario)); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
                StateConexion = false;
            else
                StateConexion = true;
        }
    }
}