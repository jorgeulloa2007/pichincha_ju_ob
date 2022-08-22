namespace creditoautomovilistico.API.Models
{
    public class PersonaResponseModel
    {
        public int Id { get; set; }

        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int Edad { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
    }
}