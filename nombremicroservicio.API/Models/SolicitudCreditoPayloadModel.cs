using creditoautomovilistico.Entities.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace creditoautomovilistico.API.Models
{
    public class SolicitudCreditoPayloadModel
    {
        [Required]
        public string IdCliente { get; set; }

        [Required]
        public DateTime FechaElaboracion { get; set; }

        [Required]
        public string Patio { get; set; }

        [Required]
        public string PlacaVehiculo { get; set; }

        [Required]
        public int MesesPlazo { get; set; }

        [Required]
        public decimal Cuotas { get; set; }

        [Required]
        public decimal Entrada { get; set; }

        [Required]
        public string IdEjecutivo { get; set; }

        public string Observacion { get; set; }

        [Required]
        public TipoEstadoSolicitud Estado { get; set; }
    }
}
