using AppToTripV3.Models;
using AppToTripV3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppToTripV3.ViewModels;
using Plugin.SimpleAudioPlayer;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Globalization;
using AppToTripV3.RecursosIdioma;

namespace AppToTripV3.ViewModels
{
    public class InfoSitiosViewModel : BaseViewModel
    {
        private readonly string IdSitio;
        private readonly LogicaWS logicaWS;
        private readonly IPageServicio pageServicio;
        private RecorridoMapaViewModel recorridoMapaView;

        private ObservableCollection<Images> _imgCarrusel;
        public ObservableCollection<Sitio> _listaSitios;
        
        ISimpleAudioPlayer player;
        private long _valueSlider;
        private long _valueSliderMax;


        private string Bandera_;
        private string IdiomaBaseAux;
        private string _horario;
        private string _direccion;
        private string _Telefono;
        private string _duracion;
        private string _equipamiento;
        private string _ordenWS;

        private string _nombreSitio;
        private string _recomendaciones;
        private string _descripcionCorta;
        private string _descripcionLarga;
        private string _like;
        private string _disLike;
        private string _sourceBanderaIdioma;
        private string _sourcePausarAudio;
        private string _horarioLabel;
        private string _direccionLabel;
        private string _TelefonoLabel;
        private string _duracionLabel;
        private string _recomendacionesLabel;
        private string _equipamientoLabel;
        private string _descripcionLabel;
        private string _labelPosicion;
        private string _estadoSitio;

        private int _positionCarusel;

        private bool primeraLlamada = true;
        private bool _verHorario;
        private bool _verDireccion;
        private bool _verTelefono;
        private bool _verDuracion;
        private bool _verDescripcion;
        private bool _verEquipamiento;
        private bool _VerRecomendaciones;
        private bool _verIconoBandera;
        private bool primeraEntrada;
        private bool _verIconoReproductor;
        private bool _verReproductorAudio;
        private bool _btnSiguienteVisible;

        public ICommand BtnAudioPausar { get; }
        public ICommand BtnAudioCerrar { get; }
        public ICommand BtnAudioIniciar { get; }
        public ICommand BtnSelectIdioma { get; }
        public ICommand BtnVolver { get; }
        public ICommand BtnNextSitio { get; }

        public ICommand Btnwaze { get; }
        public ICommand BtnGoogleMaps { get; }

     
        public ObservableCollection<Sitio> ListaSitios
        {
            get { return _listaSitios; }
            set { _listaSitios = value; OnPropertyChanged(nameof(ListaSitios)); }
        }
        public ObservableCollection<Images> ImgCarrusel
        {
            get { return _imgCarrusel; }
            set { _imgCarrusel = value; OnPropertyChanged(nameof(ImgCarrusel)); }
        }

