using AppToTripV3.Models;
using AppToTripV3.Services;
using AppToTripV3.Views;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppToTripV3.ViewModels
{
    public class TargetasSitiosVM : BaseViewModel
    {
        private readonly LogicaWS logicaWS;
        private readonly PinesEnumerados pinf;
        private readonly GeoLocation geoLocation;
        public readonly IPageServicio pageServicio;
        ISimpleAudioPlayer player;

        //public Xamarin.Forms.GoogleMaps.Map MapaSitios_ { get; private set; }

        private readonly string _Pais;
        private readonly string _Ciudad;
        private readonly string _Categoria;
        private readonly string _IdCircuito;
        private readonly string Bandera_;

        private string _idsCircuito;


        public ObservableCollection<Sitio> _listaSitios;
        static public List<Pin> pinsList = new List<Pin>();

        public Circuito circuito;
        private int _carouselPosition;

        public ObservableCollection<Sitio> ListaSitios
        {
            get { return _listaSitios; }
            set { _listaSitios = value; OnPropertyChanged(nameof(ListaSitios)); }
        }

        public int CarouselPosition
        {
            get { return _carouselPosition; }
            set
            {
                if (_carouselPosition != value)
                {
                    _carouselPosition = value;
                    OnPropertyChanged(nameof(CarouselPosition));
                }
            }
        }
                

        public ICommand BtnMenuLateral { get; }
        public ICommand BtnBuscarIr { get; }
        public ICommand BtnMiUbicacion { get; }
        public ICommand BtnBuscarHotel { get; }
        public ICommand BtnPlayRecorrido { get; }
        public ICommand BtnDescripicionAudio { get; }
        public ICommand CarruselImgMasInfo_Clicked { get; }
        public ICommand BtnVolver { get; }

        public TargetasSitiosVM(Circuito circuit, string categoria, string pais, string ciudad,
            string Bandera, string idCircuito)
        {
            IsBusy = true;
            //MapaSitios = map;
            circuito = circuit;

            _Pais = pais;
            _Ciudad = ciudad;
            Bandera_ = Bandera;
            _Categoria = categoria;
            _IdCircuito = idCircuito;

            logicaWS = new LogicaWS();
            pinf = new PinesEnumerados();
            geoLocation = new GeoLocation();
            pageServicio = new PageServicio();
            player = CrossSimpleAudioPlayer.Current;
            EstadoUsuario = Preferences.Get("estado_persona", "%20");
            NombreUsuario = Preferences.Get("nombre_persona", "%20");
            CorreoUsuario = Preferences.Get("correo_persona", "%20");

            ListaSitios = new ObservableCollection<Sitio>();

            BtnVolver = new Command(() => BtnVolver_Mtd());
            BtnPlayRecorrido = new Command(() => BtnPlayRecorrido_Mtd());
            BtnDescripicionAudio = new Command(() => BtnDescripicionAudio_Mtd());
            BtnMenuLateral = new Command(async () => await BtnMenuLateral_Mtd());
            BtnMiUbicacion = new Command(async () => await BtnMiUbicacion_Mtd());
            BtnBuscarHotel = new Command(async () => await BtnBuscarHotel_Mtd("BtnHotel"));
            BtnBuscarIr = new Command(() => BtnBuscar_Mtd());
            CarruselImgMasInfo_Clicked = new Command(CarruselImgMasInfo_ClickedMtd);

            MostrarTarjetasSitios_Mto(circuito.id_circuito);
            LocationPermisosUbicacion();
            //RemoverPin(ListaSitios[0].Id_Sitio);
        }



        private async void MostrarTarjetasSitios_Mto(string id_circuito)
        {
            if (!StateConexion)
            {
                _idsCircuito = "";
                string urli = logicaWS.Movile_Consulta_Recorrido_Circuito_Metodo(id_circuito, IdiomaBase);
                string jsonProcedimiento = await logicaWS.Conection(urli);
                JArray jArrayTargerasS = JArray.Parse(jsonProcedimiento);

                //MapaSitios.Pins.Clear();
                var latitudes = new List<double>();
                var longitudes = new List<double>();
                try
                {
                    Polyline polyline1 = new Polyline() { StrokeColor = Colors.Red, StrokeWidth = 2 };
                    foreach (JObject item in jArrayTargerasS)
                    {
                        Sitio sitio = new Sitio(
                            item.GetValue("id_sitio").ToString(),
                            item.GetValue("nombre_sitio").ToString().ToUpper(),
                            item.GetValue("descripcion_corta_sitio").ToString(),
                            item.GetValue("latitud").ToString(),
                            item.GetValue("longitud").ToString(),
                            item.GetValue("imagen").ToString(),
                            int.Parse(item.GetValue("promedio").ToString()),
                            int.Parse(item.GetValue("dislike").ToString()),
                            item.GetValue("orden").ToString());

                        ListaSitios.Add(sitio);
                        //Position position = new Position((double)item.GetValue("latitud").Value<decimal>(), (double)item.GetValue("longitud").Value<decimal>());
                        //latitudes.Add(position.Latitude);
                        //longitudes.Add(position.Longitude);

                        //Pin pin = new Pin
                        //{
                        //    Tag = sitio.Id_Sitio,
                        //    Type = PinType.Place,
                        //    Label = sitio.Nombre_Sitio,
                        //    //Label = $"{sitio.Orden}",
                        //    Address = sitio.Descripcion_Corta_Sitio,
                        //    Position = new Position(double.Parse(sitio.Latitud), double.Parse(sitio.Longitud)),
                        //    Icon = BitmapDescriptorFactory.FromBundle(pinf.PinesEnumeradosRespuesta(sitio.Orden, "1"))
                        //};


                        //MapaSitios.Pins.Add(pin);
                        _idsCircuito = id_circuito;
                        //polyline1.Positions.Add(position);
                    }
                    //MapaSitios.Polylines.Add(polyline1);

                    double lowestLat = latitudes.Min();
                    double highestLat = latitudes.Max();
                    double lowestLong = longitudes.Min();
                    double highestLong = longitudes.Max();
                    double finalLat = (lowestLat + highestLat) / 2;
                    double finalLong = (lowestLong + highestLong) / 2;
                    double distance = Location.CalculateDistance(new Location(lowestLat, lowestLong), new Location(highestLat, highestLong), DistanceUnits.Kilometers);
                    //MapaSitios.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(finalLat, finalLong), Distance.FromKilometers(distance)));

                    //if (MapaSitios.Pins.Count == 0)
                    //{
                    //    //TODO devolver a la pantalla de circuitos y no a la de filtros
                    //    await pageServicio.DisplayAlert("Opps! Algo paso", "No ahi sitios para este circuito", "Ok");
                    //    //Application.Current.MainPage = new CustomNavigationPage(new MainPage());
                    //}
                }
                catch (Exception exs)
                {
                    //TODO devolver a la pantalla de circuitos y no a la de filtros
                    await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Ok");
                    //Application.Current.MainPage = new CustomNavigationPage(new MainPage());
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
            IsBusy = false;
        }

        public void RemoverPin(string idCircuito)
        {

            //for (var i = 0; i < ListaSitios.Count(); i++)
            //{
            //    if (idCircuito == ListaSitios[i].Id_Sitio)
            //    {
            //        // Pines color rojo
            //        MapaSitios.Pins[i].Icon = BitmapDescriptorFactory.FromBundle(pinf.PinesEnumeradosRespuesta(ListaSitios[i].Orden, "2"));
            //    }
            //    else
            //    {
            //        // Pines color azul
            //        MapaSitios.Pins[i].Icon = BitmapDescriptorFactory.FromBundle(pinf.PinesEnumeradosRespuesta(ListaSitios[i].Orden, "1"));
            //    }
            //}
        }
        private async void BtnPlayRecorrido_Mtd()
        {
            if (!EstadoUsuario.Equals("2"))
            {
                var res = await pageServicio.DisplayAlert("AppToTrip", "Quieres iniciar el recorrido", "Si", "No");
                if (res)
                {
                    if (circuito.compra.ToString().Equals("1"))
                    {
                        Iniciar_Circuito_Mtd(circuito.id_circuito.ToString());
                    }
                    else
                    {
                        if (circuito.precio.ToString().Equals("Free"))
                        {
                            try
                            {
                                string urli = logicaWS.Movile_Comprar_Circuito_Metodo(circuito.id_circuito);
                                string Json_Procedimiento_Vgl = await logicaWS.Conection(urli);
                                JArray jsonObject = JArray.Parse(Json_Procedimiento_Vgl);
                                foreach (JObject item in jsonObject)
                                {
                                    string resultado = item.GetValue("resultado").ToString();
                                    if (resultado.Equals("compra realizada"))
                                    {
                                        if (!EstadoUsuario.Equals("2"))
                                        {
                                            Iniciar_Circuito_Mtd(circuito.id_circuito);
                                        }
                                    }
                                }
                            }
                            catch (Exception exs)
                            {
                                await pageServicio.DisplayAlert("Opps! Algo paso con la conexion a la App", exs.Message, "Ok");
                            }
                        }
                    }
                }
            }
            else
            {
                var res = await pageServicio.DisplayAlert("AppToTrip", "Ya estas realizando un recorrido\n          ¿Quieres finalizarlo?", "Si", "No");
                if (res)
                {
                    // recorridoMapaView.FinalizarCircuito();
                    try
                    {
                        string idCircuitoFin = Preferences.Get("id_circuito", "");
                        string urli = logicaWS.Movile_Finalizar_Circuito_Metodo(idCircuitoFin);
                        string Json_Procedimiento_Vgl = await logicaWS.Conection(urli);
                        JObject jsonObject = JObject.Parse(Json_Procedimiento_Vgl);
                        JArray jsonObject1 = JArray.Parse(jsonObject.GetValue("Table").ToString());
                        foreach (JObject item in jsonObject1)
                        {
                            string resultado = item.GetValue("resultado").ToString();
                            if (resultado.Equals("Circuito Finalizado"))
                            {
                                string audioUrl = logicaWS.url_audio + "finalizocircuito_" + IdiomaBase + ".mp3";
                                Preferences.Set("estado_persona", "1");
                                Preferences.Set("id_circuito", "");
                                Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                                //await Application.Current.MainPage.Navigation.PushPopupAsync(new PopUpCalificacion("circuito", "", ""));
                                //await CrossMediaManager.Current.Play(audioUrl);
                                WebClient wc = new WebClient();
                                Stream fileStream = wc.OpenRead(audioUrl);
                                player.Load(fileStream);
                                player.Volume = 1.0;
                                player.Play();
                            }

                        }
                    }
                    catch (Exception exs)
                    {
                        await pageServicio.DisplayAlert("Opps! Algo paso con la conexion a la App", exs.Message, "Ok");
                    }
                }
            }
        }

        private async void Iniciar_Circuito_Mtd(string id)
        {
            try
            {

                string urli = logicaWS.Movile_Asignar_Circuito_Metodo(id);
                string Json_Procedimiento_Vgl = await logicaWS.Conection(urli);
                JObject jsonObject = JObject.Parse(Json_Procedimiento_Vgl);

                string resultado = jsonObject.GetValue("Table").ToString();
                JArray jsonObject1 = JArray.Parse(resultado);

                foreach (JObject item in jsonObject1)
                {
                    resultado = item.GetValue("resultado").ToString();
                }
                if (resultado.Equals("Circuito Asignado"))
                {
                    if (!EstadoUsuario.Equals("2"))
                    {
                        Preferences.Set("estado_persona", "2");
                        Preferences.Set("id_circuito", id);
                        RootPage.Detail = new NavigationPage(new RecorridoMapa());

                    }
                }
            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert("Opps! Algo paso con la conexion a la App", exs.Message, "Ok");
            }
        }


        public async void BtnDescripicionAudio_Mtd()
        {
            /*
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("AppToTrip.Recursos.fondo.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(audioStream);
            audio.Volume = 0.1;
            */
            try
            {
                IsBusy = true;
                //audio.Play();
                string audioUrl = logicaWS.Movile_Audio_Metodo("sitio", ListaSitios[CarouselPosition].Id_Sitio.ToString(), "descripcion_corta_sitio", IdiomaBase);
                //Player("AppToTrip.Recursos.melodia.mp3");

                //_ = CrossMediaManager.Current.Play(audioUrl);
                WebClient wc = new WebClient();
                Stream fileStream = wc.OpenRead(audioUrl);
                player.Load(fileStream);
                player.Volume = 1.0;
                player.Play();

                var ans = await pageServicio.DisplayAlert(ListaSitios[CarouselPosition].Nombre_Sitio.ToString(), ListaSitios[CarouselPosition].Descripcion_Corta_Sitio.ToString(), "Detener", "Continuar");
                if (ans)
                    //audio.Stop();

                    //await CrossMediaManager.Current.Stop();
                    CrossSimpleAudioPlayer.Current.Stop();

                IsBusy = false;
            }

            catch (Exception ex)
            {
                Console.WriteLine("EL ERROR:" + ex);
            }
            //audio.Stop();
        }

        private async Task BtnBuscarHotel_Mtd(string origen)
        {
            IsBusy = true;
            if (!StateConexion)
            {
                try
                {
                    string urli = logicaWS.Movile_Consulta_Hotel_Metodo(_Ciudad, IdiomaBase, _Pais);
                    string jsonProcedimiento = await logicaWS.Conection(urli);
                    JArray jArrayTargerasC = JArray.Parse(jsonProcedimiento);

                    //foreach (JObject item in jArrayTargerasC)
                    //{
                    //    Pin pin = new Pin
                    //    {
                    //        Tag = item.GetValue("id_hotel").ToString(),
                    //        Type = PinType.Generic,
                    //        Label = item.GetValue("nombre_hotel").ToString(),
                    //        Address = item.GetValue("descripcion_corta_hotel").ToString(),
                    //        Position = new Position(double.Parse(item.GetValue("latitud").ToString()), double.Parse(item.GetValue("longitud").ToString()))
                    //    };

                    //    pin.Icon = BitmapDescriptorFactory.FromBundle("ic_hotel_pin.png");

                    //    MapaSitios.Pins.Add(pin);
                    //    MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(double.Parse(item.GetValue("latitud").ToString()), double.Parse(item.GetValue("longitud").ToString())), Distance.FromKilometers(0.5));
                    //    MapaSitios.MoveToRegion(mapSpan);
                    //}

                    if (origen.Equals("sinInfoTarjetas"))
                        await pageServicio.DisplayAlert("AppToTrip", "No se encontraron circuitos para esta ubicacion, te presentramos estos hoteles", "Ok");
                    else if (origen.Equals("BtnHotel") && jArrayTargerasC.Count == 0)
                        await pageServicio.DisplayAlert("AppToTrip", "No tenemos registreados hoteles en el momento para esta ubicacion", "Ok");

                }
                catch (Exception)
                {
                    if (origen.Equals("sinInfoTarjetas"))
                        await pageServicio.DisplayAlert("AppToTrip", "No se encontro informacion para esta ubicacion, intenta de nuevo mas tarde", "Ok");
                    else if (origen.Equals("BtnHotel"))
                        await pageServicio.DisplayAlert("AppToTrip", "No tenemos registrados hoteles en el momento para esta ubicacion", "Ok");
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
            IsBusy = false;
        }
        private async Task BtnMenuLateral_Mtd()
        {
            //await Application.Current.MainPage.Navigation.PushPopupAsync(new MenuPopUp("1", null));
        }
        private async Task BtnMiUbicacion_Mtd()
        {
            await geoLocation.GetLocationGPS();
            //MapSpan mapSpan = MapSpan.FromCenterAndRadius(new Position(geoLocation.Lat, geoLocation.Log), Distance.FromKilometers(0.5));
            //MapaSitios.MoveToRegion(mapSpan);
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
                        //MapaSitios.IsIndoorEnabled = true;
                        //MapaSitios.MyLocationEnabled = true;
                        //MapaSitios.UiSettings.MyLocationButtonEnabled = false;
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

        public async void CarruselImgMasInfo_ClickedMtd(object obj)
        {
            try
            {
                await pageServicio.PushAsync(new InfoSitios(obj.ToString(), "1", null));
            }
            catch (Exception exs)
            {
                //TODO devolver a la pantalla de circuitos y no a la de filtros
                await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Ok");
                //Application.Current.MainPage = new CustomNavigationPage(new MainPage());
            }
        }
        void BtnVolver_Mtd()
        {
            RootPage.Detail = new NavigationPage(new TargetasCircuitos(_Categoria, _Pais, _Ciudad, Bandera_, _IdCircuito));
        }

        void BtnBuscar_Mtd()
        {
            RootPage.Detail = new NavigationPage(new Filtros());
        }

    }
}
