using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.Interface.Facade
{
    public interface IPlanoFacade : IFacadeBase<PlanoModel, PlanoDto>
    {
        Task<ResponseDto<PlanoDto>> Criar(PlanoDto dto);
    }
}