        public string Horario
        {
            get { return _horario; }
            set { _horario = value; OnPropertyChanged(nameof(Horario)); }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; OnPropertyChanged(nameof(Direccion)); }
        }
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; OnPropertyChanged(nameof(Telefono)); }
        }
        public string Duracion
        {
            get { return _duracion; }
            set { _duracion = value; OnPropertyChanged(nameof(Duracion)); }
        }

        public string OrdenWS
        {
            get { return _ordenWS; }
            set { _ordenWS = value; OnPropertyChanged(nameof(OrdenWS)); }
        }
        public string Equipamiento
        {
            get { return _equipamiento; }
            set { _equipamiento = value; OnPropertyChanged(nameof(Equipamiento)); }
        }
        public string NombreSitio
        {
            get { return _nombreSitio; }
            set { _nombreSitio = value; OnPropertyChanged(nameof(NombreSitio)); }
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
        public string HorarioLabel
        {
            get { return _horarioLabel; }
            set { _horarioLabel = value; OnPropertyChanged(nameof(HorarioLabel)); }
        }
        public long ValueSlider
        {
            get { return _valueSlider; }
            set { _valueSlider = value; OnPropertyChanged(nameof(ValueSlider)); }
        }

        public string DireccionLabel
        {
            get { return _direccionLabel; }
            set { _direccionLabel = value; OnPropertyChanged(nameof(DireccionLabel)); }
        }
        public string TelefonoLabel
        {
            get { return _TelefonoLabel; }
            set { _TelefonoLabel = value; OnPropertyChanged(nameof(TelefonoLabel)); }
        }
        public string DuracionLabel
        {
            get { return _duracionLabel; }
            set { _duracionLabel = value; OnPropertyChanged(nameof(DuracionLabel)); }
        }

        public string EstadoSitio
        {
            get { return _estadoSitio; }
            set { _estadoSitio = value; OnPropertyChanged(nameof(EstadoSitio)); }
        }
        public string LabelPosicion
        {
            get { return _labelPosicion; }
            set { _labelPosicion = value; OnPropertyChanged(nameof(LabelPosicion)); }
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

        public string Like
        {
            get { return _like; }
            set { _like = value; OnPropertyChanged(nameof(Like)); }
        }
        public string DisLike
        {
            get { return _disLike; }
            set { _disLike = value; OnPropertyChanged(nameof(DisLike)); }
        }

        public string SourceBanderaIdioma
        {
            get { return _sourceBanderaIdioma; }
            set { _sourceBanderaIdioma = value; OnPropertyChanged(nameof(SourceBanderaIdioma)); }
        }

        public string SourcePausarAudio
        {
            get { return _sourcePausarAudio; }
            set { _sourcePausarAudio = value; OnPropertyChanged(nameof(SourcePausarAudio)); }
        }

        public int PositionCarusel
        {
            get { return _positionCarusel; }
            set { _positionCarusel = value; OnPropertyChanged(nameof(PositionCarusel)); }
        }

        public bool VerHorario
        {
            get { return _verHorario; }
            set { _verHorario = value; OnPropertyChanged(nameof(VerHorario)); }
        }

        public bool BtnSiguienteVisible
        {
            get { return _btnSiguienteVisible; }
            set { _btnSiguienteVisible = value; OnPropertyChanged(nameof(BtnSiguienteVisible)); }
        }
        public bool VerReproductorAudio
        {
            get { return _verReproductorAudio; }
            set { _verReproductorAudio = value; OnPropertyChanged(nameof(VerReproductorAudio)); }
        }

        public bool VerDireccion
        {
            get { return _verDireccion; }
            set { _verDireccion = value; OnPropertyChanged(nameof(VerDireccion)); }
        }

        public bool VerTelefono
        {
            get { return _verTelefono; }
            set { _verTelefono = value; OnPropertyChanged(nameof(VerTelefono)); }
        }

        public bool VerDuracion
        {
            get { return _verDuracion; }
            set { _verDuracion = value; OnPropertyChanged(nameof(VerDuracion)); }
        }

        public bool VerDescripcion
        {
            get { return _verDescripcion; }
            set { _verDescripcion = value; OnPropertyChanged(nameof(VerDescripcion)); }
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

        public bool VerIconoBandera
        {
            get { return _verIconoBandera; }
            set { _verIconoBandera = value; OnPropertyChanged(nameof(VerIconoBandera)); }
        }
        public bool VerIconoReproductor
        {
            get { return _verIconoReproductor; }
            set { _verIconoReproductor = value; OnPropertyChanged(nameof(VerIconoReproductor)); }
        }

        public long ValueSliderMax
        {
            get { return _valueSliderMax; }
            set { _valueSliderMax = value; OnPropertyChanged(nameof(ValueSliderMax)); }
        }
        public InfoSitiosViewModel(string idSitio, string Bandera, RecorridoMapaViewModel recorridoMapaView_)
        {
            IsBusy = true;
            recorridoMapaView = recorridoMapaView_;
            Bandera_ = Bandera;
            IdSitio = "897";
            IdiomaBaseAux = IdiomaBase;
            ValueSliderMax = 1000;
            primeraEntrada = true;
            VerIconoBandera = true;
            VerIconoReproductor = true;

            logicaWS = new LogicaWS();
            pageServicio = new PageServicio();
            ListaSitios = new ObservableCollection<Sitio>();
            player = CrossSimpleAudioPlayer.Current;
            BtnVolver = new Command(() => BtnVolver_Mtd());
            BtnAudioPausar = new Command(() => BtnpausarAudio_Mto());
            BtnAudioCerrar = new Command(() => BtnCerrarAudio_Mto());
            BtnAudioIniciar = new Command(() => BtnAudioIniciar_Mto());
            BtnSelectIdioma = new Command(() => CambioIdiomaBtn_Mtd());
            BtnNextSitio = new Command(() => BtnNextSitio_Mtd());
            CargarIdioma_Mto(null, IdiomaBaseAux);
            Btnwaze = new Command(() => Btnwaze_Mtd());
            BtnGoogleMaps = new Command(() => BtnGoogleMaps_Mtd());
            
            player.Loop = false;
            player.PlaybackEnded += Player_PlaybackEnded;
            string idCircuito = Preferences.Get("id_circuito", "");

        }
        async void ValidarInfoSitio_Mto(string idioma)
        {
            string oppsAlgoPaso = "Opps! Algo paso";
            string aceptar = "Aceptar";
            try
            {
                string urli = logicaWS.Movile_Consulta_Sitio_Info_Metodo(IdSitio, idioma);
                string jsonProcedimiento = await logicaWS.Conection(urli);
                JObject jsonObject = JObject.Parse(jsonProcedimiento);

                string id_sitioWS = jsonObject["Table"][0]["id_sitio"].Value<string>();
                NombreSitio = jsonObject["Table"][0]["nombre_sitio"].Value<string>();
                DescripcionLabel = jsonObject["Table"][0]["descripcion_sitio"].Value<string>();
                DescripcionCorta = jsonObject["Table"][0]["descripcion_corta_sitio"].Value<string>();
                Recomendaciones = jsonObject["Table"][0]["recomendacion"].Value<string>();
                Equipamiento = jsonObject["Table"][0]["equipamento"].Value<string>();
                Horario = jsonObject["Table"][0]["horario"].Value<string>();
                Duracion = jsonObject["Table"][0]["tiempo_estimado"].Value<string>();
                Direccion = jsonObject["Table"][0]["direccion"].Value<string>();
                Telefono = jsonObject["Table"][0]["telefono"].Value<string>();
                DisLike = jsonObject["Table"][0]["dislike"].Value<string>();
                Like = " ";
                string costoWs = jsonObject["Table"][0]["costo"].Value<string>();
                string codigo_idiomaWS = jsonObject["Table"][0]["codigo_idioma"].Value<string>();
                OrdenWS = jsonObject["Table"][0]["orden"].Value<string>();
                string compraWS = jsonObject["Table"][0]["compra"].Value<string>();
                string promedioWS = jsonObject["Table"][0]["promedio"].Value<string>();
                EstadoSitio = jsonObject["Table"][0]["estado"].Value<string>();

                JArray jArrayImagenes = JArray.Parse(jsonObject["Table1"].ToString());

                if (!string.IsNullOrWhiteSpace(Duracion))
                    VerDuracion = true;

                if (!string.IsNullOrWhiteSpace(Recomendaciones))
                    VerRecomendaciones = true;

                if (!string.IsNullOrWhiteSpace(Equipamiento))
                    VerEquipamiento = true;

                if (!string.IsNullOrWhiteSpace(Horario))
                    VerHorario = true;

                if (!string.IsNullOrWhiteSpace(Direccion))
                    VerDireccion = true;

                if (!string.IsNullOrWhiteSpace(Telefono))
                    VerTelefono = true;

                if (!string.IsNullOrWhiteSpace(DescripcionLarga))
                    VerDescripcion = true;

                try
                {
                    //if (!string.IsNullOrWhiteSpace(NombreSitio))
                    //    NombreSitio = await claseBase.Traducir_ContenidoAsync(NombreSitio, "es", idioma);

                    //if (!string.IsNullOrWhiteSpace(Duracion))
                    //    Duracion = await claseBase.Traducir_ContenidoAsync(Duracion, "es", idioma);

                    //if (!string.IsNullOrWhiteSpace(Direccion))
                    //    Direccion = await claseBase.Traducir_ContenidoAsync(Direccion, "es", idioma);
                }
                catch { }


                if (primeraEntrada)
                {
                    primeraEntrada = false;
                    ImgCarrusel = new ObservableCollection<Images>();
                    foreach (JObject item in jArrayImagenes)
                    {
                        ImgCarrusel.Add(new Images() { Url = item.GetValue("imagen").ToString() });
                    }

                    try
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(5), () => { PositionCarusel = (PositionCarusel + 1) % ImgCarrusel.Count; return true; });
                    }
                    catch { }


                    if (Bandera_ == "2")
                    {
                        BtnAudioIniciar_Mto();
                        BtnSiguienteVisible = true;
                    }

                }

            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert(oppsAlgoPaso, exs.Message, aceptar);
            }
            IsBusy = false;
        }

        private void BtnNextSitio_Mtd()
        {
            //_ = recorridoMapaView.Finalizar_SitioAsync(EstadoSitio, IdSitio);
            RootPage.Navigation.PopToRootAsync(false);
            CrossSimpleAudioPlayer.Current.Stop();
        }

        /*INCIO de metodos para el cambio de idioma */
        public async void CambioIdiomaBtn_Mtd()
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushPopupAsync(new PopupIdiomasCircuto(null, this));
            }
            catch (Exception exs)
            {
                await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
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

                appRecursos.Culture = cultureInfo;
                SourceBanderaIdioma = appRecursos.banderaIdioma;
                DuracionLabel = appRecursos.textDuracion;
                RecomendacionesLabel = appRecursos.textRecomendaciones;
                EquipamientoLabel = appRecursos.textEquipamiento;
                HorarioLabel = appRecursos.textHorario;
                DireccionLabel = appRecursos.textDireccion;
                TelefonoLabel = appRecursos.textTelefono;
                DescripcionLabel = appRecursos.textDescripcion;

                string url = logicaWS.Movile_valida_Idioma_sitio_Metodo(IdSitio, idioma);
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
                            string urliDe = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_DE_Metodo(IdSitio);
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
                            string urliFr = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_FR_Metodo(IdSitio);
                            string jsonProcedimientoFr = await logicaWS.Conection(urliFr);
                            break;

                        case "en":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliEn = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_EN_Metodo(IdSitio);
                            string jsonProcedimientoEn = await logicaWS.Conection(urliEn);
                            break;

                        case "it":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urlit = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_IT_Metodo(IdSitio);
                            string jsonProcedimientoIt = await logicaWS.Conection(urlit);
                            break;

                        case "ja":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliJa = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_JA_Metodo(IdSitio);
                            string jsonProcedimientoJa = await logicaWS.Conection(urliJa);
                            break;

                        case "pt":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliPt = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_PT_Metodo(IdSitio);
                            string jsonProcedimientoPt = await logicaWS.Conection(urliPt);
                            break;

                        case "ru":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliRu = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_RU_Metodo(IdSitio);
                            string jsonProcedimientoRu = await logicaWS.Conection(urliRu);
                            break;

                        case "tr":
                            IsBusy = true;
                            await pageServicio.DisplayAlert("AppToTripp", "Traduciendo información", "Aceptar");
                            string urliTr = logicaWS.Movile_Traducir_Campos_Sitio_Idioma_TR_Metodo(IdSitio);
                            string jsonProcedimientoTr = await logicaWS.Conection(urliTr);
                            break;

                        default:
                            break;
                    }

                }
                ValidarInfoSitio_Mto(idioma);

                appRecursos.Culture = new CultureInfo(CulturaBase, false);
            }
            catch (Exception exs)
            {
                appRecursos.Culture = new CultureInfo(CulturaBase, false);
                await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
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

                string audioUrl = logicaWS.Movile_Audio_Metodo("sitio", IdSitio, "descripcion_sitio", IdiomaBaseAux);
                WebClient wc = new WebClient();
                Stream fileStream = wc.OpenRead(audioUrl);
                player.Load(fileStream);
                player.Volume = 1.0;
                player.Play();

                VerReproductorAudio = true;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EL ERROR:" + ex);
            }

        }


        private void Player_PlaybackEnded(object sender, EventArgs e)
        {
            SourcePausarAudio = "ic_play.png";
        }

        //private void Current_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        SetupCurrentMediaPositionData(e.Position);
        //    });

        //}
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

        private void BtnCerrarAudio_Mto()
        {
            //await CrossMediaManager.Current.Stop();
            CrossSimpleAudioPlayer.Current.Stop();
            VerIconoBandera = true;
            VerIconoReproductor = true;
            VerReproductorAudio = true;
            SourcePausarAudio = "ic_play.png";
        }
        
        private async void Btnwaze_Mtd()
        {

            for (var i = 0; i < ListaSitios.Count(); i++)
            {
                if (IdSitio == ListaSitios[i].Id_Sitio)
                {
                    string wazeUrl = string.Format("https://www.waze.com/ul?ll={0},{1}&navigate=yes", ListaSitios[i].Latitud, ListaSitios[i].Longitud);
                    //Device.OpenUri(new Uri(wazeUrl));
                    await Launcher.OpenAsync(new Uri(wazeUrl));

                }

            }
        }

        private async void BtnGoogleMaps_Mtd()
        {
            for (var i = 0; i < ListaSitios.Count(); i++)
            {
                if (IdSitio == ListaSitios[i].Id_Sitio)
                {

                    var latitud = ListaSitios[i].Latitud;
                    var longitud = ListaSitios[i].Longitud;

                    var uri = $"https://www.google.com/maps/search/?api=1&query={latitud},{longitud}";

                    await Launcher.OpenAsync(new Uri(uri));
                }

            }


        }
        public void BtnVolver_Mtd()
        {
            // await CrossMediaManager.Current.Stop();
            CrossSimpleAudioPlayer.Current.Stop();
            _ = RootPage.Navigation.PopToRootAsync(false);
        }

    }
}
