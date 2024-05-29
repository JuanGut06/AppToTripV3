using AppToTripV3.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;
using AppToTripV3.Views;
using AppToTripV3.Interface;
using Plugin.GoogleClient.Shared;

namespace AppToTripV3.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LogicaWS Logica_WS_Vgl;
        private readonly IPageServicio pageServicio;

        private string _entCorreo;

        public ICommand OnLoginWithCorreoCommand { get; }
        public ICommand Crear_Cuenta { get; }
        public ICommand Buscar_Cuenta { get; }

        public ICommand ReintentarConexion { get; }


        /*Inicio Redes Sociales*/
        public ICommand OnLoginWithGoogleCommand { get; set; }
        public ICommand OnLoginWithFacebookCommand { get; set; }

        readonly IFacebookClient _facebookService = CrossFacebookClient.Current;
        readonly IGoogleClientManager _googleService = CrossGoogleClient.Current;
        /*Fin Redes Sociales*/

        public string EntCorreo
        {
            get { return _entCorreo; }
            set { _entCorreo = value; OnPropertyChanged(nameof(EntCorreo)); }
        }

        public LoginViewModel()
        {
            Title = "Login";

            StateConexion = false;

            Logica_WS_Vgl = new LogicaWS();
            pageServicio = new PageServicio();


            //Crear_Cuenta = new Command(async () => await Crear_Cuenta_Link_MtoAsync());
            //Buscar_Cuenta = new Command(async () => await Buscar_Cuenta_Link_MtoAsync());

            //OnLoginWithGoogleCommand = new Command(async () => await LoginGoogleAsync());
            //OnLoginWithFacebookCommand = new Command(async () => await LoginFacebookAsync());
            OnLoginWithCorreoCommand = new Command(async () => await IngresarCorreo_MtoAsync());



            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        /*Modulo ingreso por correo*/
        private async Task IngresarCorreo_MtoAsync()
        {
            try
            {

                if (!StateConexion)
                {
                    IsBusy = true;
                    Preferences.Set("correo_persona", EntCorreo);
                    string correo = EntCorreo;
                    if (await Validar_Formulario_Mto())
                    {
                        try
                        {
                            string tokenId = Logica_WS_Vgl.ObtenerToken();
                            string urli = Logica_WS_Vgl.Movile_Consulta_Personas_Metodo(correo, tokenId);
                            string JsonProcedimiento = await Logica_WS_Vgl.Conection(urli);
                            JObject jsonObject = JObject.Parse(JsonProcedimiento);
                            Validar_Login_Mto(jsonObject);
                        }
                        catch (Exception exs)
                        {
                            IsBusy = false;
                            //await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, exs.Message, appRecursos.aceptar);
                        }
                    }
                }
                else
                {
                    await pageServicio.DisplayAlert("AppToTrip", "Comprueba tu internet", "Aceptar");

                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                // await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, ex.Message, appRecursos.aceptar);
            }
        }

        private void Validar_Login_Mto(JObject jsonObject)
        {
            try
            {
                Preferences.Set("correo_persona", EntCorreo);
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
                pageServicio.DisplayAlert("AppToTrip", "Cuenta no encontrada con este dispositivo", "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<bool> Validar_Formulario_Mto()
        {

            if (string.IsNullOrWhiteSpace(EntCorreo))
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "No dejes campos vacíos", "Aceptar");
                return false;
            }

            bool isEmail = Regex.IsMatch(EntCorreo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", "El formato del correo electrónico es incorrecto", "Aceptar");
                return false;
            }
            return true;
        }

        /*Fin Correo*/

        /*Modulo GMail*/
        async Task LoginGoogleAsync()
        {
            try
            {
                IsBusy = true;
                _googleService.Logout();

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;              
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:

                            var googleUserString = JsonConvert.SerializeObject(e.Data);

                            var socialLoginData = new NetworkAuthData
                            {
                                Id = e.Data.Id,
                                Picture = e.Data.Picture.AbsoluteUri,
                                Name = e.Data.Name,
                                Email = e.Data.Email
                            };
                            try
                            {
                                string tokenId = Logica_WS_Vgl.ObtenerToken();
                                string urli = Logica_WS_Vgl.Movile_Consulta_Personas_Metodo(socialLoginData.Email, tokenId);
                                string JsonProcedimiento = await Logica_WS_Vgl.Conection(urli);
                                JObject jsonObject = JObject.Parse(JsonProcedimiento);
                                Validar_Google_MtoAsync(jsonObject, socialLoginData);
                            }
                            catch (Exception exs)
                            {
                                _googleService.Logout();
                                //await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, exs.Message, appRecursos.aceptar);
                            }

                            break;
                        case GoogleActionStatus.Canceled:
                            IsBusy = false;
                            await pageServicio.DisplayAlert("Google Auth", "Canceled", "Aceptar");
                            break;
                        case GoogleActionStatus.Error:
                            IsBusy = false;
                            await pageServicio.DisplayAlert("Google Auth", "Error" + GoogleActionStatus.Error, "Aceptar");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            IsBusy = false;
                            await pageServicio.DisplayAlert("Google Auth", "Unauthorized", "Aceptar");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                IsBusy = false;
                //await pageServicio.DisplayAlert("Google Auth", ex.Message, appRecursos.aceptar);
                Console.WriteLine("el error es " + ex);
                await pageServicio.DisplayAlert("AppToTrip", "Revisa tu conexión a internet", "Ok");
            }
        }

        private async void Validar_Google_MtoAsync(JObject jsonObject, NetworkAuthData socialLoginData)
        {
            try
            {
                string attrib1Value = jsonObject["Table"][0]["resultado"].Value<string>();
                try
                {
                    string tokenId = Logica_WS_Vgl.ObtenerToken();
                    string urli = Logica_WS_Vgl.Movile_Insert_Persona_Metodo(socialLoginData.Name, "%20", socialLoginData.Email, "%20", tokenId, "%20");
                    string JsonProcedimiento = await Logica_WS_Vgl.Conection(urli);
                    JArray jsonArray = JArray.Parse(JsonProcedimiento);
                    Validar_Registro_Mto(jsonArray, socialLoginData);
                }
                catch (Exception exs)
                {
                    IsBusy = false;
                    _googleService.Logout();
                    // await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, exs.Message, appRecursos.aceptar);
                }
            }
            catch
            {
                try
                {
                    Preferences.Set("correo_persona", socialLoginData.Email);
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
                catch (Exception ex)
                {
                    IsBusy = false;
                    _googleService.Logout();
                    // await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, ex.Message, appRecursos.aceptar);
                }
            }
        }

        private void Validar_Registro_Mto(JArray jsArray, NetworkAuthData socialLoginData)
        {
            string resultado = "";

            try
            {
                foreach (JObject item in jsArray)
                    resultado = item.GetValue("resultado").ToString();

                if (resultado.Equals("Usuario registrado exitosamente"))
                {

                    Preferences.Set("correo_persona", socialLoginData.Email);
                    Preferences.Set("nombre_persona", socialLoginData.Name);
                    Preferences.Set("estado_persona", "1");
                    Application.Current.MainPage = new NavigationPage(new MasterHomePage());
                }
                else if (resultado.Equals("El usuario ya esta registrado"))
                {
                    IsBusy = false;
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            pageServicio.DisplayAlert("AppToTrip", "Verifica con otra cuenta, esta no coincide con este telefono.", "Aceptar");
                            break;
                        case Device.Android:
                            pageServicio.DisplayAlert("AppToTrip", "Verifica con otra cuenta, esta no coincide con este telefono.", "Aceptar");
                            //DependencyService.Get<IMensajes>().ToastShortAlert(resultado);
                            break;
                    }
                }
                else
                {
                    IsBusy = false;
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            pageServicio.DisplayAlert("AppToTrip", resultado, "Aceptar"); ;
                            break;
                        case Device.Android:
                            DependencyService.Get<IMensajes>().ToastShortAlert(resultado);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _googleService.Logout();
                // pageServicio.DisplayAlert("Opps! Algo paso con tus datos", ex.Message, appRecursos.aceptar);
            }

        }
        /*Fin Gmail*/



        /*Crear cuenta nueva formulario*/
        async Task Crear_Cuenta_Link_MtoAsync()
        {
            if (!StateConexion)
            {
                await pageServicio.PushAsync(new Registro());
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Comprueba tu conexión a internet", "Aceptar");
            }
        }

        async Task Buscar_Cuenta_Link_MtoAsync()
        {
            if (!StateConexion)
            {
                await pageServicio.PushAsync(new IngresarCorreo());
            }
            else
            {
                await pageServicio.DisplayAlert("AppToTrip", "Comprueba tu conexión a internet", "Aceptar");
            }
        }

    }

    public class NetworkAuthData
    {
        public string Id { get; set; }
        public object Logo { get; set; }
        public object Foreground { get; set; }
        public object Background { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class FacebookProfile
    {
        public string Email { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
    }
}