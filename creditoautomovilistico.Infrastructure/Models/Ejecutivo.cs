using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class Ejecutivo: Persona
    {
        [Phone]
        public string Celular { get; set; }

        [Required]
        public int PatioId { get; set; }
    }
}
