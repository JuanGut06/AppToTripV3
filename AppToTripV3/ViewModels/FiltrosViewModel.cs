using AppToTripV3.Models;
using AppToTripV3.Services;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppToTripV3.Views;

namespace AppToTripV3.ViewModels
{
    public class FiltrosViewModel : BaseViewModel
    {

        private readonly LogicaWS logicaWS;
        public readonly IPageServicio pageServicio;

        private ObservableCollection<ListasBase> _listaMacros;
        private ObservableCollection<ListasBase> _listaCategoria;
        private ObservableCollection<ListasBase> _listaNumDias;


        ListasBase _selectMacros;
        ListasBase _selectCategoria;
        ListasBase _selectNumDias;

        public string _imagenCircuitoSitios;
        public string pais;
        public string ciudad;
        private string paisCerca;
        private string ciudadCerca;
        private string _ubicadoLabel = "";
        private string geocodeAddress;
        private string _entNumDias;

        private bool _macrosLayout;
        private bool _consultasLayout;
        private bool _createCircuit;

        private bool _imagenCircSitiosVisible;


        private bool _btnAtrasVisible;
        private bool _btnCerrarVisible = true;
        private bool _btnCrearCircVisible;
        private bool _btnCercaVisible;
        private bool _numSitiosVisible = false;
        private bool _processCircuit;

        private int contadorImagenes = 1;
        private string _imagenConContador;
        private string _totalSitiosContador;

        // Labels mensajes de estado - creacion circuitos
        private string _circuitoLabel;
        private bool _sugerenciaLabel;

        // CheckBoxs de estado - creacion circuitos
        private bool _circuitVisible;


        // Reload de estado - creacion circuitos
        private bool _circuitLoadVisible;

        private int _indexDia;

        public ICommand EquisBtn { get; }
        public ICommand BuscarBtn { get; }
        public ICommand CrearCircuito { get; }
        public ICommand MacrosBtn { get; }
        public ICommand AddCircuitBtn { get; }

        public ICommand VolverBtn { get; }
        public ICommand CercamiLayautBtn { get; }


        public string ImagenCircuitoSitios
        {
            get { return _imagenCircuitoSitios; }
            set { _imagenCircuitoSitios = value; OnPropertyChanged(nameof(ImagenCircuitoSitios)); }
        }

        public string EntNumDias
        {
            get { return _entNumDias; }
            set { _entNumDias = value; OnPropertyChanged(nameof(EntNumDias)); }
        }

        public string UbicadoLabel
        {
            get { return _ubicadoLabel; }
            set { _ubicadoLabel = value; OnPropertyChanged(nameof(UbicadoLabel)); }
        }
        public string CircuitoLabel
        {
            get { return _circuitoLabel; }
            set { _circuitoLabel = value; OnPropertyChanged(nameof(CircuitoLabel)); }
        }

        public string TotalSitiosContador
        {
            get { return _totalSitiosContador; }
            set { _totalSitiosContador = value; OnPropertyChanged(nameof(TotalSitiosContador)); }
        }

        public string ImagenConContador
        {
            get { return _imagenConContador; }
            set { _imagenConContador = value; OnPropertyChanged(nameof(ImagenConContador)); }
        }

        public bool SugerenciaLabel
        {
            get { return _sugerenciaLabel; }
            set { _sugerenciaLabel = value; OnPropertyChanged(nameof(SugerenciaLabel)); }
        }

        public bool ImagenCircSitiosVisible
        {
            get { return _imagenCircSitiosVisible; }
            set { _imagenCircSitiosVisible = value; OnPropertyChanged(nameof(ImagenCircSitiosVisible)); }
        }

        public bool MacrosLayout
        {
            get { return _macrosLayout; }
            set { _macrosLayout = value; OnPropertyChanged(nameof(MacrosLayout)); }
        }

        public bool NumSitiosVisible
        {
            get { return _numSitiosVisible; }
            set { _numSitiosVisible = value; OnPropertyChanged(nameof(NumSitiosVisible)); }
        }
        public bool BtnCercaVisible
        {
            get { return _btnCercaVisible; }
            set { _btnCercaVisible = value; OnPropertyChanged(nameof(BtnCercaVisible)); }
        }
        public bool BtnCrearCircVisible
        {
            get { return _btnCrearCircVisible; }
            set { _btnCrearCircVisible = value; OnPropertyChanged(nameof(BtnCrearCircVisible)); }
        }

