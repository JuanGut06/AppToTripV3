using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppToTripV3.Services;
using Newtonsoft.Json.Linq;
using AppToTripV3.Views;

namespace AppToTripV3.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
        private bool RespuestaLocation;
        private GeoLocation _GeoLocation;
        private LogicaWS WS;
        private bool _state;
        private bool _stateprogress;

        public string pais;
        public string ciudad;
        private string paisCerca;
        private string ciudadCerca;
        private string UbicadoLabel;
        private string geocodeAddress;
        public IPageServicio _pageServicio { get; set; }
        public ICommand BtnDescargas { get; }

        public bool State
        {
            get { return _state; }
            set { _state = value; OnPropertyChanged(nameof(State)); }

        }
        public bool StateProgress
        {
            get { return _stateprogress; }
            set { _stateprogress = value; OnPropertyChanged(nameof(StateProgress)); }

        }
        public ICommand ReintentarConexion { get; }

        public SplashViewModel()
        {
            Title = "Splash";

            WS = new LogicaWS();
            _GeoLocation = new GeoLocation();
            _pageServicio = new PageServicio();

            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            ReintentarConexion = new Command(async () => await ConsultarPerfilAsync());
            State = !Conexion_Internet_Mto();
            StateProgress = Conexion_Internet_Mto();
            //BtnDescargas = new Command(() => BtnDescargas_Mtd());
            LocationPermisosUbicacion();

        }


        bool Conexion_Internet_Mto()
        {
            //new NetWorkState().IHaveInternet();
            //if (NetWorkState.isConnect)
            //{

            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return HasInternetConnection();
        }

        public static bool HasInternetConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else if (current == NetworkAccess.ConstrainedInternet || current == NetworkAccess.Local)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        void LocationPermisosUbicacion()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {

                RespuestaLocation = await _GeoLocation.GetLocationGPS();
                if (RespuestaLocation)
                {
                    await ConsultarPerfilAsync();
                    GetDirectionAsync();
                }
                else
                {
                    var a = await _pageServicio.DisplayAlert("Atención", "Esta App no puede funcionar sin permisos de ubicación", "Reintentar", "Cerrar");
                    if (a)
                    {
                        LocationPermisosUbicacion();
                    }
                    else
                    {
                        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    }
                }
            });
        }

        public async void GetDirectionAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                double lat;
                double lon;

                if (location != null)
                {
                    lat = location.Latitude;
                    lon = location.Longitude;
                }
                else
                {
                    //Nos devolvera una posicision conosida que ya tenga en cache del telefono
                    var knowLocation = await Geolocation.GetLastKnownLocationAsync();
                    lat = knowLocation.Latitude;
                    lon = knowLocation.Longitude;
                }


                var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    Console.WriteLine(geocodeAddress);
                    paisCerca = placemark.CountryName;
                    ciudadCerca = placemark.Locality;
                    UbicadoLabel = placemark.Locality + ", " + placemark.CountryName;
                    Preferences.Set("paisCerca", paisCerca);
                    Preferences.Set("ciudadCerca", ciudadCerca);
                    Preferences.Set("UbicadoLabel", UbicadoLabel);

                }
                else
                {
                    UbicadoLabel = "No se encontró su ubicación";
                }
            }
            catch (FeatureNotSupportedException EX)
            {
                // Feature not supported on device
                UbicadoLabel = "No se encontró su ubicación";
            }
            catch (Exception EX)
            {
                // Handle exception that may have occurred in geocoding
                UbicadoLabel = "No se encontró su ubicación";
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task ConsultarPerfilAsync()
        {
            try
            {
                State = !Conexion_Internet_Mto();
                StateProgress = Conexion_Internet_Mto();
                if (StateProgress)
                {
                    var myValue = Preferences.Get("correo_persona", "%20");
                    string id = WS.ObtenerToken();
                    string jsonProcedimiento = await WS.Conection(WS.Movile_Consulta_Personas_Metodo(myValue, id));
                    ValidarLogin(JObject.Parse(jsonProcedimiento));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Pasar();
            }
        }

        void ValidarLogin(JObject jsonObject)
        {
            try
            {
                string attrib1Value = jsonObject["Table"][0]["resultado"].Value<string>();
                Pasar();
            }
            catch
            {
                try
                {
                    Preferences.Set("nombre_persona", jsonObject["Table"][0]["nombres_persona"].Value<string>());
                    Preferences.Set("estado_persona", jsonObject["Table"][0]["estado"].Value<string>());

                    if (jsonObject["Table"][0]["estado"].Value<string>().Equals("2"))
                    {
                        Preferences.Set("id_circuito", jsonObject["Table1"][0]["fk_circuito"].Value<string>());
                        Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                        RootPage.Detail = new NavigationPage(new RecorridoMapa());
                        //Application.Current.MainPage = new NavigationPage(new RecorridoMapa());
                    }
                    else
                    {

                        Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Pasar();
                }
            }
        }

        private void Pasar()
        {
            //Application.Current.MainPage = new NavigationPage(new MasterHomePage());
            Application.Current.MainPage = new NavigationPage(new Login());

        }
        //async void BtnDescargas_Mtd()
        //{
        //    if (File.Exists(WS.GetJsonFilePath("MURALES DE TIBASOSA")))
        //    {
        //        Application.Current.MainPage = new NavigationPage(new CircuitosOffline("2"));
        //    }
        //    else
        //    {
        //        await _pageServicio.DisplayAlert("AppToTrip", "No has descargado ningun circuito", "Aceptar");
        //    }

        //}
    }
}