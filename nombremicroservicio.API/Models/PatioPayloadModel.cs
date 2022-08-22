using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.API.Models
{
    public class PatioPayloadModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public int NumeroPuntoVenta { get; set; }
    }
}
