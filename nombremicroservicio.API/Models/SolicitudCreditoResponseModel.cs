using System;

namespace creditoautomovilistico.API.Models
{
    public class SolicitudCreditoResponseModel
    {
        public int Id { get; set; }

        public DateTime FechaElaboracion { get; set; }

        public ClienteResponseModel Cliente { get; set; }

        public string IdPatio { get; set; }

        public VehiculoResponseModel Vehiculo { get; set; }

        public int MesesPlazo { get; set; }

        public decimal Cuotas { get; set; }

        public decimal Entrada { get; set; }

        public EjecutivoResponseModel Ejecutivo { get; set; }

        public string Observacion { get; set; }

        public int Estado { get; set; }
    }
}