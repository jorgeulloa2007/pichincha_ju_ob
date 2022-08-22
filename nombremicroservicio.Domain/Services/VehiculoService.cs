using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Domain.Models;
using creditoautomovilistico.Entities;

namespace creditoautomovilistico.Domain.Services
{
    public class VehiculoService : IVehiculoService
    {
        protected IVehiculoRepository _repo;

        public VehiculoService(IVehiculoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> DeleteVehiculo(string placa)
        {
            if (await GetSolicitudesAsociadas(placa))
            {
                return false;
            }

            return await _repo.RemoveVehiculo(placa);
        }

        private async Task<bool> GetSolicitudesAsociadas(string placa)
        {
            return await _repo.HaveSolicitudesAsociadas(placa);
        }

        public async Task<List<Vehiculo>> GetVehiculoByMutipleFields(VehiculoSearchModel searchParams)
        {
            return await _repo.GetVehiculosBySearchParams(searchParams);

        }

        public async Task<Vehiculo> PostVehiculo(Vehiculo vehiculo)
        {
            if (await GetVehiculo(vehiculo.Placa) != null)
                throw new ApplicationException("No se puede ejecutar la operación.");

            return await _repo.AddVehiculo(vehiculo);
        }

        public async Task<Vehiculo> PutVehiculo(Vehiculo vehiculo)
        {
            return await _repo.EditVehiculo(vehiculo);
        }

        public async Task<Vehiculo> GetVehiculo(string placa)
        {
            return await _repo.GetVehiculoByPlaca(placa);
        }
    }
}
