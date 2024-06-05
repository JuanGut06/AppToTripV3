namespace AppToTripV3.Models
{
    public class Habitacion
    {
        public string Id_Habitacion { get; set; }
        public string Nombre_Habitacion { get; set; }
        public string Descripcion_Habitacion { get; set; }
        public string Descripcion_Corta_Habitacion { get; set; }
        public string Capacidad { get; set; }
        public string Costo { get; set; }
        public string Tamaño { get; set; }
        public string CamaDoble { get; set; }
        public string CamaSencilla { get; set; }
        public string SofaCama { get; set; }
        public string Indicacion { get; set; }
        public string Imagenes_Habitacion { get; set; }

        public Habitacion(string id_Habitacion, string nombre_Habitacion, string descripcion_Habitacion, string descripcion_Corta_Habitacion, string capacidad, string costo, string tamaño, string camaDoble, string camaSencilla, string sofaCama, string indicacion, string imagenes_Habitacion)
        {
            Id_Habitacion = id_Habitacion;
            Nombre_Habitacion = nombre_Habitacion;
            Descripcion_Habitacion = descripcion_Habitacion;
            Descripcion_Corta_Habitacion = descripcion_Corta_Habitacion;
            Capacidad = capacidad;
            Costo = costo;
            Tamaño = tamaño;
            CamaDoble = camaDoble;
            CamaSencilla = camaSencilla;
            SofaCama = sofaCama;
            Indicacion = indicacion;
            Imagenes_Habitacion = imagenes_Habitacion;
        }
    }
}