using Desafio4Tech.Dominio.Models;
using System.Linq.Expressions;

namespace Desafio4Tech.Dominio.Interface.Servico
{
    public interface IBeneficiarioServico : IServico<BeneficiarioModel>
    {
    //    Task<IQueryable<BeneficiarioModel>> GetAsync(Expression<Func<BeneficiarioModel, bool>> expression);
        Task<BeneficiarioModel> Criar(BeneficiarioModel beneficiario, long? idPlano, string nomePlano);
    }
}
