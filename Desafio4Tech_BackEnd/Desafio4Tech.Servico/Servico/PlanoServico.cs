using Desafio4Tech.Dominio.Interface.Repository;
using Desafio4Tech.Dominio.Interface.Servico;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Servico.Servico
{
    public class PlanoServico : Servico<PlanoModel>, IPlanoServico
    {
        private readonly IPlanoRepository _repository;
        private readonly IUnitOfWork _uow;

        public PlanoServico(IPlanoRepository repository, IUnitOfWork uow) : base(repository, uow)
        {
            _repository = repository;
            _uow = uow;
        }

        //public async Task<ResponseModel<PlanoModel>> Criar(PlanoDto planoDto)
        //{
        //    ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

        //    try
        //    {
        //        if (await PlanoExiste(planoDto))
        //        {
        //            response.Status = false;
        //            response.Error = "ValidationError";
        //            response.Mensagem = "Plano já criado";
        //            response.Details.Add(new ValidacaoModel
        //            {
        //                Field = "id",
        //                Rule = "não encontrado"
        //            });

        //            return response;
        //        }

        //        PlanoModel plano = _mapper.Map<PlanoModel>(planoCriacaoDto);

        //        var result = await _repository.AddAsync(plano);
        //        await _uow.CommitAsync();

        //        response.Dados = result;
        //        response.Mensagem = "Plano criado com sucesso";
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.Error = "ServerError";
        //        response.Mensagem = ex.Message;
        //        return response;
        //    }
        //}

        //public async Task<ResponseModel<PlanoModel>> Deletar(int id)
        //{
        //    ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

        //    try
        //    {
        //        var plano = await _context.Planos.FindAsync(id);

        //        if (plano == null)
        //        {
        //            response.Status = false;
        //            response.Error = "ValidationError";
        //            response.Mensagem = "Plano não localizado";
        //            response.Details.Add(new ValidacaoModel
        //            {
        //                Field = "id",
        //                Rule = "não encontrado"
        //            });
        //            return response;

        //        }
        //        response.Dados = plano;
        //        response.Mensagem = "Plano removido com sucesso";

        //        _context.Planos.Remove(plano);
        //        await _context.SaveChangesAsync();

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.Error = "ServerError";
        //        response.Mensagem = ex.Message;
        //        return response;
        //    }
        //}

        //public async Task<ResponseModel<PlanoModel>> Editar(PlanoEdicaoDto planoEdicaoDto)
        //{
        //    ResponseModel<PlanoModel> response = new ResponseModel<PlanoModel>();

        //    try
        //    {
        //        var PlanoBanco = _context.Planos.Find(planoEdicaoDto.Id);

        //        if (PlanoBanco == null)
        //        {
        //            response.Status = false;
        //            response.Mensagem = "Plano não localizado";
        //            response.Error = "ValidationError";
        //            response.Details.Add(new ValidacaoModel
        //            {
        //                Field = "id",
        //                Rule = "não encontrado"
        //            });
        //            return response;
        //        }

        //        PlanoBanco.Nome = planoEdicaoDto.Nome;
        //        PlanoBanco.Codigo_registro_ans = planoEdicaoDto.Codigo_registro_ans;

        //        _context.Planos.Update(PlanoBanco);
        //        await _context.SaveChangesAsync();

        //        var planoAtualizado = await _context.Planos.Include(p => p.Beneficiarios).FirstOrDefaultAsync(p => p.Id == PlanoBanco.Id);
        //        response.Dados = planoAtualizado;
        //        response.Mensagem = "Plano editado com sucesso";
        //        return response;

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.Error = "ServerError";
        //        response.Mensagem = ex.Message;
        //        return response;
        //    }
        //}

        //public async Task<bool> PlanoExiste(PlanoDto plano)
        //{
        //    return await _repository.SingleAsync(x => x.Nome.Equals(plano.Nome)) == null;
        //}
    }
}
