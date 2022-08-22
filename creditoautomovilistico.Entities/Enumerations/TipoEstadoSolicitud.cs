using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditoautomovilistico.Entities.Enumerations
{
    public enum TipoEstadoSolicitud
    {
        [Description("Registrada")]
        Registrada = 1,
        [Description("Despachada")]
        Despachada = 2,
        [Description("Cancelada")]
        Cancelada = 3
    }
}
