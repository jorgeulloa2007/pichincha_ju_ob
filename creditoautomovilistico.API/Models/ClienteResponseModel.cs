using creditoautomovilistico.Entities.Enumerations;

namespace creditoautomovilistico.API.Models
{
    public class ClienteResponseModel: PersonaResponseModel
    {
        public TipoEstadoCivil EstadoCivil { get; set; }
        public string IdentificacionConyuge { get; set; }
        public string NombreConyuge { get; set; }
        public bool SujetoDeCredito { get; set; }
    }
}