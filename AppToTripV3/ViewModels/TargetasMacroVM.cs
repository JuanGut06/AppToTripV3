using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppToTripV3.Services;
using AppToTripV3.Models;
using AppToTripV3.Views;
using Plugin.SimpleAudioPlayer;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System.Net;


namespace AppToTripV3.ViewModels
{

    public class TargetasMacroVM : BaseViewModel
    {

        private readonly LogicaWS? logicaWS;
        private readonly GeoLocation? geoLocation;
        public readonly IPageServicio? pageServicio;

        ISimpleAudioPlayer? player;

        private string? _ImagenMacro;
        private string? _NombreMacro;
        private string? _Cantidad;
        private string? _idsCircuito;

        private string? idCircuitoMacro_;
        private string? TextoMacro_;

        private int _carouselPosition;

        private ObservableCollection<CircuitoMacroSitios>? _listaCircuitoMacroSitios;

      
        public int CarouselPosition
        {
            get { return _carouselPosition; }
            set { _carouselPosition = value; OnPropertyChanged(); }
        }
        public string ImagenMacro
        {
            get { return _ImagenMacro; }
            set { _ImagenMacro = value; OnPropertyChanged(); }
        }
        public string NombreMacro
        {
            get { return _NombreMacro; }
            set { _NombreMacro = value; OnPropertyChanged(); }
        }

