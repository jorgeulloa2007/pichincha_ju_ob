using creditoautomovilistico.Domain.Models;
using creditoautomovilistico.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IVehiculoService
    {
        Task<bool> DeleteVehiculo(string placa);
        Task<List<Vehiculo>> GetVehiculoByMutipleFields(VehiculoSearchModel searchParams);
        Task<Vehiculo> PostVehiculo(Vehiculo vehiculo);
        Task<Vehiculo> PutVehiculo(Vehiculo vehiculo);
        Task<Vehiculo> GetVehiculo(string placa);
    }
}