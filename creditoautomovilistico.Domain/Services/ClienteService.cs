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
        protected IPatioRepository _patioRepo;
        protected IVehiculoRepository _vehRepo;

        public ClienteService(IClienteRepository repo, IPatioRepository patioRepo, IVehiculoRepository vehRepo)
        {
            _repo = repo;
            _patioRepo = patioRepo;
            _vehRepo = vehRepo;
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
            var clienteToUpdate = await GetCliente(cliente.Identificacion);

            if (clienteToUpdate == null)
                throw new ApplicationException("No se puede ejecutar la operación.");

            cliente.Id = clienteToUpdate.Id;

            return await _repo.EditCliente(cliente);
        }

        public async Task<SolicitudCredito> GenerateSolicitudCredito(SolicitudCredito solicitudCredito)
        {
            var cliente = await _repo.GetClienteByIdentificacion(solicitudCredito.Cliente.Identificacion);
            
            if (cliente == null)
                throw new InvalidOperationException("Cliente  no válido");

            if (cliente.Solicitudes.Any(s => s.FechaElaboracion.Date == DateTime.Now.Date 
                                    && s.Estado != Entities.Enumerations.TipoEstadoSolicitud.Cancelada))
                throw new InvalidOperationException("Cliente ya tiene una solicitud realizada el día de hoy.");

            var patio = await _patioRepo.GetPatioByIdentificacion(solicitudCredito.Patio.Nombre);

            if (patio == null)
                throw new InvalidOperationException("Patio no válido.");

            var ejecutivo = patio.Ejecutivos
                .AsReadOnly()
                .Where(e => e.Identificacion == solicitudCredito.Ejecutivo.Identificacion)
                .FirstOrDefault();

            if (ejecutivo == null)
                throw new InvalidOperationException("Ejecutivo no pertenece al Patio donde se registra la solicitud.");

            var veh = await _vehRepo.GetVehiculoByPlaca(solicitudCredito.Vehiculo.Placa);
            if (veh == null)
                throw new InvalidOperationException("Vehículo no válido.");

            solicitudCredito.Cliente = cliente;
            solicitudCredito.Patio = patio;
            solicitudCredito.Ejecutivo = ejecutivo;
            solicitudCredito.Vehiculo = veh;

            return await _repo.GenerarSolicitud(solicitudCredito);
        }
    }
}
