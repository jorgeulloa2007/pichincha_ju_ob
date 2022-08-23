using System;
using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class ClientePatio
    {
        public int Id { get; set; }

        [Required]
        public Cliente Cliente { get; set; }

        [Required]
        public DateTime FechaAsignacion { get; set; }

        [Required]
        public int? PatioId { get; set; }
    }
}