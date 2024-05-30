using AppToTripV3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.SimpleAudioPlayer;
using System.Net;
using Newtonsoft.Json.Linq;
using AppToTripV3.Views;
using System.Globalization;

namespace AppToTripV3.ViewModels
{
    public class InfoCircuitosViewModel : BaseViewModel
    {
        private readonly string IdCircuito;
        private readonly LogicaWS logicaWS;
        private readonly IPageServicio pageServicio;

        private ObservableCollection<Images> _imgCarrusel;
        private ObservableCollection<Images> _calificacionAutor;
        private ObservableCollection<Images> _calificacionCircuito;

        private long _valueSlider;
        private long _valueSliderMax;
        ISimpleAudioPlayer player;
        private int _positionCarusel;

        private readonly string _Pais;
        private readonly string _Ciudad;
        private readonly string _Categoria;
        private readonly string Bandera_;

        private string _edad;
        private string _duracion;
        private string _ubicacion;
        private string _edadLabel;
        private string _autorLabel;
        private string _nombreAutor;
        private string _autorImagen;
        private string IdiomaBaseAux;
        private string _equipamiento;
        private string _labelPosicion;
        private string _duracionLabel;
        private string _nombreCircuito;
        private string _ubicacionLabel;
        private string _recomendaciones;
        private string _descripcionCorta;
        private string _descripcionLarga;
        private string _descripcionLabel;
        private string _equipamientoLabel;
        private string _sourcePausarAudio;
        private string _sourceBanderaIdioma;
        private string _iniciarCircuitoLabel;
        private string _comprarCircuitoLabel;
        private string _recomendacionesLabel;

        private bool _verEdad;
        private bool _verAutor;
        private bool _verDuracion;
        private bool _verUbicacion;
        private bool _verDescripcion;
        private bool _verEquipamiento;
        private bool _verIconoBandera;
        private bool _verInciarCircuito;
        private bool _verComprarCircuito;
        private bool _VerRecomendaciones;
        private bool _verReproductorAudio;
        private bool _verIconoReproductor;

        private bool primeraEntrada;



        public ICommand BtnAudioPausar { get; }
        public ICommand BtnAudioCerrar { get; }
        public ICommand BtnAudioIniciar { get; }
        public ICommand BtnSelectIdioma { get; }
        public ICommand BtnIniciarCircuito { get; }
        public ICommand BtnComprarCircuito { get; }
        public ICommand BtnVolver { get; }

        public ObservableCollection<Images> ImgCarrusel
        {
            get { return _imgCarrusel; }
            set { _imgCarrusel = value; OnPropertyChanged(nameof(ImgCarrusel)); }
        }

        public ObservableCollection<Images> CalificacionAutor
        {
            get { return _calificacionAutor; }
            set { _calificacionAutor = value; OnPropertyChanged(nameof(CalificacionAutor)); }
        }

        public ObservableCollection<Images> CalificacionCircuito
        {
            get { return _calificacionCircuito; }
            set { _calificacionCircuito = value; OnPropertyChanged(nameof(CalificacionCircuito)); }
        }

        public string Edad
        {
            get { return _edad; }
            set { _edad = value; OnPropertyChanged(nameof(Edad)); }
        }

        public string Duracion
        {
            get { return _duracion; }
            set { _duracion = value; OnPropertyChanged(nameof(Duracion)); }
        }

        public string Ubicacion
        {
            get { return _ubicacion; }
            set { _ubicacion = value; OnPropertyChanged(nameof(Ubicacion)); }
        }

        public string NombreAutor
        {
            get { return _nombreAutor; }
            set { _nombreAutor = value; OnPropertyChanged(nameof(NombreAutor)); }
        }

        public string AutorImagen
        {
            get { return _autorImagen; }
            set { _autorImagen = value; OnPropertyChanged(nameof(AutorImagen)); }
        }

