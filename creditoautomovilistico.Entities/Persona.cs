﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoautomovilistico.Entities
{
    public class Persona
    {
        public int Id { get; set; }

        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int Edad { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
    }
}