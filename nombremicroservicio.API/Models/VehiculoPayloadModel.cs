using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.API.Models
{
    public class VehiculoPayloadModel
    {
        [Required]
        public string Placa { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string NroChasis { get; set; }

        public string Tipo { get; set; }

        [Required]
        public int Cilindraje { get; set; }

        [Required]
        public decimal Avaluo { get; set; }

        [Required]
        public int MarcaId { get; set; }
    }
}
