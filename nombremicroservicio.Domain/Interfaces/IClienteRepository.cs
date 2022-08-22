using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace nombremicroservicio.Domain.Interfaces
{
    public interface IClienteRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        Task<Cliente> GetClienteByIdentificacion(string identificacion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> AddCliente(Cliente cliente);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> EditCliente(Cliente cliente);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        Task<bool> RemoveCliente(string identificacion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>
        Task<bool> HaveSolicitudesAsociadas(string identificacion);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        Task<SolicitudCredito> GenerarSolicitud(SolicitudCredito solicitud);
    }
}
