using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoautomovilistico.Entities
{
    public class Patio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int NumeroPuntoVenta { get; set; }

        public List<Ejecutivo> Ejecutivos { get; set; }

        public List<ClientePatio> Clientes { get; set; }
    }
}
