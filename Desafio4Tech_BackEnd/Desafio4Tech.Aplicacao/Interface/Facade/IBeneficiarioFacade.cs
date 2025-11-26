using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.Interface.Facade
{
    public interface IBeneficiarioFacade : IFacadeBase<BeneficiarioModel, BeneficiarioDto>
    {
        Task<ResponseDto<BeneficiarioDto>> Criar(BeneficiarioDto dto);
        Task<ResponseDto<BeneficiarioDto>> Deletar(long id);
    }
}
