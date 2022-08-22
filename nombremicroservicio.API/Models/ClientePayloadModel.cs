using creditoautomovilistico.Entities.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.API.Models
{
    public class ClientePayloadModel
    {
        [Required]
        public string Identificacion { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public TipoEstadoCivil EstadoCivil { get; set; }

        [Required]
        public string IdentificacionConyuge { get; set; }

        [Required]
        public string NombreConyuge { get; set; }

        [Required]
        public bool SujetoDeCredito { get; set; }
    }
}