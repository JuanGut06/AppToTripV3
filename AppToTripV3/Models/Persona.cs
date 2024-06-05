namespace AppToTripV3.Models
{
    public class Persona
    {
        public string nombres_persona { get; set; }
        public string apellidos_persona { get; set; }
        public string id_persona { get; set; }

        public Persona(string Nombres_persona, string Apellidos_persona, string Id_persona)
        {
            nombres_persona = Nombres_persona;
            apellidos_persona = Apellidos_persona;
            id_persona = Id_persona;
        }
    }

}
