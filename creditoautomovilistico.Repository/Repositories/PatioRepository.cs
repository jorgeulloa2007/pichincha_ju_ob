using AutoMapper;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Entities;
using creditoautomovilistico.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace creditoautomovilistico.Repository.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly DatabaseContext _context;
        private IMapper _mapper { get; set; }

        public PatioRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Patio> GetPatioByIdentificacion(string identificacion)
        {
            try
            {
                var patio = await _context.Patios.FirstOrDefaultAsync(p => p.Nombre == identificacion);

                if (patio != null)
                {
                    await _context.Entry(patio).Collection(c => c.Clientes).LoadAsync();
                    await _context.Entry(patio).Collection(c => c.Ejecutivos).LoadAsync();
                    _context.Entry(patio).State = EntityState.Detached;
                   // _context.Entry(patio.Ejecutivos).State = EntityState.Detached;
                }

                return _mapper.Map<Patio>(patio);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Patio> AddPatio(Patio patio)
        {
            try
            {
                var patioToAdd = _mapper.Map<Infrastructure.Models.Patio>(patio);

                var patioAdded = await _context.Patios.AddAsync(patioToAdd);

                await _context.SaveChangesAsync();

                return _mapper.Map<Patio>(patioAdded.Entity);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<Patio> EditPatio(Patio patio)
        {
            try
            {
                var patioToUpd = _mapper.Map<Infrastructure.Models.Patio>(patio);

                var patioUpdated = _context.Update(patioToUpd);

                await _context.SaveChangesAsync();

                return _mapper.Map<Patio>(patioUpdated.Entity);
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(
                                "Error accediendo a datos.", ex);
            }
        }

        public async Task<bool> RemovePatio(string identificacion)
        {
            int successfullyRemoved = 0;

            var patio = await _context.Patios.AsNoTracking().Where(p => p.Nombre == identificacion).FirstOrDefaultAsync();

            if (patio != null)
                try
                {
                    _context.Patios.Remove(patio);

                    successfullyRemoved = await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new DbUpdateException(
                                    "Error accediendo a datos.", ex);
                }

            return successfullyRemoved > 0;
        }

        public async Task<bool> HaveAssociatedInfo(string identificacion)
        {
            try
            {
                var patio = await _context.Patios.AsNoTracking().FirstOrDefaultAsync(p => p.Nombre == identificacion);

                if (patio == null)
                    return false;

                if (patio.Ejecutivos != null || patio.Clientes != null)
                    return true;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(
                                "Error accediendo a datos.", ex);
            }

            return false;
        }
    }
}