        public bool BtnCerrarVisible
        {
            get { return _btnCerrarVisible; }
            set { _btnCerrarVisible = value; OnPropertyChanged(nameof(BtnCerrarVisible)); }
        }

        public bool BtnAtrasVisible
        {
            get { return _btnAtrasVisible; }
            set { _btnAtrasVisible = value; OnPropertyChanged(nameof(BtnAtrasVisible)); }
        }

        public bool CreateCircuit
        {
            get { return _createCircuit; }
            set { _createCircuit = value; OnPropertyChanged(nameof(CreateCircuit)); }
        }

        public bool ProcessCircuit
        {
            get { return _processCircuit; }
            set { _processCircuit = value; OnPropertyChanged(nameof(ProcessCircuit)); }
        }
        public bool ConsultasLayout
        {
            get { return _consultasLayout; }
            set { _consultasLayout = value; OnPropertyChanged(nameof(ConsultasLayout)); }
        }

        public bool CircuitVisible
        {
            get { return _circuitVisible; }
            set { _circuitVisible = value; OnPropertyChanged(nameof(CircuitVisible)); }
        }

        public bool CircuitLoadVisible
        {
            get { return _circuitLoadVisible; }
            set { _circuitLoadVisible = value; OnPropertyChanged(nameof(CircuitLoadVisible)); }
        }

        public int IndexDia
        {
            get { return _indexDia; }
            set { _indexDia = value; OnPropertyChanged(nameof(IndexDia)); }
        }

        public ObservableCollection<ListasBase> ListaMacros
        {
            get { return _listaMacros; }
            set { _listaMacros = value; OnPropertyChanged(nameof(ListaMacros)); }
        }

        public ListasBase SelectCategoria
        {
            get { return _selectCategoria; }
            set { _selectCategoria = value; OnPropertyChanged(nameof(SelectCategoria)); }
        }

        public ListasBase SelectMacros
        {
            get { return _selectMacros; }
            set { _selectMacros = value; OnPropertyChanged(nameof(SelectMacros)); BuscarMacro(_selectMacros.Nombre); }
        }

        public ObservableCollection<ListasBase> ListaCategoria
        {
            get { return _listaCategoria; }
            set { _listaCategoria = value; OnPropertyChanged(nameof(ListaCategoria)); }
        }

        public ObservableCollection<ListasBase> ListaNumDias
        {
            get { return _listaNumDias; }
            set { _listaNumDias = value; OnPropertyChanged(nameof(ListaNumDias)); }
        }
        public ListasBase SelectNumDias
        {
            get { return _selectNumDias; }
            set { _selectNumDias = value; OnPropertyChanged(nameof(SelectNumDias)); }
        }

        public FiltrosViewModel()
        {
            _ = GetDirectionAsync();
            IsBusy = true;
            logicaWS = new LogicaWS();
            pageServicio = new PageServicio();

            EquisBtn = new Command(() => Equis_Mtd());
            VolverBtn = new Command(() => VolverBtn_Mtd());
            MacrosBtn = new Command(() => MacrosBtn_Mtd());
            AddCircuitBtn = new Command(() => CreateCircuitBtn_Mtd());
            BuscarBtn = new Command(async () => await BuscarBtn_MtdAsync());
            CrearCircuito = new Command(async () => await crearCircuitoService_Mtd());
            CercamiLayautBtn = new Command(async () => await BuscarCerca_MtdAsync());

            Incializar_Mtd();
            CargarNumeroDias();


        }

        /* =========================  MODULO CREACION CIRCUITOS ==========================*/

