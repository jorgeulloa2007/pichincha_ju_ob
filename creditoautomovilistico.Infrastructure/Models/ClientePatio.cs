using System;

namespace creditoautomovilistico.Infrastructure.Models
{
    public class ClientePatio
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}