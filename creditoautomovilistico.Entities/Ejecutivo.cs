using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoautomovilistico.Entities
{
    public class Ejecutivo: Persona
    {
        public string Celular { get; set; }

        public int PatioId { get; set; }
    }
}
