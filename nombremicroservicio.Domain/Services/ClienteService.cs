using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Entities;
using nombremicroservicio.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Services
{
    public class ClienteService : IClienteService
    {
        protected IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        private async Task<bool> GetSolicitudesAsociadas(string identificacion)
        {
            return await _repo.HaveSolicitudesAsociadas(identificacion);
        }

        public async Task<bool> DeleteCliente(string identificacion)
        {
            if (await GetSolicitudesAsociadas(identificacion))
            {
                return false;
            }

            return await _repo.RemoveCliente(identificacion);
        }

        public async Task<Cliente> GetCliente(string identificacion)
        {
            return await _repo.GetClienteByIdentificacion(identificacion);
        }

        public async Task<Cliente> PostCliente(Cliente cliente)
        {
            if (await GetCliente(cliente.Identificacion) != null)
                throw new ApplicationException("No se puede ejecutar la operación.");

            return await _repo.AddCliente(cliente);
        }

        public async Task<Cliente> PutCliente(Cliente cliente)
        {
            return await _repo.EditCliente(cliente);
        }

        public async Task<SolicitudCredito> GenerateSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            var cliente = await _repo.GetClienteByIdentificacion(solicitudCredito.Cliente.Identificacion);
            
            if (cliente == null)
                throw new ArgumentException("Cliente  no válido");

            if (cliente.Solicitudes.Any(s => s.FechaElaboracion.Date == DateTime.Now.Date 
                                    && s.Estado != Entities.Enumerations.TipoEstadoSolicitud.Cancelada))
                throw new ArgumentException("Cliente ya tiene una solicitud realizada el día de hoy.");

            solicitudCredito.Cliente = cliente;

            return await _repo.GenerarSolicitud(solicitudCredito);
        }
    }
}
