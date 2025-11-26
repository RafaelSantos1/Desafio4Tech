using Desafio4Tech.Aplicacao.Dto;

namespace Desafio4Tech.Aplicacao.Interface.Facade
{
    public interface IFacadeBase<TEntity, TDto>
    {
        Task<ResponseDto<TDto>> BuscarPorId(long id);
        Task<ResponseDto<TDto>> Deletar(long id);
        Task<ResponseDto<TDto>> Editar(long id, TDto dto);
        Task<ResponseDto<List<TDto>>> Listar();
    }
}
