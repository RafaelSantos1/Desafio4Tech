using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Models;
using Desafio4Tech.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Data.Repository
{
    public class BeneficiarioRepository : Repository<BeneficiarioModel>, IBeneficiarioRepository
    {
        public BeneficiarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IQueryable<BeneficiarioModel>> GetAsync(Expression<Func<BeneficiarioModel, bool>> expression)
        {
            return await Task.Run(() =>
            {
                return _dbSet.AsNoTracking().Where(expression).Include(x => x.Plano);
            });
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
