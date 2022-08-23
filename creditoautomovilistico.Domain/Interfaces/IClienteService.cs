using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IClienteService
    {
        /// <summary>
        /// Elimina cliente de la BD
        /// </summary>
        /// <param name="identificacion">id del cliente</param>
        /// <returns></returns>
        Task<bool> DeleteCliente(string identificacion);

        /// <summary>
        /// Agrega nuevo cliente a la BD
        /// </summary>
        /// <param name="cliente">objeto con datos del cliente</param>
        /// <returns></returns>
        Task<Cliente> PostCliente(Cliente cliente);

        /// <summary>
        /// Edita datos del cliente
        /// </summary>
        /// <param name="cliente">objeto con datos del cliente</param>
        /// <returns></returns>
        Task<Cliente> PutCliente(Cliente cliente);

        /// <summary>
        /// Obtiene datos del cliente basado en su id
        /// </summary>
        /// <param name="identificacion">id del cliente</param>
        /// <returns></returns>
        Task<Cliente> GetCliente(string identificacion);

        /// <summary>
        /// Genera solicitud de crédito
        /// </summary>
        /// <param name="solicitud">datos de la solicitud</param>
        /// <returns></returns>
        Task<SolicitudCredito> GenerateSolicitudCredito(SolicitudCredito solicitudCredito);
    }
}
