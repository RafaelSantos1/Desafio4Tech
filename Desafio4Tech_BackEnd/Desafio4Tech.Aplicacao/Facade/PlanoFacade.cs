using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Desafio4Tech.Dominio.Exceptions;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.Facade
{
    public class PlanoFacade : FacadeBase<PlanoModel, PlanoDto>, IPlanoFacade
    {
        private readonly IPlanoServico _servico;
        public PlanoFacade(IPlanoServico servico, IMapper mapper) : base(servico, mapper)
        {
            _servico = servico;
        }

        public async Task<ResponseDto<PlanoDto>> Criar(PlanoDto dto)
        {
            ResponseDto<PlanoDto> response = new ResponseDto<PlanoDto>();

            try
            {
                var result = await _servico.Criar(_mapper.Map<PlanoModel>(dto));
                response.Dados = _mapper.Map<PlanoDto>(result);
                response.Mensagem = "Plano criado com sucesso";
                return response;
            }
            catch (ServicoException ex)
            {
                response.Status = false;
                response.Error = ex.Error;
                response.Mensagem = ex.Message;
                response.Details.Add(new ValidacaoDto
                {
                    Field = ex.Field,
                    Rule = ex.Message
                });

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

        public override async Task<ResponseDto<PlanoDto>> Deletar(long id)
        {
            ResponseDto<PlanoDto> response = new ResponseDto<PlanoDto>();

            try
            {
                var model = await _servico.GetByIdAsync(id);

                if (model == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = $"Plano não localizado";
                    response.Details.Add(new ValidacaoDto
                    {
                        Field = "id",
                        Rule = "não encontrado"
                    });
                    return response;
                }

                await _servico.Deletar(id);

                response.Mensagem = $"Plano removido com sucesso";

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
