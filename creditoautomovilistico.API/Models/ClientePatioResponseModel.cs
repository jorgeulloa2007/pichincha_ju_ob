using System;

namespace creditoautomovilistico.API.Models
{
    public class ClientePatioResponseModel
    {
        public int Id { get; set; }

        public ClienteResponseModel Cliente { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public int? PatioId { get; set; }
    }
}