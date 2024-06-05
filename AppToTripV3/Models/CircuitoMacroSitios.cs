namespace AppToTripV3.Models
{
    public class CircuitoMacroSitios
    {
        public string id_circuito_macro { get; set; }
        public string fk_ciudad { get; set; }
        public string ciudad { get; set; }
        public string id_circuito { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string orden_macro { get; set; }

        public CircuitoMacroSitios(string Id_circuito_macro, string Fk_ciudad, string Ciudad,
        string Id_circuito, string Latitud, string Longitud, string Orden_macro)
        {
            id_circuito_macro = Id_circuito_macro;
            fk_ciudad = Fk_ciudad;
            ciudad = Ciudad;
            id_circuito = Id_circuito;
            latitud = Latitud;
            longitud = Longitud;
            orden_macro = Orden_macro;
        }
    }

}
