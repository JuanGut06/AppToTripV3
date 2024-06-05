namespace AppToTripV3.Models
{
    public class Circuito
    {
        public string id_circuito { get; set; }
        public string nombre_circuito { get; set; }
        public string descripcion_corta_circuito { get; set; }
        public string descripcion_circuito { get; set; }
        public string imagen_circuito { get; set; }
        public List<string> imagenes_circuito { get; set; }
        public string contexto_circuito { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        public string categoria { get; set; }
        public string edad { get; set; }
        public string tiempo_estimado { get; set; }
        public string equipamento { get; set; }
        public string recomendacion { get; set; }
        public string codigo_lenguaje { get; set; }
        public string cantidad_sitio { get; set; }
        public string promedio_circuito { get; set; }
        public string autor { get; set; }
        public string foto_autor { get; set; }
        public string promedio_autor { get; set; }
        public string precio { get; set; }
        public string compra { get; set; }
        public string estado_calificacion { get; set; }


        public Circuito(string Id_Circuito, string Nombre_Circuito, string Descripcion_Corta_Circuito, string Descripcion_Circuito,
                  string Latitud, string Longitud, string Ciudad, string Pais, string Imagen_Circuito,
                  string Categoria, string Tiempo_Estimado, string Codigo_Lenguaje, string Cantidad_Sitio,
                  string Promedio_Circuito, string Precio, string Compra)
        {
            id_circuito = Id_Circuito;
            nombre_circuito = Nombre_Circuito;
            descripcion_corta_circuito = Descripcion_Corta_Circuito;
            descripcion_circuito = Descripcion_Circuito;
            latitud = Latitud;
            longitud = Longitud;
            ciudad = Ciudad;
            pais = Pais;
            imagen_circuito = Imagen_Circuito;
            categoria = Categoria;
            tiempo_estimado = Tiempo_Estimado;
            codigo_lenguaje = Codigo_Lenguaje;
            cantidad_sitio = Cantidad_Sitio;
            promedio_circuito = Promedio_Circuito;
            precio = Precio;
            compra = Compra;
        }


        public Circuito(string Id_Circuito, string Nombre_Circuito, string Contexto_Circuito, string Estado_Calificacion)
        {
            id_circuito = Id_Circuito;
            nombre_circuito = Nombre_Circuito;
            contexto_circuito = Contexto_Circuito;
            estado_calificacion = Estado_Calificacion;
        }

        public Circuito(string Id_Circuito, string Nombre_Circuito, string Descripcion_Circuito, string Imagen_Circuito, string Cantidad_Sitio)
        {
            id_circuito = Id_Circuito;
            nombre_circuito = Nombre_Circuito;
            descripcion_circuito = Descripcion_Circuito;
            imagen_circuito = Imagen_Circuito;
            cantidad_sitio = Cantidad_Sitio;
        }

    }
}