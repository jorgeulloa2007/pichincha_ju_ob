namespace creditoautomovilistico.API.Models
{
    public class VehiculoResponseModel
    {
        public int Id { get; set; }

        public string Placa { get; set; }

        public string Modelo { get; set; }

        public string NroChasis { get; set; }

        public string Tipo { get; set; }

        public int Cilindraje { get; set; }

        public decimal Avaluo { get; set; }

        public int MarcaId { get; set; }
    }
}
