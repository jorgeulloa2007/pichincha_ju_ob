using creditoautomovilistico.Domain.Models;
using creditoautomovilistico.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IVehiculoService
    {
        /// <summary>
        /// Elimna veh de la BD si no tiene solicitudes asociadas
        /// </summary>
        /// <param name="placa">placa del vehículo</param>
        /// <returns></returns>
        Task<bool> DeleteVehiculo(string placa);

        /// <summary>
        /// Obtiene datos del vehículo a partir de params de búsqueda
        /// </summary>
        /// <param name="searchParams">params de búsqueda</param>
        /// <returns></returns>
        Task<List<Vehiculo>> GetVehiculoByMutipleFields(VehiculoSearchModel searchParams);

        /// <summary>
        /// Agrega veh a la DB
        /// </summary>
        /// <param name="vehiculo">objeto con datos del veh</param>
        /// <returns></returns>
        Task<Vehiculo> PostVehiculo(Vehiculo vehiculo);

        /// <summary>
        /// Edita datos del veh
        /// </summary>
        /// <param name="vehiculo">objeto con datos del veh</param>
        /// <returns></returns>
        Task<Vehiculo> PutVehiculo(Vehiculo vehiculo);

        /// <summary>
        /// Obtiene datos del vehículo a partir del número de placa
        /// </summary>
        /// <param name="placa">placa del vehículo</param>
        /// <returns></returns>
        Task<Vehiculo> GetVehiculo(string placa);
    }
}