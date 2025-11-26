


using Desafio4Tech.Dominio.Exceptions;
using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Dominio.Models;
using System.Numerics;

namespace Desafio4Tech.Servico.Servico
{
    public class BeneficiarioServico : Servico<BeneficiarioModel>, IBeneficiarioServico
    {
        private IPlanoServico _planoServico;

        public BeneficiarioServico(IBeneficiarioRepository repository, IUnitOfWork uow, IPlanoServico planoServico) : base(repository, uow)
        {
            _planoServico = planoServico;
        }

        public async Task<BeneficiarioModel> Criar(BeneficiarioModel beneficiario, long? idPlano,string nomePlano)
        {            
            if (await BeneficiarioExiste(beneficiario))                                 
                throw new ServicoException("beneficiário já criado", "cpf", "ValidationError");

            if (!idPlano.HasValue && string.IsNullOrEmpty(nomePlano))
                throw new ServicoException("nenhum parâmetro para buscar plano", "plano", "ValidationPlanError");

            PlanoModel plano = new PlanoModel();
            if (idPlano.HasValue)
                plano = await _planoServico.GetByIdAsync(idPlano.Value);
            else
                plano = await _planoServico.SingleAsync(x => x.Nome.ToUpper().Equals(nomePlano.ToUpper()));

            if (plano == null)
                throw new ServicoException("plano não encontrado", "plano");

            beneficiario.IdPlano = plano.Id;               

            return await AddAsync(beneficiario);         

        }

        private async Task<bool> BeneficiarioExiste(BeneficiarioModel beneficiario)
        {
            return await SingleAsync(x => x.Cpf.Equals(beneficiario.Cpf)) != null;
        }

    }
}
