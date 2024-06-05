using AppToTripV3.Models;
using AppToTripV3.RecursosIdioma;
using AppToTripV3.Services;
using AppToTripV3.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppToTripV3.ViewModels
{
    public class IngresarCorreoViewModel : BaseViewModel
    {
        private readonly LogicaWS Logica_WS_Vgl;
        private readonly IPageServicio pageServicio;

        private Random random = new Random();
        private string verificationCode;

        private string _entCorreo;
        private string _CodigoPersona;
        public ICommand BtnRecoverCorreo { get; }

        public string EntCorreo
        {
            get { return _entCorreo; }
            set { _entCorreo = value; OnPropertyChanged(nameof(EntCorreo)); }
        }
        public string CodigoPersona
        {
            get { return _CodigoPersona; }
            set { _CodigoPersona = value; OnPropertyChanged(); }
        }


        public IngresarCorreoViewModel()
        {
            StateConexion = false;

            Logica_WS_Vgl = new LogicaWS();
            pageServicio = new PageServicio();
            BtnRecoverCorreo = new Command(async () => await BtnRecoverCorreo_Mto());

        }

        /*Modulo ingreso correo*/
        private async Task BtnRecoverCorreo_Mto()
        {
            try
            {
                if (!StateConexion)
                {
                    IsBusy = true;
                    string correo = EntCorreo;
                    if (await Validar_Formulario_Mto())
                    {
                        try
                        {
                            string urli = Logica_WS_Vgl.Movile_consulta_persona_Recuperar_Metodo(correo);
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
                            string CorreoUrl = Logica_WS_Vgl.Movile_envio_correo_persona_Metodo("1", correo, CodigoPersona);
                            verificationCode = Convert.ToString(random.Next(1000, 9999)); // Genera un número aleatorio de 4 dígitos
                            Console.WriteLine("---------->>>>>>> = " + verificationCode);
                            await pageServicio.DisplayAlert("CÓDIGO", verificationCode, appRecursos.aceptar);
                            await pageServicio.PushAsync(new IngresarCodigo(correo, verificationCode));
                        }
                        catch (Exception exs)
                        {
                            IsBusy = false;
                            await pageServicio.DisplayAlert(appRecursos.oppsAlgoPaso, exs.Message, appRecursos.aceptar);
                        }
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


        private async Task<bool> Validar_Formulario_Mto()
        {

            if (string.IsNullOrWhiteSpace(EntCorreo))
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", appRecursos.noDejesCamposVacios, appRecursos.aceptar);
                return false;
            }

            bool isEmail = Regex.IsMatch(EntCorreo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                IsBusy = false;
                await pageServicio.DisplayAlert("AppToTrip", appRecursos.elFormatoDeCorreoEsIncorrecto, appRecursos.aceptar);
                return false;
            }
            return true;
        }

        /*Fin ingreso Correo*/
    }
}
