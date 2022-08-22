using creditoautomovilistico.Domain.Models;
using creditoautomovilistico.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IVehiculoRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        Task<Vehiculo> GetVehiculoByPlaca(string placa);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        Task<List<Vehiculo>> GetVehiculosBySearchParams(VehiculoSearchModel searchParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        Task<Vehiculo> AddVehiculo(Vehiculo vehiculo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        Task<Vehiculo> EditVehiculo(Vehiculo vehiculo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        Task<bool> RemoveVehiculo(string placa);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placa"></param>
        /// <returns></returns>
        Task<bool> HaveSolicitudesAsociadas(string placa);
    }
}
