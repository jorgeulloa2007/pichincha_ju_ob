using AutoMapper;
using creditoautomovilistico.Entities;
using creditoautomovilistico.Infrastructure.Context;
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
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Identificacion == identificacion);

                if (cliente != null)
                {
                    _context.Entry(cliente).Collection(c => c.Solicitudes).Load();

                    _context.Entry(cliente).State = EntityState.Detached;
                }

                return _mapper.Map<Cliente>(cliente);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<Cliente> AddCliente(Cliente cliente)
        {
            try
            {
                var clienteToAdd = _mapper.Map<Infrastructure.Models.Cliente>(cliente);

                var clienteAdded = await _context.Clientes.AddAsync(clienteToAdd);

                await _context.SaveChangesAsync();

                return _mapper.Map<Cliente>(clienteAdded.Entity);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<Cliente> EditCliente(Cliente cliente)
        {
            try
            {
                var clienteUpdated = _context.Update(_mapper.Map<Infrastructure.Models.Cliente>(cliente));

                await _context.SaveChangesAsync();

                return _mapper.Map<Cliente>(clienteUpdated.Entity);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<bool> RemoveCliente(string identificacion)
        {
            int successfullyRemoved = 0;

            var cliente = await _context.Clientes.Where(c => c.Identificacion == identificacion).FirstOrDefaultAsync();

            if (cliente != null)
                try
                {
                    _context.Clientes.Remove(cliente);
                    successfullyRemoved = await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
                }

            return successfullyRemoved > 0;
        }


        public async Task<bool> HaveSolicitudesAsociadas(string identificacion)
        {
            try
            {
                return await _context.Solicitudes.AsNoTracking().AnyAsync(s => s.Cliente.Identificacion == identificacion);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<SolicitudCredito> GenerarSolicitud(SolicitudCredito solicitud)
        {
            try
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    var solDb = _mapper.Map<Infrastructure.Models.SolicitudCredito>(solicitud);

                    solDb.Patio.Ejecutivos.Clear();

                    var solicitudSaved = await _context.Solicitudes.AddAsync(solDb);

                    _context.Entry(solDb.Patio).State = EntityState.Detached;
                    _context.Entry(solDb.Cliente).State = EntityState.Detached;
                    _context.Entry(solDb.Vehiculo).State = EntityState.Detached;
                    _context.Entry(solDb.Ejecutivo).State = EntityState.Detached;

                    _context.Entry(solicitudSaved.Entity).State = EntityState.Added;

                    var clientePatio = new Infrastructure.Models.ClientePatio()
                    {
                        FechaAsignacion = DateTime.Now.ToUniversalTime(),
                        Cliente = solDb.Cliente,
                        PatioId = solDb.Patio.Id
                    };

                    solDb.Patio.Clientes.Add(clientePatio);

                    _context.Entry(clientePatio).State = EntityState.Added;

                    await _context.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return _mapper.Map<SolicitudCredito>(solicitudSaved.Entity);
                }
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }
    }
}
