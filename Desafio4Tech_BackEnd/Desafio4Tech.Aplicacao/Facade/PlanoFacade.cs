using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.Facade
{
    public class PlanoFacade : FacadeBase<PlanoModel, PlanoDto>, IPlanoFacade
    {
        public PlanoFacade(IPlanoServico servico, IMapper mapper) : base(servico, mapper)
        {
        }

        public async Task<ResponseDto<PlanoDto>> Criar(PlanoDto dto)
        {
            ResponseDto<PlanoDto> response = new ResponseDto<PlanoDto>();

            try
            {
                var booleano = await PlanoExiste(dto);
                if (booleano)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = "Plano já existe";
                    response.Details.Add(new ValidacaoDto
                    {
                        Field = "Nome",
                        Rule = "nome plano ou codigo registro ans encontrado"
                    });

                    return response;
                }

                PlanoModel plano = _mapper.Map<PlanoModel>(dto);

                var result = await _servico.AddAsync(plano);
                response.Dados = _mapper.Map<PlanoDto>(result);
                response.Mensagem = "Plano criado com sucesso";
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

        private async Task<bool> PlanoExiste(PlanoDto plano)
        {
            var s = await _servico.SingleAsync(x => x.Nome.ToUpper().Equals(plano.Nome.ToUpper()) ||
                                                    x.CodigoRegistroAns.ToUpper().Equals(plano.Codigo_registro_ans.ToUpper()));
            return s != null;
        }
    }
}
