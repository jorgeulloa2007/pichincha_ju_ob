using creditoautomovilistico.Domain.Models;
using creditoautomovilistico.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IVehiculoRepository
    {
        /// <summary>
        /// Obtiene datos del vehículo a partir del número de placa
        /// </summary>
        /// <param name="placa">placa del vehículo</param>
        /// <returns></returns>
        Task<Vehiculo> GetVehiculoByPlaca(string placa);

        /// <summary>
        /// Obtiene datos del vehículo a partir de params de búsqueda
        /// </summary>
        /// <param name="searchParams">params de búsqueda</param>
        /// <returns></returns>
        Task<List<Vehiculo>> GetVehiculosBySearchParams(VehiculoSearchModel searchParams);

        /// <summary>
        /// Agrega veh a la DB
        /// </summary>
        /// <param name="vehiculo">objeto con datos del veh</param>
        /// <param name="marca">objeto con datos de la marca de veh</param>
        /// <returns></returns>
        Task<Vehiculo> AddVehiculo(Vehiculo vehiculo, Marca marca);

        /// <summary>
        /// Edita datos del veh
        /// </summary>
        /// <param name="vehiculo">objeto con datos del veh</param>
        /// <param name="marca">objeto con datos de la marca de veh</param>
        /// <returns></returns>
        Task<Vehiculo> EditVehiculo(Vehiculo vehiculo, Marca marca);

        /// <summary>
        /// Elimna veh de la BD si no tiene solicitudes asociadas
        /// </summary>
        /// <param name="placa">placa del vehículo</param>
        /// <returns></returns>
        Task<bool> RemoveVehiculo(string placa);

        /// <summary>
        /// Devuelve true si el vehículo tiene solicitudes de crédito asociadas
        /// </summary>
        /// <param name="placa">placa del vehículo</param>
        /// <returns></returns>
        Task<bool> HaveSolicitudesAsociadas(string placa);

        /// <summary>
        /// Obtiene objeto con datos de la marca del veh a partir de la desc de la marca
        /// </summary>
        /// <param name="marca">marca del vehículo</param>
        /// <returns></returns>
        Task<Marca> GetMarcaVehByName(string marca);
    }
}