        public string Equipamiento
        {
            get { return _equipamiento; }
            set { _equipamiento = value; OnPropertyChanged(nameof(Equipamiento)); }
        }

        public string NombreCircuito
        {
            get { return _nombreCircuito; }
            set { _nombreCircuito = value; OnPropertyChanged(nameof(NombreCircuito)); }
        }

        public string Recomendaciones
        {
            get { return _recomendaciones; }
            set { _recomendaciones = value; OnPropertyChanged(nameof(Recomendaciones)); }
        }

        public string DescripcionCorta
        {
            get { return _descripcionCorta; }
            set { _descripcionCorta = value; OnPropertyChanged(nameof(DescripcionCorta)); }
        }

        public string DescripcionLarga
        {
            get { return _descripcionLarga; }
            set { _descripcionLarga = value; OnPropertyChanged(nameof(DescripcionLarga)); }
        }

        public string IniciarCircuitoLabel
        {
            get { return _iniciarCircuitoLabel; }
            set { _iniciarCircuitoLabel = value; OnPropertyChanged(nameof(IniciarCircuitoLabel)); }
        }

        public string ComprarCircuitoLabel
        {
            get { return _comprarCircuitoLabel; }
            set { _comprarCircuitoLabel = value; OnPropertyChanged(nameof(ComprarCircuitoLabel)); }
        }

        public string SourceBanderaIdioma
        {
            get { return _sourceBanderaIdioma; }
            set { _sourceBanderaIdioma = value; OnPropertyChanged(nameof(SourceBanderaIdioma)); }
        }

        public string UbicacionLabel
        {
            get { return _ubicacionLabel; }
            set { _ubicacionLabel = value; OnPropertyChanged(nameof(UbicacionLabel)); }
        }

        public string EdadLabel
        {
            get { return _edadLabel; }
            set { _edadLabel = value; OnPropertyChanged(nameof(EdadLabel)); }
        }

        public string DuracionLabel
        {
            get { return _duracionLabel; }
            set { _duracionLabel = value; OnPropertyChanged(nameof(DuracionLabel)); }
        }

        public string RecomendacionesLabel
        {
            get { return _recomendacionesLabel; }
            set { _recomendacionesLabel = value; OnPropertyChanged(nameof(RecomendacionesLabel)); }
        }

        public string EquipamientoLabel
        {
            get { return _equipamientoLabel; }
            set { _equipamientoLabel = value; OnPropertyChanged(nameof(EquipamientoLabel)); }
        }

        public string DescripcionLabel
        {
            get { return _descripcionLabel; }
            set { _descripcionLabel = value; OnPropertyChanged(nameof(DescripcionLabel)); }
        }

        public string AutorLabel
        {
            get { return _autorLabel; }
            set { _autorLabel = value; OnPropertyChanged(nameof(AutorLabel)); }
        }

        public string SourcePausarAudio
        {
            get { return _sourcePausarAudio; }
            set { _sourcePausarAudio = value; OnPropertyChanged(nameof(SourcePausarAudio)); }
        }

        public long ValueSlider
        {
            get { return _valueSlider; }
            set { _valueSlider = value; OnPropertyChanged(nameof(ValueSlider)); }
        }

        public long ValueSliderMax
        {
            get { return _valueSliderMax; }
            set { _valueSliderMax = value; OnPropertyChanged(nameof(ValueSliderMax)); }
        }

        public string LabelPosicion
        {
            get { return _labelPosicion; }
            set { _labelPosicion = value; OnPropertyChanged(nameof(LabelPosicion)); }
        }

        public bool VerEdad
        {
            get { return _verEdad; }
            set { _verEdad = value; OnPropertyChanged(nameof(VerEdad)); }
        }

        public bool VerAutor
        {
            get { return _verAutor; }
            set { _verAutor = value; OnPropertyChanged(nameof(VerAutor)); }
        }

