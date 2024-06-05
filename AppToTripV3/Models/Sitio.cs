namespace AppToTripV3.Models
{
    public class Sitio
    {
        public string Id_Sitio { get; set; }
        public string Nombre_Sitio { get; set; }
        public string Descripcion_Sitio { get; set; }
        public string Descripcion_Corta_Sitio { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Recomendacion { get; set; }
        public string Equipamento { get; set; }
        public string Horario { get; set; }
        public string Tiempo_Estimado { get; set; }
        public string Direccion { get; set; }
        public string Ira { get; set; }
        public string Telefono_Sitio { get; set; }
        public string Costo { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Codigo_Lenguaje { get; set; }
        public string Imagen_Sitio { get; set; }
        public List<string> Imagenes_Circuito { get; set; }
        public string Fk_Circuito { get; set; }
        public string Orden { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Estado { get; set; }
        public int Estado_Calificacion { get; set; }
        public int Encuesta { get; set; }
        public string Id_Encuesta { get; set; }
        public string Tipo_Encuesta { get; set; }
        public string Enunciado { get; set; }
        public string Opciones { get; set; }

        readonly string Coordenadas_Map;

        public Sitio(string id_sitio, string nombre_sitio, string descripcion_Sitio, string descripcion_Corta_Sitio, string recomendacion, string equipamento, string horario, string tiempo_estimado, string direccion, string telefono_sitio, string costo, string codigo_lenguaje, List<string> imagenes_Circuito, string orden, int like, int dislike)
        {
            Id_Sitio = id_sitio;
            Nombre_Sitio = nombre_sitio;
            Descripcion_Sitio = descripcion_Sitio;
            Descripcion_Corta_Sitio = descripcion_Corta_Sitio;
            Recomendacion = recomendacion;
            Equipamento = equipamento;
            Horario = horario;
            Tiempo_Estimado = tiempo_estimado;
            Direccion = direccion;
            Telefono_Sitio = telefono_sitio;
            Costo = costo;
            Codigo_Lenguaje = codigo_lenguaje;
            Imagenes_Circuito = imagenes_Circuito;
            Orden = orden;
            Dislike = dislike;
            Like = like;
        }

        public Sitio(string id_sitio, string nombre_sitio, string descripcion_corta_sitio, string latitud, string longitud, string imagen_sitio, int likes, int dislike, string orden)
        {
            Id_Sitio = id_sitio;
            Nombre_Sitio = nombre_sitio;
            Descripcion_Corta_Sitio = descripcion_corta_sitio;
            Latitud = latitud;
            Longitud = longitud;
            Imagen_Sitio = imagen_sitio;
            Like = likes;
            Dislike = dislike;
            Orden = orden;
        }

        public Sitio(string id_sitio, string nombre_sitio, string descripcion_corta_sitio, string latitud, string longitud, string imagen_sitio, string ira, string orden, int estado, int estado_calificacion, int encuesta, string id_encuesta, string tipo_encuesta, string enunciado, string opciones)
        {
            Id_Sitio = id_sitio;
            Nombre_Sitio = nombre_sitio;
            Descripcion_Corta_Sitio = descripcion_corta_sitio;
            Latitud = latitud;
            Longitud = longitud;
            Imagen_Sitio = imagen_sitio;
            Ira = ira;
            Orden = orden;
            Estado = estado;
            Estado_Calificacion = estado_calificacion;
            Encuesta = encuesta;
            Id_Encuesta = id_encuesta;
            Tipo_Encuesta = tipo_encuesta;
            Enunciado = enunciado;
            Opciones = opciones;
        }
        public Sitio(string id_sitio, string nombre_sitio, string descripcion_corta_sitio, string coordenadas, string ira, string orden, int estado)
        {
            Id_Sitio = id_sitio;
            Nombre_Sitio = nombre_sitio;
            Descripcion_Corta_Sitio = descripcion_corta_sitio;
            Coordenadas_Map = coordenadas;
            Ira = ira;
            Orden = orden;
            Estado = estado;
        }
    }
}
