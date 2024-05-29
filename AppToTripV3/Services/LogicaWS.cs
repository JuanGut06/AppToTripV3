
using Plugin.DeviceInfo;
using System.Diagnostics;

namespace AppToTripV3.Services
{
    public class LogicaWS
    {
        private readonly string infopackageName = "cdi.jlge.com.circuitosturisticos";

        /*Enlace ambiente produccion*/
        //public string url_audio = "http://akyvoy.com/produccion_audios/";

        public string url_audio = "https://api.apptotrip.com/produccion_audios/";
        private readonly static string url = "https://api.apptotrip.com/AppToTripMovile/AppToTripMovile.svc";

        /*Enlace ambiente pruebas*/
        //public string url_audio = "http://api.apptotrip.com/produccion_audios/";
        //private readonly static string url = "http://api.apptotrip.com/AppToTripPrbMovile/AppToTripMovile.svc";
        //private static string urlHotel = "http://api.apptotrip.com/AppToTripPrbMovile/AppToTripMovile.svc";

        //private string url_audio_de = "http://35.163.51.145/prueba/";
        //private static final string url = "http://35.163.51.145/AppToTripMovile/AppToTripMovile.svc";

        private readonly string Movile_Respuesta_Encuesta = "Movile_Respuesta_Encuesta"; // No se ha validado url
        private readonly string Movile_Consulta_Circuitos_Macro = "Movile_Consulta_Circuitos_Macro"; // No se ha validado url
        private readonly string Movile_Consulta_Macro_Detalle = "Movile_Consulta_Macro_Detalle"; // No se ha validado url
        private readonly string Movile_Consulta_Macros = "Movile_Consulta_Macros";
        private readonly string Movile_Consulta_Ciudades_Cp = "Movile_Consulta_Ciudades_Cp";
        private readonly string Movile_Consulta_Categoria = "Movile_Consulta_Categoria";
        private readonly string Movile_Insert_Persona = "Movile_Insert_Persona";
        private readonly string Movile_Consulta_Personas = "Movile_Consulta_Personas";
        private readonly string Movile_Consulta_Circuitos = "Movile_Consulta_Circuitos";
        private readonly string Movile_Consulta_Circuitos_Persona = "Movile_Consulta_Circuitos_Persona";
        private readonly string Movile_Consulta_Circuito_Info = "Movile_Consulta_Circuito_Info";
        private readonly string Movile_Consulta_Recorrido_Circuito = "Movile_Consulta_Recorrido_Circuito";
        private readonly string Movile_Consulta_Sitio_Info = "Movile_Consulta_Sitio_Info";
        private readonly string Movile_Consulta_Web = "Movile_Consulta_Web";
        private readonly string Movile_Traducir = "Movile_Traducir";
        private readonly string Movile_Traduccion_Circuitos = "Movile_Traduccion_Circuitos";
        private readonly string Movile_Traduccion_Sitios = "Movile_Traduccion_Sitios";
        private readonly string Movile_Comprar_Circuito = "Movile_Comprar_Circuito";
        private readonly string Movile_Asignar_Circuito = "Movile_Asignar_Circuito";
        private readonly string Movile_Finalizar_Circuito = "Movile_Finalizar_Circuito";
        private readonly string Movile_Finalizar_Sitio = "Movile_Finalizar_Sitio";
        private readonly string Movile_Calificacion_Circuito = "Movile_Calificacion_Circuito";
        private readonly string Movile_Calificacion_Sitio = "Movile_Calificacion_Sitio";
        private readonly string Movile_Calificacion_Usuario = "Movile_Calificacion_Usuario";
        private readonly string Movile_Registro_FCM = "Movile_Registro_FCM";
        private readonly string Movile_Notificacion = "Movile_Notificacion";
        private readonly string Movile_Actualiza_persona = "Movile_Actualiza_persona";
        private readonly string Movile_envio_correo_persona = "Movile_envio_correo_persona";
        private readonly string Movile_consulta_persona_Recuperar = "Movile_consulta_persona_Recuperar";
        private readonly string Movile_Crear_Circuitos = "Movile_Crear_Circuitos";
        private readonly string Movile_Traducir_Campos_Circuitos = "Movile_Traducir_Campos_Circuitos";
        private readonly string Movile_Generar_Audios_Circuitos = "Movile_Generar_Audios_Circuitos";
        private readonly string Movile_Generar_Nombre_Sitios = "Movile_Generar_Nombre_Sitios";
        private readonly string Movile_Generar_Info_Sitios = "Movile_Generar_Info_Sitios";
        private readonly string Movile_Traducir_Campos_Sitio = "Movile_Traducir_Campos_Sitio";
        private readonly string Movile_Generar_Audios_Sitios = "Movile_Generar_Audios_Sitios";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_Destino = "Movile_Traducir_Campos_Circuitos_Idioma_Destino";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_Destino = "Movile_Traducir_Campos_Sitio_Idioma_Destino";


        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_FR = "Movile_Traducir_Campos_Circuitos_Idioma_FR";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_EN = "Movile_Traducir_Campos_Circuitos_Idioma_EN";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_IT = "Movile_Traducir_Campos_Circuitos_Idioma_IT";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_JA = "Movile_Traducir_Campos_Circuitos_Idioma_JA";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_PT = "Movile_Traducir_Campos_Circuitos_Idioma_PT";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_RU = "Movile_Traducir_Campos_Circuitos_Idioma_RU";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_TR = "Movile_Traducir_Campos_Circuitos_Idioma_TR";
        private readonly string Movile_Traducir_Campos_Circuitos_Idioma_DE = "Movile_Traducir_Campos_Circuitos_Idioma_DE";

