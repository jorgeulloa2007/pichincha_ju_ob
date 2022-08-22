using System.Collections.Generic;
using creditoautomovilistico.Entities.Enumerations;

namespace creditoautomovilistico.Entities
{
    public class Cliente:Persona
    {
        public TipoEstadoCivil EstadoCivil { get; set; }
        public string IdentificacionConyuge { get; set; }
        public string NombreConyuge { get; set; }
        public bool SujetoDeCredito { get; set; }
        public List<SolicitudCredito> Solicitudes { get; set; }
    }
}
