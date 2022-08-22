using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class Patio
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        public int NumeroPuntoVenta { get; set; }

        public List<Ejecutivo> Ejecutivos { get; set; }

        public List<ClientePatio> Clientes { get; set; }
    }
}
