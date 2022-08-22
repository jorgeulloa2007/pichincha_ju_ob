using creditoautomovilistico.Entities.Enumerations;
using System;

namespace creditoautomovilistico.Entities
{
    public class SolicitudCredito
    {
        public int Id { get; set; }

        public DateTime FechaElaboracion { get; set; }

        public Cliente Cliente { get; set; }

        public Patio Patio { get; set; }

        public Vehiculo Vehiculo { get; set; }

        public int MesesPlazo { get; set; }

        public decimal Cuotas { get; set; }

        public decimal Entrada { get; set; }

        public Ejecutivo Ejecutivo { get; set; }

        public string Observacion { get; set; }

        public TipoEstadoSolicitud Estado { get; set; }
    }
}
