using System.Collections.Generic;

namespace creditoautomovilistico.API.Models
{
    public class PatioResponseModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public int NumeroPuntoVenta { get; set; }

        public List<EjecutivoResponseModel> Ejecutivos { get; set; }

        public List<ClientePatioResponseModel> Clientes { get; set; }
    }
}