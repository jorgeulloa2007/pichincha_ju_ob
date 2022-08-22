using creditoautomovilistico.Entities;
using System.Threading.Tasks;

namespace creditoautomovilistico.Domain.Interfaces
{
    public interface IPatioService
    {
        Task<bool> DeletePatio(string identificacion);
        Task<Patio> PostPatio(Patio Patio);
        Task<Patio> PutPatio(Patio Patio);
        Task<Patio> GetPatio(string identificacion);
    }
}