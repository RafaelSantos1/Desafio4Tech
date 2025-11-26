using Desafio4Tech.Dominio.Exceptions;
using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Servico.Servico
{
    public class PlanoServico : Servico<PlanoModel>, IPlanoServico
    {
        private readonly IBeneficiarioRepository _beneficiarioRepository;
        public PlanoServico(IPlanoRepository repository, IUnitOfWork uow, IBeneficiarioRepository beneficiarioRepository) : base(repository, uow)
        {
            _beneficiarioRepository = beneficiarioRepository;
        }

        public async Task<PlanoModel> Criar(PlanoModel plano)
        {
            var booleano = await PlanoExiste(plano);
            if ( booleano)
                throw new ServicoException("Plano já criado", "plano", "ValidationError");      


            return await AddAsync(plano);

        }

        public async Task Deletar(long id)
        {

            bool existe = await _beneficiarioRepository.ExistsAsync(x => x.IdPlano == id);          
            if (existe)
                throw new ServicoException("Existe beneficiarios vinculados a este plano", "plano", "ValidationError");


            await DeleteAsync(id);

        }

        private async Task<bool> PlanoExiste(PlanoModel plano)
        {
            var model = await SingleAsync(x => x.Nome.ToUpper().Equals(plano.Nome.ToUpper()) ||
                                                    x.CodigoRegistroAns.ToUpper().Equals(plano.CodigoRegistroAns.ToUpper()));
            return model != null;
        }


    }
}
