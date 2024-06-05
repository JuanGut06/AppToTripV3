using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using AppToTripV3.Controles;
using AppToTripV3.Services;
using Newtonsoft.Json.Linq;

namespace AppToTripV3.ViewModels
{
    public class RegistroViewModel : BaseViewModel
    {
        private readonly LogicaWS Logica_ws;
        private readonly IPageServicio pageServicio;

        private bool _verLabelDate;
        private bool _verPickerDate;
        private bool primeraEntrada;
        private bool _IsCheckTerminos;

        private string _entNombre;
        private string _entCorreo;
        private string _entCelular;

        private DateTime _entMaximumDate;
        private DateTime _entMinimumDate;
        private DateTime _entFechaNacimiento;

        public string EntNombre
        {
            get { return _entNombre; }
            set { _entNombre = value; OnPropertyChanged(nameof(EntNombre)); }
        }

        public string EntCorreo
        {
            get { return _entCorreo; }
            set { _entCorreo = value; OnPropertyChanged(nameof(EntCorreo)); }
        }
        public string EntCelular
        {
            get { return _entCelular; }
            set { _entCelular = value; OnPropertyChanged(nameof(EntCelular)); }
        }
        public bool IsCheckTerminos
        {
            get { return _IsCheckTerminos; }
            set { _IsCheckTerminos = value; OnPropertyChanged(nameof(IsCheckTerminos)); }
        }
        public bool VerPickerDate
        {
            get { return _verPickerDate; }
            set { _verPickerDate = value; OnPropertyChanged(nameof(VerPickerDate)); }
        }
        public bool VerLabelDate
        {
            get { return _verLabelDate; }
            set { _verLabelDate = value; OnPropertyChanged(nameof(VerLabelDate)); }
        }

        public DateTime EntMaximumDate
        {
            get { return _entMaximumDate; }
            set { _entMaximumDate = value; OnPropertyChanged(nameof(EntMaximumDate)); }
        }

        public DateTime EntMinimumDate
        {
            get { return _entMinimumDate; }
            set { _entMinimumDate = value; OnPropertyChanged(nameof(EntMinimumDate)); }
        }

        public DateTime EntFechaNacimiento
        {
            get { return _entFechaNacimiento; }
            set
            {
                _entFechaNacimiento = value;
                OnPropertyChanged(nameof(EntFechaNacimiento));
                if (primeraEntrada)
                {
                    VerLabelDate = false;
                    VerPickerDate = true;
                }
            }
        }


        public ICommand BtnRegistro { get; }
        public ICommand LinkPoliticas { get; }
        public ICommand FocusDatePicker { get; }
        public RegistroViewModel() 
        {
            Title = "AppToTrip";

            IsBusy = false;
            StateConexion = false;

            Logica_ws = new LogicaWS();
            pageServicio = new PageServicio();

            BtnRegistro = new Command(() => BtnRegistro_MtdAsync());
            FocusDatePicker = new Command(FocusDatePicker_Mtd);

            LinkPoliticas = new Command(() => LinkPoliticas_Mtd());

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            Inicializar_Mtd();

        }
        private async void LinkPoliticas_Mtd()
        {
            await Browser.OpenAsync("https://www.apptotrip.com/politica-de-privacidad", BrowserLaunchMode.SystemPreferred);
        }

        private void Inicializar_Mtd()
        {
            EntMaximumDate = DateTime.Now;
            EntFechaNacimiento = DateTime.Now;
            EntMinimumDate = DateTime.Now.AddYears(-100);

            VerLabelDate = true;
            primeraEntrada = true;
        }

        private async void BtnRegistro_MtdAsync()
        {
            if (!StateConexion)
            {
                IsBusy = true;
                if (await ValidarRegistro_MtoAsync())
                {
                    try
                    {
                        string tokenId = Logica_ws.ObtenerToken();
                        string urli = Logica_ws.Movile_Insert_Persona_Metodo(EntNombre, "0", EntCorreo, EntCelular, tokenId, EntFechaNacimiento.ToString("dd-MM-yyyy"));
                        string jsonProcedimiento = await Logica_ws.Conection(urli);
                        JArray jsonArray = JArray.Parse(jsonProcedimiento);
                        foreach (JObject item in jsonArray)
                        {
                            if (item.GetValue("resultado").ToString().Equals("El usuario ya esta registrado"))
                            {
                                IsBusy = false;
                                await pageServicio.DisplayAlert("AppToTrip", "El usuario ya está registrado", "Aceptar");
                            }
                            else
                            {
                                Preferences.Set("correo_persona", EntCorreo);
                                Preferences.Set("nombre_persona", EntNombre);
                                Preferences.Set("estado_persona", "1");
                                Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                            }
                        }
                    }
                    catch (Exception exs)
                    {
                        IsBusy = false;
                        await pageServicio.DisplayAlert("Opps! Algo paso", exs.Message, "Aceptar");
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Comprueba tu conexión a internet", "Aceptar");
            }
        }

        private async Task<bool> ValidarRegistro_MtoAsync()
        {
            bool resultado;
            char[] charsToTrim = { ' ' };
            string expreregularCelular = @"^[0-9]*$";
            string expreregularLetras = @"^([a-zA-Z ñáéíóúÑÁÉÍÓÚ]{1,99})$";
            string expreregularCorreo = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (string.IsNullOrWhiteSpace(EntNombre) || string.IsNullOrWhiteSpace(EntCorreo) || string.IsNullOrWhiteSpace(EntCelular))
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "No dejes campos vacíos", "Aceptar");
                return false;
            }

            // Validar Fecha
            if (VerLabelDate)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "No dejes campos vacíos", "Aceptar");
                return false;
            }

            // Validar Nombre
            resultado = Regex.IsMatch(EntNombre.Trim(charsToTrim), expreregularLetras, RegexOptions.IgnoreCase);
            if (!resultado)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "El formato del nombre es incorrecto", "Aceptar");
                return false;
            }

            // Validar Correo
            resultado = Regex.IsMatch(EntCorreo.Trim(charsToTrim), expreregularCorreo, RegexOptions.IgnoreCase);
            if (!resultado)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "El formato del correo electrónico es incorrecto", "Aceptar");
                return false;
            }

            // Validar Telefono
            resultado = Regex.IsMatch(EntCelular.Trim(charsToTrim), expreregularCelular, RegexOptions.IgnoreCase);
            if (!resultado)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "El formato del télefono es incorrecto", "Aceptar");
                return false;
            }

            if (!IsCheckTerminos)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "Acepto los términos y condiciones de las políticas de tratamiento de datos.", "Aceptar");
                return false;
            }

            return true;
        }

        private void FocusDatePicker_Mtd(object obj)
        {
            CustomPickerDate DatePicker = (CustomPickerDate)obj;
            DatePicker.Focus();
        }

    }
}