using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Models;
using Desafio4Tech.Infra.Data;

namespace Infra.Data.Repository
{
    public class PlanoRepository : Repository<PlanoModel>, IPlanoRepository
    {
        public PlanoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
