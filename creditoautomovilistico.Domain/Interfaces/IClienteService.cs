using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<bool> DeleteCliente(string identificacion);
        Task<Cliente> PostCliente(Cliente cliente);
        Task<Cliente> PutCliente(Cliente cliente);
        Task<Cliente> GetCliente(string identificacion);
        Task<SolicitudCredito> GenerateSolicitudCredito(SolicitudCredito solicitudCredito);
    }
}