        public bool VerDuracion
        {
            get { return _verDuracion; }
            set { _verDuracion = value; OnPropertyChanged(nameof(VerDuracion)); }
        }

        public bool VerUbicacion
        {
            get { return _verUbicacion; }
            set { _verUbicacion = value; OnPropertyChanged(nameof(VerUbicacion)); }
        }

        public bool VerDescripcionLarga
        {
            get { return _verDescripcion; }
            set { _verDescripcion = value; OnPropertyChanged(nameof(VerDescripcionLarga)); }
        }

        public bool VerEquipamiento
        {
            get { return _verEquipamiento; }
            set { _verEquipamiento = value; OnPropertyChanged(nameof(VerEquipamiento)); }
        }

        public bool VerRecomendaciones
        {
            get { return _VerRecomendaciones; }
            set { _VerRecomendaciones = value; OnPropertyChanged(nameof(VerRecomendaciones)); }
        }

        public bool VerReproductorAudio
        {
            get { return _verReproductorAudio; }
            set { _verReproductorAudio = value; OnPropertyChanged(nameof(VerReproductorAudio)); }
        }

        public bool VerInciarCircuito
        {
            get { return _verInciarCircuito; }
            set { _verInciarCircuito = value; OnPropertyChanged(nameof(VerInciarCircuito)); }
        }


        public bool VerComprarCircuito
        {
            get { return _verComprarCircuito; }
            set { _verComprarCircuito = value; OnPropertyChanged(nameof(VerComprarCircuito)); }
        }

        public bool VerIconoReproductor
        {
            get { return _verIconoReproductor; }
            set { _verIconoReproductor = value; OnPropertyChanged(nameof(VerIconoReproductor)); }
        }
        public bool VerIconoBandera
        {
            get { return _verIconoBandera; }
            set { _verIconoBandera = value; OnPropertyChanged(nameof(VerIconoBandera)); }
        }


        public int PositionCarusel
        {
            get { return _positionCarusel; }
            set { _positionCarusel = value; OnPropertyChanged(nameof(PositionCarusel)); }
        }

        string oppsAlgoPaso = "Opps! Algo paso";
        string aceptar = "Aceptar";

        public InfoCircuitosViewModel(string idCircuito)
        {
            IsBusy = true;
          
            IdCircuito = "179";
            IdiomaBaseAux = IdiomaBase;
            ValueSliderMax = 1000;
            player = CrossSimpleAudioPlayer.Current;
            primeraEntrada = true;
            VerIconoBandera = true;
            VerIconoReproductor = true;

            logicaWS = new LogicaWS();
            pageServicio = new PageServicio();

            EstadoUsuario = Preferences.Get("estado_persona", "%20");
            NombreUsuario = Preferences.Get("nombre_persona", "%20");
            CorreoUsuario = Preferences.Get("correo_persona", "%20");

            BtnAudioPausar = new Command(() => BtnpausarAudio_Mto());
            BtnAudioCerrar = new Command(() => BtnCerrarAudio_Mto());
            BtnAudioIniciar = new Command(() => BtnAudioIniciar_Mto());

            BtnSelectIdioma = new Command(() => CambioIdiomaBtn_Mtd());
            BtnIniciarCircuito = new Command(() => BtnIniciarCircuito_Mto());
            BtnComprarCircuito = new Command(() => BtnComprarCircuito_Mto());
            BtnVolver = new Command(() => BtnVolver_Mtd());

            CargarIdioma_Mto(null, IdiomaBaseAux);


        }
             
