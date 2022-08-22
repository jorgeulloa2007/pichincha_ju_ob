using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Placa { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(10)]
        public string NroChasis { get; set; }

        [StringLength(10)]
        public string Tipo { get; set; }

        [Required]
        public int Cilindraje { get; set; }

        [Required]
        public decimal Avaluo { get; set; }

        [Required]
        public int MarcaId { get; set; }
    }
}
