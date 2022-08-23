using System;
using System.Threading.Tasks;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Entities;

namespace creditoautomovilistico.Domain.Services
{
    public class PatioService : IPatioService
    {
        protected IPatioRepository _repo;

        public PatioService(IPatioRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> DeletePatio(string identificacion)
        {
            if (await _repo.HaveAssociatedInfo(identificacion))
            {
                return false;
            }

            return await _repo.RemovePatio(identificacion);
        }

        public async Task<Patio> GetPatio(string identificacion)
        {
            return await _repo.GetPatioByIdentificacion(identificacion);
        }

        public async Task<Patio> PostPatio(Patio patio)
        {
            if (await GetPatio(patio.Nombre) != null)
                throw new ApplicationException("No se puede ejecutar la operación.");

            return await _repo.AddPatio(patio);
        }

        public async Task<Patio> PutPatio(Patio patio)
        {
            var patioToUpdate = await GetPatio(patio.Nombre);

            if (patioToUpdate == null)
                throw new ApplicationException("No se puede ejecutar la operación.");

            patio.Id = patioToUpdate.Id;

            return await _repo.EditPatio(patio);
        }
    }
}
