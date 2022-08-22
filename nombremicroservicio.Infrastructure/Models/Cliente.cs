using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class Cliente: Persona
    {
        [Required]
        public int EstadoCivil { get; set; }

        [StringLength(10)]
        public string IdentificacionConyuge { get; set; }

        [StringLength(100)]
        public string NombreConyuge { get; set; }

        [Required]
        public bool SujetoDeCredito { get; set; }

        public List<SolicitudCredito> Solicitudes { get; set; }
    }
}
