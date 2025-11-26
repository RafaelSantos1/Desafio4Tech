using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Dominio.Interface.Servico
{
    public interface IBeneficiarioServico : IServico<BeneficiarioModel>
    {
        Task<BeneficiarioModel> Criar(BeneficiarioModel beneficiario, long? idPlano, string nomePlano);
    }
}
