using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class SolicitudCredito
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaElaboracion { get; set; }

        [Required]
        public virtual Cliente? Cliente { get; set; }

        [Required]
        public virtual Patio? Patio { get; set; }

        [Required]
        public virtual Vehiculo? Vehiculo { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MesesPlazo { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Cuotas { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Entrada { get; set; }

        [Required]
        public virtual Ejecutivo? Ejecutivo { get; set; }

        [StringLength(500)]
        public string Observacion { get; set; }

        [Required]
        public int Estado { get; set; }
    }
}
