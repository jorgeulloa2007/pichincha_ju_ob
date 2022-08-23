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

        public async Task<Vehiculo> AddVehiculo(Vehiculo vehiculo, Marca marca)
        {
            try
            {
                var vehToAdd = _mapper.Map<Infrastructure.Models.Vehiculo>(vehiculo);

                vehToAdd.MarcaId = marca.Id;

                var vehAdded = await _context.Vehiculos.AddAsync(vehToAdd);

                await _context.SaveChangesAsync();

                var retVeh = _mapper.Map<Vehiculo>(vehAdded.Entity);
                retVeh.MarcaAuto = marca.MarcaAuto;

                return retVeh;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<Vehiculo> EditVehiculo(Vehiculo vehiculo, Marca marca)
        {
            try
            {
                var vehToUpd = _mapper.Map<Infrastructure.Models.Vehiculo>(vehiculo);

                vehToUpd.MarcaId = marca.Id;

                var updatedVeh = _context.Vehiculos.Update(_mapper.Map<Infrastructure.Models.Vehiculo>(vehiculo));

                await _context.SaveChangesAsync();

                var retVeh = _mapper.Map<Vehiculo>(updatedVeh.Entity);
                retVeh.MarcaAuto = marca.MarcaAuto;

                return retVeh;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<bool> HaveSolicitudesAsociadas(string placa)
        {
            try
            {
                return await _context.Solicitudes.AsNoTracking().AnyAsync(v => v.Vehiculo.Placa == placa);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<Vehiculo> GetVehiculoByPlaca(string placa)
        {
            try
            {
                var vehDb = await _context.Vehiculos.AsNoTracking().FirstOrDefaultAsync(v => v.Placa == placa);
                var veh = _mapper.Map<Vehiculo>(vehDb);

                if (veh != null)
                    veh.MarcaAuto = _context.Marcas.AsNoTracking().FirstOrDefault(m => m.Id == vehDb.MarcaId)?.MarcaAuto;

                return veh;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<List<Vehiculo>> GetVehiculosBySearchParams(VehiculoSearchModel searchParams)
        {
            try
            {
                return await _context.Vehiculos
                    .AsNoTracking()
                    .Where(v => v.Modelo == (searchParams.Modelo ?? v.Modelo) && v.Year == (searchParams.Year ?? v.Year))
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
                            MarcaAuto = vm.m.MarcaAuto,
                            Year = vm.v.Year
                        })
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }

        public async Task<bool> RemoveVehiculo(string placa)
        {
            int successfullyRemoved = 0;

            var vehiculo = await _context.Vehiculos.Where(v => v.Placa == placa).FirstOrDefaultAsync();

            if (vehiculo!= null)
                try
                {
                    _context.Vehiculos.Remove(vehiculo);

                    successfullyRemoved = await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
                }
            
            return successfullyRemoved > 0;
        }

        public async Task<Marca> GetMarcaVehByName(string marca)
        {
            try
            {
                return _mapper.Map<Marca>(await _context.Marcas.AsNoTracking().FirstOrDefaultAsync(v => v.MarcaAuto == marca));
            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error accediendo a datos: " + ex.Message);
            }
        }
    }
}