        // 1.  CREA EL CIRCUITO A NIVEL GENERAL
        public async Task crearCircuitoService_Mtd()
        {
            if (!StateConexion)
            {
                try
                {
                    if (SelectNumDias != null)
                    {
                        var respu = await pageServicio.DisplayAlert("AppToTrip", "Tu circuito estará listo en tan solo unos pocos minutos. Te notificaremos cuando este creado.\n¿Estás listo/a para continuar?", "Si", "No");
                        if (respu)
                        {
                            BtnCerrarVisible = false;
                            BtnAtrasVisible = false;
                            CreateCircuit = false;
                            ProcessCircuit = true;
                            ImagenCircuitoSitios = "apptotrip.png";
                            CircuitoLabel = "Creando Circuito...";
                            SugerenciaLabel = true;
                            CircuitLoadVisible = true;

                            // 1. CREA EL CIRCUITO
                            string urli = logicaWS.Movile_Crear_Circuitos_Metodo(ciudadCerca, paisCerca, SelectNumDias.Id);
                            string jsonProcedimiento = await logicaWS.Conection(urli);
                            JObject jsonObject = JObject.Parse(jsonProcedimiento);
                            string id_IdCircuitoWS = jsonObject["IdCircuito"].Value<string>();
                            string Nom_CircuitoWS = jsonObject["NombreSitio"].Value<string>();
                            string NumDiaWS = jsonObject["numDia"].Value<string>();

                            ImagenCircuitoSitios = jsonObject["Img"].Value<string>();
                            ImagenCircSitiosVisible = true;
                            CircuitoLabel = "Generando información de " + jsonObject["Mensaje"].Value<string>();

                            // 2. TRADUCE EL CIRCUITO 
                            string urli2 = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_Destino_Metodo(id_IdCircuitoWS, IdiomaBase);
                            string jsonProcedimiento2 = await logicaWS.Conection(urli2);
                            JObject jsonObject2 = JObject.Parse(jsonProcedimiento2);
                            string idCircuito2 = jsonObject2["IdCircuito"].Value<string>();

                            // 3. CREA EL NOMBRE DE LOS SITIOS
                            string urli4 = logicaWS.Movile_Generar_Nombre_Sitios_Metodo(Nom_CircuitoWS, idCircuito2, NumDiaWS);
                            string jsonProcedimiento4 = await logicaWS.Conection(urli4);
                            JObject jsonObject4 = JObject.Parse(jsonProcedimiento4);
                            string sitioWS4 = jsonObject4["PrimerNombre"].Value<string>();
                            string idSitio4 = jsonObject4["PrimerIdSitio"].Value<string>();
                            string idcircuito4 = jsonObject4["idCircuito"].Value<string>();
                            TotalSitiosContador = jsonObject4["cantidad"].Value<string>();

                            CircuitoLabel = "Creando sitios";

                            // 4. GENERA INFORMACION DE LOS SITIOS                           
                            GenerarInfoSitios_Mtd(sitioWS4, Nom_CircuitoWS, idSitio4, idcircuito4);
                        }
                    }
                    else
                    {
                        await pageServicio.DisplayAlert("AppToTrip", "Ingrese el número de días.", "Aceptar");
                    }
                }
                catch (Exception exs)
                {

                    await pageServicio.DisplayAlert("AppToTrip", exs.Message, "Aceptar");
                }

            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
        }

        public async void GenerarInfoSitios_Mtd(string sitio, string nomCircuito, string idSitio, string idCircuito)
        {
            if (!StateConexion)
            {
                try
                {
                    string validaSitio = "";
                    string urli5 = "";
                    if (validaSitio != idSitio)
                    {
                        urli5 = logicaWS.Movile_Generar_Info_Sitios_Metodo(sitio, nomCircuito, idSitio, idCircuito);
                    }
                    validaSitio = idSitio;
                    string jsonProcedimiento5 = await logicaWS.Conection(urli5);
                    JObject jsonObject5 = JObject.Parse(jsonProcedimiento5);
                    string idsitio5 = jsonObject5["IdSitio"].Value<string>();
                    string idcircuito5 = jsonObject5["IdCircuito"].Value<string>();
                    ImagenCircuitoSitios = jsonObject5["Img"].Value<string>();
                    ImagenConContador = contadorImagenes.ToString();
                    contadorImagenes++;

                    CircuitoLabel = "Generando información de " + jsonObject5["Mensaje"].Value<string>();



                    NumSitiosVisible = true;
                    TraducirInfoSitios_Mtd(idsitio5, idcircuito5, IdiomaBase);
                }
                catch (Exception ex)
                {
                    await pageServicio.DisplayAlert("AppToTrip", "Error generar info sitios" + ex.Message, "Aceptar");
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
        }
        public async void TraducirInfoSitios_Mtd(string idSitio, string idCircuito, string idioma)
        {
            if (!StateConexion)
            {
                try
                {
                    string urli6 = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_Destino_Metodo(idSitio, idCircuito, idioma);
                    string jsonProcedimiento6 = await logicaWS.Conection(urli6);
                    JObject jsonObject6 = JObject.Parse(jsonProcedimiento6);
                    string nextIdSitio = jsonObject6["NextIdSitio"].Value<string>();
                    string nextNombreSitio = jsonObject6["NextNombreSitio"].Value<string>();
                    string NomCircuito7 = jsonObject6["NombreCircuito"].Value<string>();
                    string idCircuito7 = jsonObject6["idCircuito"].Value<string>();
                    //CircuitoLabel = jsonObject6["Message"].Value<string>();
                    if (nextIdSitio != "")
                    {
                        GenerarInfoSitios_Mtd(nextNombreSitio, NomCircuito7, nextIdSitio, idCircuito7);
                    }
                    else
                    {
                        CircuitVisible = true;
                        CircuitLoadVisible = false;
                        CircuitoLabel = "Circuito creado exitosamente";
                        await Task.Delay(2000);
                        SugerenciaLabel = false;
                        //CrossLocalNotifications.Current.Show("AppToTrip", CircuitoLabel);
                        RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, paisCerca, ciudadCerca, "1", idCircuito7));
                    }

                }
                catch (Exception ex)
                {
                    await pageServicio.DisplayAlert("AppToTrip", "Error traducir sitio: " + ex.Message, "Aceptar");
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
        }


        /* ========================= *** FIN MODULO CREACION CIRCUITOS *** ==========================*/


        private void Incializar_Mtd()
        {
            ConsultasLayout = true;
            MultilistasPrincipal_MtdAsync();
        }

        private void BuscarMacro(string NombreMacro)
        {
            IsBusy = true;
            RootPage.Detail = new NavigationPage(new TargetasMacros(NombreMacro));
        }



        private async void MultilistasPrincipal_MtdAsync()
        {
            try
            {
                string urli = logicaWS.Movile_Consulta_Categoria_Metodo(IdiomaBase);
                string jsonProcedimiento = await logicaWS.Conection(urli);
                JArray jsonArray = JArray.Parse(jsonProcedimiento);
                ListaCategoria = new ObservableCollection<ListasBase>();
                foreach (JObject item in jsonArray)
                    ListaCategoria.Add(new ListasBase { Nombre = item.GetValue("nombre_categoria").ToString() });
            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
            }

            try
            {
                string urli = logicaWS.Movile_Consulta_Macros_Metodo();
                string jsonProcedimiento = await logicaWS.Conection(urli);
                JArray jsonArray = JArray.Parse(jsonProcedimiento);
                ListaMacros = new ObservableCollection<ListasBase>();
                foreach (JObject item in jsonArray)
                    ListaMacros.Add(new ListasBase { Nombre = item.GetValue("nombre_macro").ToString() });
            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
            }
            IsBusy = false;
        }


        private async Task BuscarBtn_MtdAsync()
        {

            if (!string.IsNullOrWhiteSpace(pais) && !string.IsNullOrWhiteSpace(ciudad))
            {
                if (SelectCategoria != null)
                    RootPage.Detail = new NavigationPage(new TargetasCircuitos(SelectCategoria.Nombre, pais, ciudad, "1", null));
                else
                    RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, pais, ciudad, "1", null));
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Seleccione un destino", "Aceptar");
            }
        }

        private async Task BuscarCerca_MtdAsync()
        {

            if (!string.IsNullOrWhiteSpace(paisCerca) && !string.IsNullOrWhiteSpace(ciudadCerca))
            {
                RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, paisCerca, ciudadCerca, "1", null));
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Seleccione un destino", "Aceptar");
            }
        }

