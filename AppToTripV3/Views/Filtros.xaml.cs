using AppToTripV3.Services;
using AppToTripV3.ViewModels;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;


namespace AppToTripV3.Views
{


    public partial class Filtros : ContentPage
    {
        private LogicaWS LogicaWS;
        private ObservableCollection<string> ListaDestinos;

        float maxValue = 1;
        float progressmax = 10;
        bool istimerRunning = true;
        float progress = 0;
        int counter = 1;
        public Filtros()
        {
            InitializeComponent();
            BindingContext = new FiltrosViewModel();
            Lista_Destinos_Mto();
        }
        async void Lista_Destinos_Mto()
        {

            try
            {
                LogicaWS = new LogicaWS();
                ListaDestinos = new ObservableCollection<string>();
                string urliDestino = LogicaWS.Movile_Consulta_Ciudades_Cp_Metodo();
                string jsonProcedimientoDestino = await LogicaWS.Conection(urliDestino);
                JArray jsonArrayDestino = JArray.Parse(jsonProcedimientoDestino);
                try
                {
                    foreach (JObject item in jsonArrayDestino)
                    {
                        string resultadoDestinosCiudad = item.GetValue("nombre_ciudad").ToString();
                        string resultadoDestinosPais = item.GetValue("nombre_pais").ToString();
                        ListaDestinos.Add(resultadoDestinosCiudad + " - " + resultadoDestinosPais);

                    }
                    //AutoSuggestBoxName.ItemsSource = ListaDestinos;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Opps! Algo paso", ex.Message, "Aceptar");
                }
            }
            catch (Exception exs)
            {
                await DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
            }
        }

    }
}