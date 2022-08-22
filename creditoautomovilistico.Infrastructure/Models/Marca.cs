using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class Marca
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MarcaAuto  { get; set; }
    }
}
