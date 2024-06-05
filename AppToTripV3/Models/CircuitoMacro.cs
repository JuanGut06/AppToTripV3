namespace AppToTripV3.Models
{
    public class CircuitoMacro
    {
        public string id_circuito_macro { get; set; }
        public string id_circuito { get; set; }
        public string nombre_circuito { get; set; }
        public string descripcion_circuito { get; set; }
        public string imagen { get; set; }
        public string tiempo_estimado { get; set; }
        public string cantidad { get; set; }

        public CircuitoMacro(string Id_circuito_macro, string Id_circuito, string Nombre_circuito,
            string Descripcion_circuito, string Imagen, string Tiempo_estimado, string Cantidad)
        {
            id_circuito_macro = Id_circuito_macro;
            id_circuito = Id_circuito;
            nombre_circuito = Nombre_circuito;
            descripcion_circuito = Descripcion_circuito;
            imagen = Imagen;
            tiempo_estimado = Tiempo_estimado;
            cantidad = Cantidad;
        }

    }


}
