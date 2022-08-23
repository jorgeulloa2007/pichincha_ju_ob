using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces
{
    public interface IClienteRepository
    {
        /// <summary>
        /// Obtiene datos del cliente basado en su id
        /// </summary>
        /// <param name="identificacion">id del cliente</param>
        /// <returns></returns>
        Task<Cliente> GetClienteByIdentificacion(string identificacion);

        /// <summary>
        /// Agrega nuevo cliente a la BD
        /// </summary>
        /// <param name="cliente">objeto con datos del cliente</param>
        /// <returns></returns>
        Task<Cliente> AddCliente(Cliente cliente);

        /// <summary>
        /// Edita datos del cliente
        /// </summary>
        /// <param name="cliente">objeto con datos del cliente</param>
        /// <returns></returns>
        Task<Cliente> EditCliente(Cliente cliente);

        /// <summary>
        /// Elimina cliente de la BD
        /// </summary>
        /// <param name="identificacion">id del cliente</param>
        /// <returns></returns>
        Task<bool> RemoveCliente(string identificacion);

        /// <summary>
        /// Determina si el cliente tiene solicitudes de crédito asociadas
        /// </summary>
        /// <param name="identificacion">id del cliente</param>
        /// <returns></returns>
        Task<bool> HaveSolicitudesAsociadas(string identificacion);


        /// <summary>
        /// Genera solicitud de crédito
        /// </summary>
        /// <param name="solicitud">datos de la solicitud</param>
        /// <returns></returns>
        Task<SolicitudCredito> GenerarSolicitud(SolicitudCredito solicitud);
    }
}