        public async void ValidarInfoCircuito_Mto(string idioma)
        {
            
            try
            {
                string urli = logicaWS.Movile_Consulta_Circuito_Info_Metodo(IdCircuito, idioma);
                string JsonProcedimiento = await logicaWS.Conection(urli);
                JObject JsonObject = JObject.Parse(JsonProcedimiento);

                string Id_Circuto_WS_Vgl = JsonObject["Table"][0]["id_circuito"].Value<string>();
                string compraWS = JsonObject["Table"][0]["compra"].Value<string>();
                string valorWS = JsonObject["Table"][0]["valor"].Value<string>();
                JArray jArrayImagenes = JArray.Parse(JsonObject["Table1"].ToString());

                NombreCircuito = JsonObject["Table"][0]["nombre_circuito"].Value<string>();
                DescripcionCorta = JsonObject["Table"][0]["descripcion_corta_circuito"].Value<string>();
                DescripcionLarga = JsonObject["Table"][0]["descripcion_circuito"].Value<string>();
                Ubicacion = JsonObject["Table"][0]["ciudad"].Value<string>() + ", " + JsonObject["Table"][0]["nombre_pais"].Value<string>();
                Edad = JsonObject["Table"][0]["rango"].Value<string>();
                Duracion = JsonObject["Table"][0]["tiempo_estimado"].Value<string>();
                Equipamiento = JsonObject["Table"][0]["equipamento"].Value<string>();
                Recomendaciones = JsonObject["Table"][0]["recomendacion"].Value<string>();
                NombreAutor = JsonObject["Table"][0]["nombres_usuario"].Value<string>() + " " + JsonObject["Table"][0]["apellidos_usuario"].Value<string>();
                AutorImagen = JsonObject["Table"][0]["foto_cuenta"].Value<string>();


                if (!string.IsNullOrWhiteSpace(Ubicacion))
                    VerUbicacion = true;

                if (!string.IsNullOrWhiteSpace(Edad))
                    VerEdad = true;

                if (!string.IsNullOrWhiteSpace(Duracion))
                    VerDuracion = true;

                if (!string.IsNullOrWhiteSpace(NombreAutor) || !string.IsNullOrWhiteSpace(AutorImagen))
                    VerAutor = true;

                if (!string.IsNullOrWhiteSpace(Equipamiento) && (valorWS.ToLower().Equals("0") || compraWS.Equals("1"))) //Valida el valor del circuito y si ya esta comprado
                    VerEquipamiento = true;

                if (!string.IsNullOrWhiteSpace(DescripcionLarga) && (valorWS.ToLower().Equals("0") || compraWS.Equals("1"))) //Valida el valor del circuito y si ya esta comprado
                    VerDescripcionLarga = true;

                if (!string.IsNullOrWhiteSpace(Recomendaciones) && (valorWS.ToLower().Equals("0") || compraWS.Equals("1"))) //Valida el valor del circuito y si ya esta comprado
                    VerRecomendaciones = true;


                if (primeraEntrada)
                {
                    // Validar la compra del circuito
                    if ((valorWS.ToLower().Equals("0") || compraWS.Equals("1")))
                        VerInciarCircuito = true;
                    else
                        VerComprarCircuito = true;

                    CalificacionAutor = new ObservableCollection<Images>();
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(JsonObject["Table2"][0]["promedio"].Value<string>()))
                        {
                            int auxCalificacion = int.Parse(JsonObject["Table2"][0]["promedio"].Value<string>());
                            for (int i = 1; i <= 5; i++)
                            {
                                if (i <= auxCalificacion)
                                    CalificacionAutor.Add(new Images() { Url = "ic_estrellaBlue.png" });
                                else
                                    CalificacionAutor.Add(new Images() { Url = "ic_estrellaGris.png" });
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= 5; i++)
                                CalificacionAutor.Add(new Images() { Url = "ic_estrellaGris.png" });
                        }
                    }
                    catch
                    {
                        for (int i = 1; i <= 5; i++)
                            CalificacionAutor.Add(new Images() { Url = "ic_estrellaGris.png" });
                    }



                    CalificacionCircuito = new ObservableCollection<Images>();
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(JsonObject["Table3"][0]["calificacion_circuito"].Value<string>()))
                        {
                            int auxCalificacion = int.Parse(JsonObject["Table3"][0]["calificacion_circuito"].Value<string>());
                            for (int i = 1; i <= 5; i++)
                            {
                                if (i <= auxCalificacion)
                                    CalificacionCircuito.Add(new Images() { Url = "ic_estrellaBlue.png" });
                                else
                                    CalificacionCircuito.Add(new Images() { Url = "ic_estrellaGris.png" });
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= 5; i++)
                                CalificacionCircuito.Add(new Images() { Url = "ic_estrellaGris.png" });
                        }
                    }
                    catch
                    {
                        for (int i = 1; i <= 5; i++)
                            CalificacionCircuito.Add(new Images() { Url = "ic_estrellaGris.png" });
                    }


