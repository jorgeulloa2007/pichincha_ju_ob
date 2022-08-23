using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IPatioService
    {
        /// <summary>
        /// Deletes patio from DB
        /// </summary>
        /// <param name="identificacion">Patio´s nombre</param>
        /// <returns></returns>
        Task<bool> DeletePatio(string identificacion);

        /// <summary>
        /// Adds a patio to database
        /// </summary>
        /// <param name="patio">object with patio attributes</param>
        /// <returns></returns>
        Task<Patio> PostPatio(Patio Patio);

        /// <summary>
        /// Edit patio attributes
        /// </summary>
        /// <param name="patio">objeect with new patio attibutes</param>
        /// <returns></returns>
        Task<Patio> PutPatio(Patio Patio);

        /// <summary>
        /// Get Patio object based on nombre
        /// </summary>
        /// <param name="identificacion">nombre del patio</param>
        /// <returns></returns>
        Task<Patio> GetPatio(string identificacion);
    }
}