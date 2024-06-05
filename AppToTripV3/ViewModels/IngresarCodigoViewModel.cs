using AppToTripV3.RecursosIdioma;
using AppToTripV3.Services;
using AppToTripV3.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppToTripV3.Models;

namespace AppToTripV3.ViewModels
{
    public class IngresarCodigoViewModel : BaseViewModel
    {
        private readonly LogicaWS Logica_WS_Vgl;
        private readonly IPageServicio pageServicio;

        private string _email;
        private string verificationCode;
        private string _txtcodigo;
        private Label _reenviaCode;
        private Label _timerLabel;
        private string _CodigoPersona;

        private Random random = new Random();
        public ICommand BtnRecoverCodigo { get; }

        public ICommand LnkReCodigo { get; }

        public string CorreoText
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(CorreoText)); }
        }

        public string Txtcodigo
        {
            get { return _txtcodigo; }
            set { _txtcodigo = value; OnPropertyChanged(nameof(Txtcodigo)); }
        }

        public Label TxtreenviaCode
        {
            get { return _reenviaCode; }
            set { _reenviaCode = value; OnPropertyChanged(nameof(TxtreenviaCode)); }
        }

        public Label timerLabel
        {
            get { return _timerLabel; }
            set { _timerLabel = value; OnPropertyChanged(nameof(timerLabel)); }
        }
        public string CodigoPersona
        {
            get { return _CodigoPersona; }
            set { _CodigoPersona = value; OnPropertyChanged(); }
        }

        public IngresarCodigoViewModel(string email, string verificationCode)
        {
            CorreoText = email;
            this.verificationCode = verificationCode;

            StateConexion = false;
            Logica_WS_Vgl = new LogicaWS();
            pageServicio = new PageServicio();

            BtnRecoverCodigo = new Command(async () => await BtnRecoverCodigo_Mto());
            LnkReCodigo = new Command(async () => await LnkReCodigo_Mto());

        }

        private async Task LnkReCodigo_Mto()
        {
            try
            {
                if (!StateConexion)
                {
                    IsBusy = true;

                    try
                    {
                        string urli = Logica_WS_Vgl.Movile_consulta_persona_Recuperar_Metodo(CorreoText);
                        string JsonProcedimiento = await Logica_WS_Vgl.Conection(urli);
                        JObject jsonObject = JObject.Parse(JsonProcedimiento);
                        string resultado = jsonObject.GetValue("Table").ToString();
                        JArray jsonObject1 = JArray.Parse(resultado);
                        foreach (JObject item in jsonObject1)
                        {
                            Persona persona = new Persona(
                                item.GetValue("nombres_persona").ToString(),
                                item.GetValue("apellidos_persona").ToString(),
                                item.GetValue("id_persona").ToString());

                            CodigoPersona = item.GetValue("id_persona").ToString();
                        }
                        //ENVIO DE CORREO CON EL CODIGO
                        string CorreoUrl = Logica_WS_Vgl.Movile_envio_correo_persona_Metodo("1", CorreoText, CodigoPersona);
                        verificationCode = Convert.ToString(random.Next(1000, 9999)); // Genera un número aleatorio de 4 dígitos
                        Console.WriteLine("REENVIO ---------->>>>>>> = " + verificationCode);
                        await pageServicio.DisplayAlert("CÓDIGO", verificationCode, appRecursos.aceptar);
                        await pageServicio.PushAsync(new IngresarCodigo(CorreoText, verificationCode));
                    }
                    catch (Exception exs)
                    {
                        IsBusy = false;
                        await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, exs.Message, appRecursos.aceptar);
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert("AppToTrip", appRecursos.compruebaTuConexionAInternet, appRecursos.aceptar);

                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, ex.Message, appRecursos.aceptar);
            }
        }

        private async Task BtnRecoverCodigo_Mto()
        {
            try
            {
                if (!StateConexion)
                {


                    if (Txtcodigo == verificationCode)
                    {
                        // Código válido             

                        string tokenId = Logica_WS_Vgl.ObtenerToken();
                        string urli = Logica_WS_Vgl.Movile_Actualiza_persona_Metodo(CorreoText, tokenId);


                        string urliConsulta = Logica_WS_Vgl.Movile_Consulta_Personas_Metodo(CorreoText, tokenId);
                        string JsonConsulta = await Logica_WS_Vgl.Conection(urliConsulta);
                        JObject jsonPersona = JObject.Parse(JsonConsulta);
                        Validar_Login_Mto(jsonPersona);
                    }
                    else
                    {
                        if (Txtcodigo == "" || Txtcodigo == null)
                        {
                            await pageServicio.DisplayAlert("AppToTrip", appRecursos.labelCodigo4Digitos + "\n" + CorreoText, appRecursos.aceptar);
                        }
                        else
                        {
                            // Código inválido              
                            await pageServicio.DisplayAlert("AppToTrip", appRecursos.codigoNOValido, appRecursos.aceptar);
                        }
                    }
                    Txtcodigo = "";
                }
                else
                {
                    await pageServicio.DisplayAlert("AppToTrip", appRecursos.compruebaTuConexionAInternet, appRecursos.aceptar);

                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, ex.Message, appRecursos.aceptar);
            }

        }


        private void Validar_Login_Mto(JObject jsonObject)
        {
            try
            {
                Preferences.Set("correo_persona", CorreoText);
                Preferences.Set("nombre_persona", jsonObject["Table"][0]["nombres_persona"].Value<string>());
                Preferences.Set("estado_persona", jsonObject["Table"][0]["estado"].Value<string>());
                if (jsonObject["Table"][0]["estado"].Value<string>().Equals("2"))
                {
                    Preferences.Set("id_circuito", jsonObject["Table1"][0]["fk_circuito"].Value<string>());
                    Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                    RootPage.Detail = new NavigationPage(new RecorridoMapa());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                }
            }
            catch
            {
                IsBusy = false;
                pageServicio.DisplayAlert("AppToTrip", appRecursos.cuentaNoEncontradaConEsteDispositivo, appRecursos.aceptar);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