                    //Cargar imagenes para el carrusel del circuito
                    primeraEntrada = false;

                    ImgCarrusel = new ObservableCollection<Images>();
                    foreach (JObject item in jArrayImagenes)
                        ImgCarrusel.Add(new Images() { Url = item.GetValue("imagen").ToString() });

                    try
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(5), () => { PositionCarusel = (PositionCarusel + 1) % ImgCarrusel.Count; return true; });
                    }
                    catch { }
                }


            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert(oppsAlgoPaso, exs.Message, aceptar);
            }

            IsBusy = false;
        }

        public async void BtnIniciarCircuito_Mto()
        {
            if (!EstadoUsuario.Equals("2"))
            {
                var res = await pageServicio.DisplayAlert("AppToTrip", "Quieres iniciar el recorrido", "Si", "No");
                if (res)
                {
                    CrossSimpleAudioPlayer.Current.Stop();

                    string urli = logicaWS.Movile_Asignar_Circuito_Metodo(IdCircuito);
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
                            Preferences.Set("id_circuito", IdCircuito);
                            _ = RootPage.Navigation.PopToRootAsync(false);
                            RootPage.Detail = new NavigationPage(new RecorridoMapa());
                        }
                    }
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Ya estas realizando un recorrido ", "Ok");
            }
        }


        public async void BtnComprarCircuito_Mto()
        {
            //await pageServicio.DisplayAlert("AppToTripp", appRecursos.estamosCargandoInformacion, appRecursos.aceptar);
        }

        /*INCIO de metodos para el cambio de idioma */
        public async void CambioIdiomaBtn_Mtd()
        {
            try
            {
               // await Application.Current.MainPage.Navigation.PushPopupAsync(new PopupIdiomasCircuto(this, null));
            }
            catch (Exception exs)
            {
                //await pageServicio.DisplayAlert(oppsAlgoPaso, exs.Message, appRecursos.aceptar);
            }
        }

        public async void CargarIdioma_Mto(CultureInfo cultureInfo, string idioma)
        {
            try
            {
                IsBusy = true;
                IdiomaBaseAux = idioma;

                if (cultureInfo == null)
                    cultureInfo = new CultureInfo(CulturaBase, false);

                //appRecursos.Culture = cultureInfo;
                //SourceBanderaIdioma = appRecursos.banderaIdioma;
                //UbicacionLabel = appRecursos.textUbicacion;
                //EdadLabel = appRecursos.textRango;
                //DuracionLabel = appRecursos.textDuracion;
                //RecomendacionesLabel = appRecursos.textRecomendaciones;
                //EquipamientoLabel = appRecursos.textEquipamiento;
                //DescripcionLabel = appRecursos.textDescripcion;
                //AutorLabel = appRecursos.textAutor;
                //IniciarCircuitoLabel = appRecursos.textIniciarCircuito;
                //ComprarCircuitoLabel = appRecursos.textComprarCircuito;


                string url = logicaWS.Movile_valida_Idioma_Metodo(IdCircuito, idioma);
                string jsonProc = await logicaWS.Conection(url);
                JObject JsonObject = JObject.Parse(jsonProc);
                JArray jArray = JArray.Parse(JsonObject["Table"].ToString());
                string resultado = JsonObject["Table"][0]["valida"].Value<string>();
                if (resultado == "0")
                {
                    switch (idioma)
                    {
                        case "de":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliDe = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_DE_Metodo(IdCircuito);
                            string jsonProcedimientoDe = await logicaWS.Conection(urliDe);
                            break;

                        case "es":
                            //IsBusy = true;
                            //await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", appRecursos.aceptar);
                            //string urli = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_FR_Metodo(IdCircuito);
                            //string jsonProcedimiento = await logicaWS.Conection(urli);
                            break;

                        case "fr":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliFr = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_FR_Metodo(IdCircuito);
                            string jsonProcedimientoFr = await logicaWS.Conection(urliFr);
                            Console.WriteLine("holsa");
                            break;

                        case "en":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliEn = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_EN_Metodo(IdCircuito);
                            string jsonProcedimientoEn = await logicaWS.Conection(urliEn);
                            break;

                        case "it":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urlit = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_IT_Metodo(IdCircuito);
                            string jsonProcedimientoIt = await logicaWS.Conection(urlit);
                            break;

                        case "ja":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliJa = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_JA_Metodo(IdCircuito);
                            string jsonProcedimientoJa = await logicaWS.Conection(urliJa);
                            break;

                        case "pt":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliPt = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_PT_Metodo(IdCircuito);
                            string jsonProcedimientoPt = await logicaWS.Conection(urliPt);
                            break;

                        case "ru":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliRu = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_RU_Metodo(IdCircuito);
                            string jsonProcedimientoRu = await logicaWS.Conection(urliRu);
                            break;

                        case "tr":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliTr = logicaWS.Movile_Traducir_Campos_Circuitos_Idioma_TR_Metodo(IdCircuito);
                            string jsonProcedimientoTr = await logicaWS.Conection(urliTr);
                            break;
                        default:
                            break;
                    }
                }


                ValidarInfoCircuito_Mto(idioma);
                //appRecursos.Culture = new CultureInfo(CulturaBase, false);
            }
            catch (Exception exs)
            {
                //appRecursos.Culture = new CultureInfo(CulturaBase, false);
                await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
        }
        /*FIN de metodos para el cambio de idioma */

        /*INICIO de metodos para el audio */
        private void BtnAudioIniciar_Mto()
        {

            try
            {
                IsBusy = true;
                VerIconoBandera = false;
                VerIconoReproductor = false;
                SourcePausarAudio = "ic_pause.png";

                string audioUrl = logicaWS.Movile_Audio_Metodo("circuito", IdCircuito, "descripcion_circuito", IdiomaBaseAux);
                WebClient wc = new WebClient();
                Stream fileStream = wc.OpenRead(audioUrl);
                player.Load(fileStream);
                player.Volume = 1.0;
                player.Play();

                VerReproductorAudio = true;
                IsBusy = false;
                player.PlaybackEnded += Player_MediaEnded;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EL ERROR:" + ex);
            }


        }
        private void Player_MediaEnded(object sender, EventArgs e)
        {
            SourcePausarAudio = "ic_play.png";
        }
        private void BtnCerrarAudio_Mto()
        {
            CrossSimpleAudioPlayer.Current.Stop();
            VerIconoBandera = true;
            VerIconoReproductor = true;
            VerReproductorAudio = true;
            SourcePausarAudio = "ic_play.png";
        }
        private void BtnpausarAudio_Mto()
        {
            if (SourcePausarAudio == "ic_pause.png")
            {
                SourcePausarAudio = "ic_play.png";
                CrossSimpleAudioPlayer.Current.Pause();
            }
            else
            {
                SourcePausarAudio = "ic_pause.png";
                CrossSimpleAudioPlayer.Current.Play();
            }
        }

        public void BtnVolver_Mtd()
        {
            RootPage.Navigation.PopToRootAsync(false);
        }

    }
}

public class Images
{
    public string Url { get; set; }
}