        public string Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; OnPropertyChanged(); }
        }


        public ObservableCollection<CircuitoMacroSitios> ListaMacroSitios
        {
            get { return _listaCircuitoMacroSitios; }
            set { _listaCircuitoMacroSitios = value; OnPropertyChanged(nameof(ListaMacroSitios)); }
        }


        /*Botones del mapa*/
        public ICommand? BtnMenuLateral { get; }
        public ICommand? BtnBuscarIr { get; }
        public ICommand? BtnMiUbicacion { get; }
        public ICommand? BtnVolver { get; }

        public ICommand? BtnMasCircuitos { get; }

        public ICommand? BtnDescripicionAudio { get; }

        public TargetasMacroVM(string nombreMacro)
        {
            IsBusy = true;

            //MapaCircuitos = map;
            NombreMacro = nombreMacro;

            logicaWS = new LogicaWS();
            geoLocation = new GeoLocation();
            pageServicio = new PageServicio();
            player = CrossSimpleAudioPlayer.Current;
            NombreUsuario = Preferences.Get("nombre_persona", "%20");
            CorreoUsuario = Preferences.Get("correo_persona", "%20");
            EstadoUsuario = Preferences.Get("estado_persona", "%20");


            BtnVolver = new Command(() => BtnVolver_Mtd());
            BtnBuscarIr = new Command(() => BtnVolver_Mtd());
            BtnMasCircuitos = new Command(() => BtnMasCircuitos_Mtd());
            BtnDescripicionAudio = new Command(() => BtnDescripicionAudio_Mtd());
            BtnMenuLateral = new Command(async () => await BtnMenuLateral_Mtd());
            BtnMiUbicacion = new Command(async () => await BtnMiUbicacion_Mtd());


            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            CargarInfoPrincipalMacros_Mtd(nombreMacro, IdiomaBase);
            LocationPermisosUbicacion();
        }

        /* Inicio informacion carrusel de circuitos y pines  */
        async void CargarInfoPrincipalMacros_Mtd(string nombreMacro, string lenguaje)
        {
            ListaMacroSitios = new ObservableCollection<CircuitoMacroSitios>();

            if (!StateConexion)
            {
                try
                {
                    IsBusy = true;
                    string urli = logicaWS.Movile_Consulta_Macro_Detalle_Metodo(nombreMacro, lenguaje);
                    string Json_Procedimiento_Vgl = await logicaWS.Conection(urli);
                    JObject jsonObject = JObject.Parse(Json_Procedimiento_Vgl);

                    string resultado = jsonObject.GetValue("Table").ToString();
                    JArray jsonObject1 = JArray.Parse(resultado);

                    string resultado1 = jsonObject.GetValue("Table1").ToString();
                    JArray jsonObject2 = JArray.Parse(resultado1);

                    if (jsonObject1.Count == 0 || jsonObject2.Count == 0)
                    {
                        await pageServicio.DisplayAlert("AppToTrip", "No tenemos circuitos para este macro", "Ok");
                    }
                    else
                    {
                        // Guarda la informacion de la macro como tal 
                        try
                        {
                            foreach (JObject item in jsonObject1)
                            {

                                CircuitoMacro circuito = new CircuitoMacro(
                                    item.GetValue("id_circuito_macro").ToString(),
                                    item.GetValue("id_circuito").ToString(),
                                    item.GetValue("nombre_circuito").ToString(),
                                    item.GetValue("descripcion_circuito").ToString(),
                                    item.GetValue("imagen").ToString(),
                                    item.GetValue("tiempo_estimado").ToString(),
                                    item.GetValue("cantidad").ToString());
                                ImagenMacro = item.GetValue("imagen").ToString();
                                NombreMacro = item.GetValue("nombre_circuito").ToString();
                                Cantidad = item.GetValue("cantidad").ToString();

                                idCircuitoMacro_ = item.GetValue("id_circuito").ToString();
                                TextoMacro_ = item.GetValue("descripcion_circuito").ToString();
                            }
                        }
                        catch
                        { }

                        //Guarda la informacion de los circuitos que componen ese macro
                        try
                        {
                            _idsCircuito = "";
                            //MapaCircuitos.Pins.Clear();
                            var latitudes = new List<double>();
                            var longitudes = new List<double>();

                            Polyline polyline1 = new Polyline() { StrokeColor = Colors.Blue, StrokeWidth = 2 };
                            foreach (JObject item in jsonObject2)
                            {
                                CircuitoMacroSitios circuitoMSitios = new CircuitoMacroSitios(
                                    item.GetValue("id_circuito_macro").ToString(),
                                    item.GetValue("fk_ciudad").ToString(),
                                    item.GetValue("ciudad").ToString(),
                                    item.GetValue("id_circuito").ToString(),
                                    item.GetValue("latitud").ToString(),
                                    item.GetValue("longitud").ToString(),
                                    item.GetValue("orden_macro").ToString());


                                ListaMacroSitios.Add(circuitoMSitios);
                                //Position position = new Position((double)item.GetValue("latitud").Value<decimal>(), (double)item.GetValue("longitud").Value<decimal>());
                                //latitudes.Add(position.Latitude);
                                //longitudes.Add(position.Longitude);

                                //Pin pin = new Pin
                                //{
                                //    Type = PinType.Place,
                                //    Tag = circuitoMSitios.id_circuito,
                                //    Label = circuitoMSitios.ciudad,
                                //    Position = position
                                //};
                                //pin.
                                //pin.Icon = BitmapDescriptorFactory.FromBundle("ic_ubicacion_azul.png");
                                //polyline1.Positions.Add(position);


                                //MapaCircuitos.Pins.Add(pin);
                                _idsCircuito = _idsCircuito + "," + circuitoMSitios.id_circuito;


                            }

                            //MapaCircuitos.Polylines.Add(polyline1);

                            double lowestLat = latitudes.Min();
                            double highestLat = latitudes.Max();
                            double lowestLong = longitudes.Min();
                            double highestLong = longitudes.Max();
                            double finalLat = (lowestLat + highestLat) / 2;
                            double finalLong = (lowestLong + highestLong) / 2;

                            double distance = Location.CalculateDistance(new Location(lowestLat, lowestLong), new Location(highestLat, highestLong), DistanceUnits.Kilometers);

                            //MapaCircuitos.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(finalLat, finalLong), Distance.FromKilometers(distance)));

                        }
                        catch
                        {

                        }
                    }
                }
                catch (Exception)
                {
                    IsBusy = false;
                    //await pageServicio.PopAsync();
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
        }


        /*Inicio Botones carrusel de imagenes*/
        public void BtnMasCircuitos_Mtd()
        {
            try
            {
                _idsCircuito = _idsCircuito.Replace(',', ' ');
                _idsCircuito = _idsCircuito.Trim();
                _idsCircuito = _idsCircuito.Replace(' ', ',');

                RootPage.Detail = new NavigationPage(new TargetasCircuitos(NombreMacro, null, null, "2", _idsCircuito));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async void BtnDescripicionAudio_Mtd()
        {
            //var assembly = typeof(App).GetTypeInfo().Assembly;
            //Stream audioStream = assembly.GetManifestResourceStream("AppToTrip.Recursos.fondo.mp3");
            //var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            //audio.Load(audioStream);
            //audio.Volume = 0.1;

            try
            {
                IsBusy = true;
                //audio.Play();
                string audioUrl = logicaWS.Movile_Audio_Metodo("circuito", idCircuitoMacro_, "descripcion_circuito", IdiomaBase);
                //await CrossMediaManager.Current.Play(audioUrl);
                WebClient wc = new WebClient();
                Stream fileStream = wc.OpenRead(audioUrl);
                player.Load(fileStream);
                player.Volume = 1.0;
                player.Play();
                bool resp = await pageServicio.DisplayAlert(NombreMacro, TextoMacro_, "Detener", "Continuar");

                if (resp)
                    //await CrossMediaManager.Current.Stop();
                    CrossSimpleAudioPlayer.Current.Stop();

                IsBusy = false;
            }

            catch (Exception ex)
            {
                Console.WriteLine("EL ERROR:" + ex);
            }
            // audio.Stop();
        }

        private async Task BtnMenuLateral_Mtd()
        {
            //await Application.Current.MainPage.Navigation.PushPopupAsync(new MenuPopUp("1", null));
        }

        private async Task BtnMiUbicacion_Mtd()
        {
            await geoLocation.GetLocationGPS();
            //MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(geoLocation.Lat, geoLocation.Log), Distance.FromKilometers(0.5));
            //MapaCircuitos.MoveToRegion(mapSpan);
        }

        void LocationPermisosUbicacion()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var RespuestaLocation = await geoLocation.GetLocationGPS();
                    if (RespuestaLocation)
                    {
                        //MapaCircuitos.IsIndoorEnabled = true;
                        //MapaCircuitos.MyLocationEnabled = true;
                        //MapaCircuitos.UiSettings.MyLocationButtonEnabled = false;
                    }
                    else
                    {
                        await pageServicio.DisplayAlert("Atencion", "Debe conceder los permisos de ubicación", "ok");
                        await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    }
                }
                catch (Exception) { }
            });
        }

        void BtnVolver_Mtd()
        {
            RootPage.Detail = new NavigationPage(new Filtros());
        }


    }

}