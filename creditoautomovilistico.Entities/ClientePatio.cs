using System;

namespace creditoautomovilistico.Entities
{
    public class ClientePatio
    {
        public int Id { get; set; }

        public Cliente Cliente { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public int? PatioId { get; set; }
    }
}
