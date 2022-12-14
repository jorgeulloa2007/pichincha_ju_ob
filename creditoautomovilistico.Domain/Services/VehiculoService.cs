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
                throw new InvalidOperationException("No se puede ejecutar la operación.");

            var marca = await _repo.GetMarcaVehByName(vehiculo.MarcaAuto);

            if (marca == null)
                throw new ArgumentOutOfRangeException("No se puede ejecutar la operación.");

            return await _repo.AddVehiculo(vehiculo, marca);
        }

        public async Task<Vehiculo> PutVehiculo(Vehiculo vehiculo)
        {
            var vehToUpdate = await GetVehiculo(vehiculo.Placa);

            if (vehToUpdate == null)
                throw new InvalidOperationException("No se puede ejecutar la operación.");

            var marca = await _repo.GetMarcaVehByName(vehiculo.MarcaAuto);

            if (marca == null)
                throw new InvalidOperationException("No se puede ejecutar la operación.");

            vehiculo.Id = vehToUpdate.Id;

            return await _repo.EditVehiculo(vehiculo, marca);
        }

        public async Task<Vehiculo> GetVehiculo(string placa)
        {
            return await _repo.GetVehiculoByPlaca(placa);
        }
    }
}
