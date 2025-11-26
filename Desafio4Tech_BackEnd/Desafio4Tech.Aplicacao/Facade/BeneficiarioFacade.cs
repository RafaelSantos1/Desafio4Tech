using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Desafio4Tech.Dominio.Exceptions;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.Facade
{
    public class BeneficiarioFacade : FacadeBase<BeneficiarioModel, BeneficiarioDto>, IBeneficiarioFacade
    {
        private readonly IBeneficiarioServico _servico;
        private readonly IPlanoServico _planoServico;
        public BeneficiarioFacade(IBeneficiarioServico servico, IPlanoServico planoServico, IMapper mapper) : base(servico, mapper)
        {
            _servico = servico;
            _planoServico = planoServico;
        }

        public virtual async Task<ResponseDto<List<BeneficiarioDto>>> Listar()
        {
            ResponseDto<List<BeneficiarioDto>> response = new ResponseDto<List<BeneficiarioDto>>();

            try
            {
                var result = await _servico.GetAllAsync();

                response.Dados = _mapper.Map<List<BeneficiarioDto>>(result.ToList());
                response.Mensagem = $"Beneficiarios listados com sucesso";
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

        public async Task<ResponseDto<BeneficiarioDto>> Criar(BeneficiarioDto dto)
        {
            ResponseDto<BeneficiarioDto> response = new ResponseDto<BeneficiarioDto>();

            try
            {   
                var result = await _servico.Criar(_mapper.Map<BeneficiarioModel>(dto),dto.IdPlano,dto.Plano);

                response.Dados = _mapper.Map<BeneficiarioDto>(result);
                response.Mensagem = "Beneficiário criado com sucesso";
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


        public async Task<ResponseDto<BeneficiarioDto>> Deletar(long id)
        {
            ResponseDto<BeneficiarioDto> response = new ResponseDto<BeneficiarioDto>();

            try
            {
                var model = await _servico.GetByIdAsync(id);

                if (model == null)
                {
                    response.Status = false;
                    response.Error = "ValidationError";
                    response.Mensagem = "Beneficario não encontrado";
                    response.Details.Add(new ValidacaoDto
                    {
                        Field = "id",
                        Rule = "invalido"
                    });

                    return response;
                }

                model.DataExclusao = DateTime.Now.AddMinutes(2);
                await _servico.UpdateAsync(model);

                response.Dados = _mapper.Map<BeneficiarioDto>(model);
                response.Mensagem = $"Beneficiario inserido na fila de exclusão com sucesso";
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
    }
}
