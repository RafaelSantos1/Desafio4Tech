using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Models;
using Desafio4Tech.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class BeneficiarioRepository : Repository<BeneficiarioModel>, IBeneficiarioRepository
    {
        public BeneficiarioRepository(AppDbContext context) : base(context)
        {
        }       

        public override async Task<IQueryable<BeneficiarioModel>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return _dbSet.AsNoTracking().AsQueryable().Include(x => x.Plano);
            });
        }

        public override async Task<BeneficiarioModel> GetByIdAsync(long id)
        {
            return await _context.Beneficiarios
                .Include(b => b.Plano)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

    }
}
