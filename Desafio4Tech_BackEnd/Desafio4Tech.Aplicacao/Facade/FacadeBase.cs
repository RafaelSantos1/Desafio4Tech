using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Desafio4Tech.Dominio.Interface.Servico;

namespace Desafio4Tech.Aplicacao.Facade
{
    public class FacadeBase<TEntity, TDto> : IFacadeBase<TEntity, TDto> where TEntity : class
    {
        protected readonly IServico<TEntity> _servico;
        protected readonly IMapper _mapper;

        public FacadeBase(IServico<TEntity> servico, IMapper mapper)
        {
            _servico = servico;
            _mapper = mapper;
        }

        public async Task<ResponseDto<TDto>> BuscarPorId(long id)
        {
            ResponseDto<TDto> response = new ResponseDto<TDto>();

            try
            {
                var model = await _servico.GetByIdAsync(id);

                if (model == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = $"{typeof(TEntity).Name.Replace("Model", "")} não localizado";
                    response.Details.Add(new ValidacaoDto
                    {
                        Field = "id",
                        Rule = "not_found"
                    });

                    return response;
                }
                response.Dados = _mapper.Map<TDto>(model);
                response.Mensagem = $"{typeof(TEntity).Name.Replace("Model", "")} localizado com sucesso";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }


        public async Task<ResponseDto<TDto>> Deletar(long id)
        {
            ResponseDto<TDto> response = new ResponseDto<TDto>();

            try
            {
                var model = await _servico.GetByIdAsync(id);

                if (model == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = $"{typeof(TEntity).Name.Replace("Model","")} não localizado";
                    response.Details.Add(new ValidacaoDto
                    {
                        Field = "id",
                        Rule = "não encontrado"
                    });
                    return response;
                }

                await _servico.DeleteAsync(id);

                response.Mensagem = $"{typeof(TEntity).Name.Replace("Model", "")} removido com sucesso";

                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseDto<TDto>> Editar(long id, TDto dto)
        {
            ResponseDto<TDto> response = new ResponseDto<TDto>();

            try
            {
                var model = await _servico.GetByIdAsync(id);

                if (model == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = "CPF inválido";
                    response.Details.Add(new ValidacaoDto
                    {
                        Field = "cpf",
                        Rule = "invalid"
                    });

                    return response;
                }

                _mapper.Map(dto, model);
                await _servico.UpdateAsync(model);

                response.Dados = dto;
                response.Mensagem = $"{typeof(TEntity).Name.Replace("Model", "")} editado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "NotFound";
                response.Mensagem = ex.Message;
                return response;
            }
        }

        public async Task<ResponseDto<List<TDto>>> Listar()
        {
            ResponseDto<List<TDto>> response = new ResponseDto<List<TDto>>();

            try
            {
                var result = await _servico.GetAsync();

                response.Dados = _mapper.Map<List<TDto>>(result.ToList());
                response.Mensagem = $"{typeof(TEntity).Name.Replace("Model", "")}s listados com sucesso";
                return response;


            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Error = "ServerError";
                response.Mensagem = ex.Message;
                return response;
            }
        }


    }
}