        private void Equis_Mtd()
        {
            string estadoCircuito = Preferences.Get("estado_persona", "");

            if (estadoCircuito == "2")
            {
                _ = RootPage.Navigation.PopToRootAsync(false);
                RootPage.Detail = new NavigationPage(new RecorridoMapa());
            }
            else
            {
                try
                {
                    if (!UbicadoLabel.Equals("No se encontró su ubicación."))
                    {
                        if (!string.IsNullOrWhiteSpace(paisCerca) && !string.IsNullOrWhiteSpace(ciudadCerca))
                        {
                            RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, paisCerca, ciudadCerca, "1", null));
                        }
                        else
                        {
                            RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, "Colombia", "Bogotá", "1", null));
                            _ = GetDirectionAsync();
                        }
                    }
                    else
                    {
                        RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, "Colombia", "Bogotá", "1", null));
                        _ = GetDirectionAsync();
                    }
                }
                catch
                {
                    RootPage.Detail = new NavigationPage(new TargetasCircuitos(null, "Colombia", "Bogotá", "1", null));
                    _ = GetDirectionAsync();
                }
            }
        }
        public async Task GetDirectionAsync()
        {
            try
            {
                var cachedLocation = await Geolocation.GetLastKnownLocationAsync();
                if (cachedLocation != null)
                {
                    ProcessLocation(cachedLocation);
                    return;
                }

                var request = new GeolocationRequest(GeolocationAccuracy.Medium) { Timeout = TimeSpan.FromSeconds(10) };
                var location = await Geolocation.GetLocationAsync(request);
                ProcessLocation(location);
            }
            catch (FeatureNotSupportedException)
            {
                UbicadoLabel = "No se encontró su ubicación";
            }
            catch (PermissionException)
            {
                UbicadoLabel = "No se encontró su ubicación";
            }
            catch (Exception ex)
            {
                UbicadoLabel = "No se encontró su ubicación";
            }
        }

        private async void ProcessLocation(Location location)
        {
            if (location != null)
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    paisCerca = placemark.CountryName;
                    ciudadCerca = placemark.Locality;
                    UbicadoLabel = placemark.Locality + ", " + placemark.CountryName;
                    Preferences.Set("UbicadoLabel", UbicadoLabel);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!string.IsNullOrEmpty(UbicadoLabel))
                        {
                            BtnCercaVisible = true;
                        }
                        else
                        {
                            BtnCercaVisible = false;
                        }
                    });
                    string urli = logicaWS.Movile_Consulta_Circuitos_Metodo(ciudadCerca, IdiomaBase, paisCerca);
                    string jsonProcedimiento = await logicaWS.Conection(urli);

                    if (jsonProcedimiento == "[{\"resultado\":\"No se encontro en base de datos\"}]")
                    {
                        JArray jArrayTargerasC = JArray.Parse(jsonProcedimiento);
                        foreach (JObject item in jArrayTargerasC)
                        {
                            string resultado = item.GetValue("resultado").ToString();
                            if (resultado.Equals("No se encontro en base de datos"))
                            {
                                BtnCercaVisible = false;
                            }
                            else
                            {
                                BtnCercaVisible = true;
                            }
                        }
                    }
                    else
                    {
                        BtnCercaVisible = true;
                    }
                }
                else
                {
                    UbicadoLabel = "No se encontró su ubicación.";
                }
            }
            else
            {
                UbicadoLabel = "No se encontró su ubicación.";
            }
        }

        private void MacrosBtn_Mtd()
        {
            MacrosLayout = true;
            ConsultasLayout = false;
            ProcessCircuit = false;

        }

        private async void CreateCircuitBtn_Mtd()
        {
            if (!string.IsNullOrWhiteSpace(ciudadCerca) && !string.IsNullOrWhiteSpace(paisCerca))
            {
                BtnAtrasVisible = true;
                CreateCircuit = true;
                ConsultasLayout = false;
                ProcessCircuit = true;
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Ubicación no encontrada", "Aceptar");
            }
        }

        private void VolverBtn_Mtd()
        {
            MacrosLayout = false;
            CreateCircuit = false;
            BtnAtrasVisible = false;
            ConsultasLayout = true;
            ProcessCircuit = false;
        }
        public void CargarNumeroDias()
        {
            ListaNumDias = new ObservableCollection<ListasBase>
            {
                new ListasBase() { Id = "1", Nombre = "1 día" },
                new ListasBase() { Id = "2", Nombre = "2 días" },
                new ListasBase() { Id = "3", Nombre = "3 días" },
                new ListasBase() { Id = "4", Nombre = "4 días" },
                new ListasBase() { Id = "5", Nombre = "5 días" },
                new ListasBase() { Id = "6", Nombre = "6 días" },
                new ListasBase() { Id = "7", Nombre = "7 días" }
            };

            IndexDia = 0;
        }
    }
}