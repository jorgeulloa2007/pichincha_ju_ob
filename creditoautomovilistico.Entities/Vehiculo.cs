using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoautomovilistico.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Placa { get; set; }

        public string Modelo { get; set; }

        public string NroChasis { get; set; }

        public string Tipo { get; set; }

        public int Cilindraje { get; set; }

        public decimal Avaluo { get; set; }

        public string MarcaAuto { get; set; }

        public int Year { get; set; }
    }
}
