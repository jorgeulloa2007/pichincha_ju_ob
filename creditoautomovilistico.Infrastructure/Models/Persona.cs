using System;
using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Edad { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }
    }
}