        private readonly string Movile_Traducir_Campos_Sitio_Idioma_FR = "Movile_Traducir_Campos_Sitio_Idioma_FR";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_EN = "Movile_Traducir_Campos_Sitio_Idioma_EN";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_IT = "Movile_Traducir_Campos_Sitio_Idioma_IT";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_JA = "Movile_Traducir_Campos_Sitio_Idioma_JA";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_PT = "Movile_Traducir_Campos_Sitio_Idioma_PT";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_RU = "Movile_Traducir_Campos_Sitio_Idioma_RU";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_TR = "Movile_Traducir_Campos_Sitio_Idioma_TR";
        private readonly string Movile_Traducir_Campos_Sitio_Idioma_DE = "Movile_Traducir_Campos_Sitio_Idioma_DE";

        private readonly string Movile_valida_Idioma = "Movile_valida_Idioma";
        private readonly string Movile_valida_Idioma_Sitio = "Movile_valida_Idioma_Sitio";



        public string Movile_Respuesta_Encuesta_Metodo(string id_encuesta, string respuesta)
        {
            return url + "/" + Movile_Respuesta_Encuesta + "/" +
                    id_encuesta + "/" +
                    ObtenerToken() + "/" +
                    respuesta + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Circuitos_Macro_Metodo(string lang, string ids)
        {
            return url + "/" + Movile_Consulta_Circuitos_Macro + "/" +
                    lang + "/" +
                    ObtenerToken() + "/" +
                    ids + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Macro_Detalle_Metodo(string nombre_macro, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Macro_Detalle + "/" +
                    nombre_macro + "/" +
                    codigo_idioma + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Macros_Metodo()
        {
            return url + "/" + Movile_Consulta_Macros + "/" + infopackageName;
        }


        public string Movile_Consulta_Ciudades_Cp_Metodo()
        {
            return url + "/" +
                Movile_Consulta_Ciudades_Cp + "/" +
                ObtenerToken() + "/" +
                infopackageName;
        }

        public string Movile_Consulta_Categoria_Metodo(string codigo_idioma)
        {
            return url + "/" +
                Movile_Consulta_Categoria + "/" +
                codigo_idioma + "/" +
                infopackageName;
        }

        public string Movile_Insert_Persona_Metodo(string nombres_persona, string apellidos_persona, string correo_persona, string celular_persona, string token_persona, string fecha_nacimiento)
        {
            return url + "/" + Movile_Insert_Persona + "/" +
                nombres_persona + "/" +
                apellidos_persona + "/" +
                correo_persona + "/" +
                celular_persona + "/" +
                token_persona + "/" +
                fecha_nacimiento + "/" +
                infopackageName;
        }

        public string Movile_Consulta_Personas_Metodo(string correo_persona, string token_persona)
        {
            return url + "/" +
                Movile_Consulta_Personas + "/" +
                correo_persona + "/" +
                token_persona + "/" +
                infopackageName;
        }

        public string Movile_Consulta_Circuitos_Metodo(string ciudad, string codigo_idioma, string pais)
        {
            return url + "/" + Movile_Consulta_Circuitos + "/" +
                    ciudad + "/" +
                    codigo_idioma + "/" +
                    pais + "/" +
                    ObtenerToken() + "/" +
                    infopackageName;
        }
        public string Movile_Consulta_Hotel_Metodo(string ciudad, string codigo_idioma, string pais)
        {
            return url + "/" + Movile_Consulta_Circuitos + "/" +
                    ciudad + "/" +
                    codigo_idioma + "/" +
                    pais + "/" +
                    "Hotel" + "/" +
                    infopackageName;
        }


        public string Movile_Consulta_Hotel_Info_Metodo(string id_circuito, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Circuito_Info + "/" +
                    id_circuito + "/" +
                    codigo_idioma + "/" +
                    "Hotel" + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Circuitos_Persona_Metodo(string id_circuito, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Circuitos_Persona + "/" +
                    ObtenerToken() + "/" +
                    id_circuito + "/" +
                    codigo_idioma + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Circuito_Info_Metodo(string id_circuito, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Circuito_Info + "/" +
                    id_circuito + "/" +
                    codigo_idioma + "/" +
                    ObtenerToken() + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Recorrido_Circuito_Metodo(string fk_circuito, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Recorrido_Circuito + "/" +
                    fk_circuito + "/" +
                    codigo_idioma + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Sitio_Info_Metodo(string id_sitio, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Sitio_Info + "/" +
                    id_sitio + "/" +
                    codigo_idioma + "/" +
                    ObtenerToken() + "/" +
                    infopackageName;
        }

        public string Movile_Consulta_Web_Metodo(string id_sitio, string codigo_idioma)
        {
            return url + "/" + Movile_Consulta_Web + "/" +
                id_sitio + "/" +
                codigo_idioma + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Metodo(string texto, string entrada, string salida)
        {
            return url + "/" + Movile_Traducir + "/" +
                texto + "/" +
                entrada + "/" +
                salida + "/" +
                infopackageName;
        }

        public string Movile_Traduccion_Circuitos_Metodo(string id_circuito, string codigo_idioma)
        {
            return url + "/" + Movile_Traduccion_Circuitos + "/" +
                id_circuito + "/" +
                codigo_idioma + "/" +
                infopackageName;
        }

        public string Movile_Traduccion_Sitios_Metodo(string id_sitio, string codigo_idioma)
        {
            return url + "/" + Movile_Traduccion_Sitios + "/" +
                id_sitio + "/" +
                codigo_idioma + "/" +
                infopackageName;
        }

        public string Movile_Comprar_Circuito_Metodo(string id_circuito)
        {
            return url + "/" + Movile_Comprar_Circuito + "/" +
                    ObtenerToken() + "/" +
                    id_circuito + "/" +
                infopackageName;
        }

        public string Movile_Asignar_Circuito_Metodo(string id_circuito)
        {
            return url + "/" + Movile_Asignar_Circuito + "/" +
                    ObtenerToken() + "/" +
                    id_circuito + "/" +
                    infopackageName;
        }

        public string Movile_Finalizar_Circuito_Metodo(string id_circuito)
        {
            return url + "/" + Movile_Finalizar_Circuito + "/" +
                    ObtenerToken() + "/" +
                    id_circuito + "/" +
                    infopackageName;
        }

        public string Movile_Finalizar_Sitio_Metodo(string id_sitio, string estado)
        {
            return url + "/" + Movile_Finalizar_Sitio + "/" +
                    ObtenerToken() + "|" + estado + "/" +
                    id_sitio + "/" +
                    infopackageName;
        }

        public string Movile_Calificacion_Circuito_Metodo(string token_persona, string fk_circuito, string calificacion_circuito, string comentario_circuito)
        {
            return url + "/" + Movile_Calificacion_Circuito + "/" +
                    token_persona + "/" +
                    fk_circuito + "/" +
                    calificacion_circuito + "/" +
                    comentario_circuito + "/" +
                    infopackageName;
        }

        public string Movile_Calificacion_Sitio_Metodo(string token_persona, string fk_sitio, string valor, string comentario)
        {
            return url + "/" + Movile_Calificacion_Sitio + "/" +
                    token_persona + "/" +
                    fk_sitio + "/" +
                    valor + "/" +
                    comentario + "/" +
                    infopackageName;
        }

        public string Movile_Calificacion_Usuario_Metodo(string fk_persona, string fk_usuario, string calificacion_usuario, string comentario_usuario)
        {
            return url + "/" + Movile_Calificacion_Usuario + "/" +
                    fk_persona + "/" +
                    fk_usuario + "/" +
                    calificacion_usuario + "/" +
                    comentario_usuario + "/" +
                    infopackageName;
        }

        public string Movile_Audio_Metodo(string tabla, string id, string campo, string lenguaje)
        {
            return url_audio + tabla + "_" +
                    id + "_" +
                    campo + "_" +
                    lenguaje + ".mp3";
        }

        public string Movile_Registro_FCM_Metodo(string token_fcm)
        {
            return Uri.EscapeDataString(url + "/" + Movile_Registro_FCM + "/" +
                    ObtenerToken() + "/" +
                    token_fcm + "/" +
                    infopackageName);
        }

        public string Movile_Notificacion_Metodo(string codigo_idioma)
        {
            return Uri.EscapeDataString(url + "/" + Movile_Notificacion + "/" +
                    codigo_idioma + "/" +
                    infopackageName);
        }

        public string Movile_Actualiza_persona_Metodo(string correo_persona, string token_persona)
        {
            return url + "/" +
                Movile_Actualiza_persona + "/" +
                correo_persona + "/" +
                token_persona + "/" +
                infopackageName;
        }

        public string Movile_envio_correo_persona_Metodo(string Bandera, string correo_persona, string Codigo)
        {
            return url + "/" +
                Movile_envio_correo_persona + "/" +
                Bandera + "/" +
                correo_persona + "/" +
                Codigo + "/" +
                infopackageName;
        }

        public string Movile_consulta_persona_Recuperar_Metodo(string correo_persona)
        {
            return url + "/" +
                Movile_consulta_persona_Recuperar + "/" +
                correo_persona + "/" +
                infopackageName;
        }

        public string Movile_Crear_Circuitos_Metodo(string nameCity, string nameCountry, string ParEntrada)
        {
            return url + "/" +
                Movile_Crear_Circuitos + "/" +
                nameCity + "/" +
                nameCountry + "/" +
                ParEntrada + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Circuitos_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos + "/" +
                IdCircuito + "/" +
                infopackageName;
        }

        public string Movile_Generar_Audios_Circuitos_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Generar_Audios_Circuitos + "/" +
                IdCircuito + "/" +
                infopackageName;
        }

        public string Movile_Generar_Nombre_Sitios_Metodo(string nombreSitio, string IdCircuito, string NumeroDias)
        {
            return url + "/" +
                Movile_Generar_Nombre_Sitios + "/" +
                nombreSitio + "/" +
                IdCircuito + "/" +
                NumeroDias + "/" +
                infopackageName;
        }

        public string Movile_Generar_Info_Sitios_Metodo(string sitio, string nombreciudad, string idSitio, string idCircuito)
        {
            return url + "/" +
                Movile_Generar_Info_Sitios + "/" +
                sitio + "/" +
                nombreciudad + "/" +
                idSitio + "/" +
                idCircuito + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Metodo(string IdSitio, string idCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio + "/" +
                IdSitio + "/" +
                idCircuito + "/" +
                infopackageName;
        }

        public string Movile_Generar_Audios_Sitios_Metodo(string idSitio, string idCircuito)
        {
            return url + "/" +
                Movile_Generar_Audios_Sitios + "/" +
                idSitio + "/" +
                idCircuito + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Circuitos_Idioma_Destino_Metodo(string IdCircuito, string codIdioma)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_Destino + "/" +
                IdCircuito + "/" +
                codIdioma + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_Destino_Metodo(string IdSitio, string idCircuito, string codIdioma)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_Destino + "/" +
                IdSitio + "/" +
                idCircuito + "/" +
                codIdioma + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_FR_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_FR + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_EN_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_EN + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_IT_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_IT + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_JA_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_JA + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_PT_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_PT + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_RU_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_RU + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_TR_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_TR + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Sitio_Idioma_DE_Metodo(string IdSitio)
        {
            return url + "/" +
                Movile_Traducir_Campos_Sitio_Idioma_DE + "/" +
                IdSitio + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Circuitos_Idioma_FR_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_FR + "/" +
                IdCircuito + "/" +
                infopackageName;
        }

        public string Movile_Traducir_Campos_Circuitos_Idioma_EN_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_EN + "/" +
                IdCircuito + "/" +
                infopackageName;
        }
        public string Movile_Traducir_Campos_Circuitos_Idioma_IT_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_IT + "/" +
                IdCircuito + "/" +
                infopackageName;
        }
        public string Movile_Traducir_Campos_Circuitos_Idioma_JA_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_JA + "/" +
                IdCircuito + "/" +
                infopackageName;
        }
        public string Movile_Traducir_Campos_Circuitos_Idioma_PT_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_PT + "/" +
                IdCircuito + "/" +
                infopackageName;
        }
        public string Movile_Traducir_Campos_Circuitos_Idioma_RU_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_RU + "/" +
                IdCircuito + "/" +
                infopackageName;
        }
        public string Movile_Traducir_Campos_Circuitos_Idioma_TR_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_TR + "/" +
                IdCircuito + "/" +
                infopackageName;
        }
        public string Movile_Traducir_Campos_Circuitos_Idioma_DE_Metodo(string IdCircuito)
        {
            return url + "/" +
                Movile_Traducir_Campos_Circuitos_Idioma_DE + "/" +
                IdCircuito + "/" +
                infopackageName;
        }

        public string Movile_valida_Idioma_Metodo(string idCircuito, string CodIdioma)
        {
            return url + "/" +
                Movile_valida_Idioma + "/" +
                idCircuito + "/" +
                CodIdioma + "/" +
                infopackageName;
        }

        public string Movile_valida_Idioma_sitio_Metodo(string IdSitio, string CodIdioma)
        {
            return url + "/" +
                Movile_valida_Idioma_Sitio + "/" +
                IdSitio + "/" +
                CodIdioma + "/" +
                infopackageName;
        }
        // Se hace la conexion con el ws
        public async Task<string> Conection(string urlo)
        {
            try
            {
                using (var cliente = new HttpClient())
                {
                    var peticion = await cliente.GetAsync(urlo); // Se realiza la peticion

                    if (peticion != null)
                    {
                        string aux;
                        aux = peticion.Content.ReadAsStringAsync().Result.Replace("'", "|comillasReplace|");
                        aux = aux.Replace("\\\"", "'").Replace("\"", "");
                        aux = aux.Replace("'", "\"");
                        return aux.Replace("|comillasReplace|", "'");
                    }

                    if (peticion != null)
                        return peticion.Content.ReadAsStringAsync().Result.Replace("\\\"", "'").Replace("\"", "");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
            }
            return default;
        }

        //Metodo obtiene le IMEI del usuario y se pasa como token de usuario
        public string ObtenerToken()
        {
            //string correoPersona = Preferences.Get("correo_persona", "");

            //if (!string.IsNullOrEmpty(correoPersona) && correoPersona.ToLower() == "juan.16gut@gmail.com")
            //{
            //    return "dc9fd13c5fe07359";
            //}
            //else
            //{
            //    return CrossDeviceInfo.Current.Id;
            //}
            return CrossDeviceInfo.Current.Id;

        }
        public string GetJsonFilePath(string nombreDocumento)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(documentsPath, nombreDocumento + ".json");

        }

        public async Task<string> DescargarImagen(string urlImagen, string nombreArchivo)
        {
            try
            {
                // Descargar la imagen utilizando HttpClient
                using (var httpClient = new HttpClient())
                {
                    var bytes = await httpClient.GetByteArrayAsync(urlImagen);

                    // Obtener la ruta del directorio de documentos
                    var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                    // Combinar la ruta del directorio de documentos con el nombre de archivo
                    var filePath = Path.Combine(documentsPath, nombreArchivo);

                    // Guardar los bytes de la imagen en un archivo local
                    File.WriteAllBytes(filePath, bytes);

                    // Devolver la ruta del archivo guardado
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al descargar imagen: {ex.Message}");
                return null;
            }
        }
    }
}