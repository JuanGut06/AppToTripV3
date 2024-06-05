namespace AppToTripV3.Models
{
    public class Hotel
    {
        public string Id_Hotel { get; set; }
        public string Nombre_Hotel { get; set; }
        public string Descripcion_Hotel { get; set; }
        public string Descripcion_Corta_Hotel { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Indicacion { get; set; }
        public string Rnt { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Sitio_Web { get; set; }
        public string Idioma { get; set; }
        public string Encargado { get; set; }
        public string Correo_Hotel { get; set; }
        public string Web_Whatsapp { get; set; }

        public Hotel(string id_Hotel, string nombre_Hotel, string descripcion_Hotel, string descripcion_Corta_Hotel, string ciudad, string pais, string indicacion, string rnt, string direccion, string celular, string sitio_Web, string idioma, string encargado, string correo_Hotel, string web_Whatsapp)
        {
            Id_Hotel = id_Hotel;
            Nombre_Hotel = nombre_Hotel;
            Descripcion_Hotel = descripcion_Hotel;
            Descripcion_Corta_Hotel = descripcion_Corta_Hotel;
            Ciudad = ciudad;
            Pais = pais;
            Indicacion = indicacion;
            Rnt = rnt;
            Direccion = direccion;
            Celular = celular;
            Sitio_Web = sitio_Web;
            Idioma = idioma;
            Encargado = encargado;
            Correo_Hotel = correo_Hotel;
            Web_Whatsapp = web_Whatsapp;
        }
    }
}