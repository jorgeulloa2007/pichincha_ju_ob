using AutoMapper;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Domain.Models;
using creditoautomovilistico.Entities;
using creditoautomovilistico.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace creditoautomovilistico.Repository.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly DatabaseContext _context;
        private IMapper _mapper { get; set; }

        public VehiculoRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Vehiculo> AddVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo == null)
                throw new ArgumentNullException(
                                "Error de datos: Datos a actualizar no válidos.");

            try
            {
                var vehToAdd = _mapper.Map<Infrastructure.Models.Vehiculo>(vehiculo);

                var vehAdded = await _context.Vehiculos.AddAsync(vehToAdd);

                await _context.SaveChangesAsync();

                return _mapper.Map<Vehiculo>(vehAdded.Entity);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.",ex);
            }
        }

        public async Task<Vehiculo> EditVehiculo(Vehiculo vehiculo)
        {
            if (vehiculo == null)
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");
            try
            {
                var vehToUpd = _mapper.Map<Infrastructure.Models.Vehiculo>(vehiculo);

                var updatedVeh = _context.Vehiculos.Update(_mapper.Map<Infrastructure.Models.Vehiculo>(vehiculo));

                await _context.SaveChangesAsync();

                return _mapper.Map < Vehiculo > (updatedVeh);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<bool> HaveSolicitudesAsociadas(string placa)
        {
            if (string.IsNullOrEmpty(placa))
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            try
            {
                return await _context.Solicitudes.AnyAsync(v => v.Vehiculo.Placa == placa);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Vehiculo> GetVehiculoByPlaca(string placa)
        {
            if (string.IsNullOrEmpty(placa))
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            try
            {
                return _mapper.Map<Vehiculo>(await _context.Vehiculos.FirstOrDefaultAsync(v => v.Placa == placa));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Vehiculo> GetVehiculoById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            try
            {
                return _mapper.Map<Vehiculo>(await _context.Vehiculos.FirstOrDefaultAsync(v => v.Id == id));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<List<Vehiculo>> GetVehiculosBySearchParams(VehiculoSearchModel searchParams)
        {
            if (searchParams == null)
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");
            try
            {
                return await _context.Vehiculos.AsNoTracking().Where(v => v.Modelo == (searchParams.Modelo ?? v.Modelo))
                        .Join(_context.Marcas.AsNoTracking().Where(m => m.MarcaAuto == (searchParams.Marca ?? m.MarcaAuto)), 
                                v => v.MarcaId, m => m.Id, (v, m) => new {v, m})
                        .Select(vm => new Vehiculo()
                        {
                            Id = vm.v.Id,
                            Placa = vm.v.Placa,
                            Modelo = vm.v.Modelo,
                            NroChasis = vm.v.NroChasis,
                            Tipo = vm.v.Tipo,
                            Cilindraje = vm.v.Cilindraje,
                            Avaluo = vm.v.Avaluo,
                            MarcaId = vm.v.MarcaId
                        })
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<bool> RemoveVehiculo(string placa)
        {
            int successfullyRemoved = 0;

            if (string.IsNullOrEmpty(placa))
                throw new ArgumentNullException(
                               "Error de datos: Datos a actualizar no válidos.");

            var vehiculo = await _context.Vehiculos.Where(v => v.Placa == placa).FirstOrDefaultAsync();

            if (vehiculo!= null)
                try
                {
                    _context.Vehiculos.Remove(vehiculo);

                    successfullyRemoved = await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                                    "Error accediendo a datos.", ex);
                }
            
            return successfullyRemoved > 0;
        }
    }
}
