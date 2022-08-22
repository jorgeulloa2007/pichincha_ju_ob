using AutoMapper;
using creditoautomovilistico.Entities;
using creditoautomovilistico.Infrastructure.Context;
//using creditoautomovilistico.Infrastructure.Models;
//using creditoautomovilistico.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using nombremicroservicio.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace creditoautomovilistico.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DatabaseContext _context;
        private IMapper _mapper { get; set; }

        public ClienteRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Cliente> GetClienteByIdentificacion(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            try
            {
                return _mapper.Map<Cliente>(await _context.Clientes.FirstOrDefaultAsync(c => c.Identificacion == identificacion));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Cliente> AddCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(
                                "Error de datos: Datos a actualizar no válidos.");

            try
            {
                var clienteToAdd = _mapper.Map<Infrastructure.Models.Cliente>(cliente);

                var clienteAdded = await _context.Clientes.AddAsync(clienteToAdd);

                await _context.SaveChangesAsync();

                return _mapper.Map<Cliente>(clienteAdded.Entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Cliente> EditCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");
            try
            {
                var clienteUpdated = _context.Update(_mapper.Map<Infrastructure.Models.Cliente>(cliente));

                await _context.SaveChangesAsync();

                return _mapper.Map<Cliente>(clienteUpdated);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<bool> RemoveCliente(string identificacion)
        {
            int successfullyRemoved = 0;

            if (string.IsNullOrEmpty(identificacion))
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            var cliente = await _context.Clientes.Where(c => c.Identificacion == identificacion).FirstOrDefaultAsync();

            if (cliente != null)
                try
                {
                    _context.Clientes.Remove(cliente);
                    successfullyRemoved = await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                                    "Error accediendo a datos.", ex);
                }

            return successfullyRemoved > 0;
        }


        public async Task<bool> HaveSolicitudesAsociadas(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            try
            {
                return await _context.Solicitudes.AnyAsync(s => s.Cliente.Identificacion == identificacion);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Cliente> GetClienteById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            try
            {
                return _mapper.Map<Cliente>(await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<SolicitudCredito> GenerarSolicitud(SolicitudCredito solicitud)
        {
            if (solicitud == null)
                throw new ArgumentNullException(
                                "Error de datos: Datos a actualizar no válidos.");

            var patio = _context.Patios.Where(p => p.Nombre == solicitud.Patio.Nombre).FirstOrDefault();

            if (patio.Ejecutivos.FirstOrDefault(e => e.Identificacion == solicitud.Ejecutivo.Identificacion) == null)
                throw new ArgumentException("Ejecutivo no pertenece al Patio donde se registra la solicitud.");


            var solicitudDB = new SolicitudCredito();
            try
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    solicitudDB = _mapper.Map<SolicitudCredito>(
                    _context.Solicitudes.Add(_mapper.Map<Infrastructure.Models.SolicitudCredito>(solicitud))
                    );

                    var clientePatio = new Infrastructure.Models.ClientePatio()
                    {
                        FechaAsignacion = DateTime.Now,
                        Cliente = _mapper.Map<Infrastructure.Models.Cliente>(solicitud.Cliente)
                    };

                    patio.Clientes.Add(clientePatio);

                    await _context.SaveChangesAsync();

                    dbContextTransaction.Commit();
                }
                return solicitudDB;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }
    }
}
