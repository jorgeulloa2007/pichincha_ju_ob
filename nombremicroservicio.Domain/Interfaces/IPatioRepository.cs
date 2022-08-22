using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IPatioRepository
    {
        /// <summary>
        /// Get Patio object based on nombre
        /// </summary>
        /// <param name="identificacion">nombre del patio</param>
        /// <returns></returns>
        Task<Patio> GetPatioByIdentificacion(string identificacion);

        /// <summary>
        /// Add a patio to database
        /// </summary>
        /// <param name="patio">object with patio attributes</param>
        /// <returns></returns>
        Task<Patio> AddPatio(Patio patio);

        /// <summary>
        /// Edit patio attributes
        /// </summary>
        /// <param name="patio">objeect with new patio attibutes</param>
        /// <returns></returns>
        Task<Patio> EditPatio(Patio patio);

        /// <summary>
        /// Deletes patio from DB
        /// </summary>
        /// <param name="identificacion">Patio´s nombre</param>
        /// <returns></returns>
        Task<bool> RemovePatio(string identificacion);

        /// <summary>
        /// Check if Patio is related with any client or executive
        /// </summary>
        /// <param name="identificacion">Patio´s nombre</param>
        /// <returns></returns>
        Task<bool> HaveAssociatedInfo(string identificacion);
    }
}
