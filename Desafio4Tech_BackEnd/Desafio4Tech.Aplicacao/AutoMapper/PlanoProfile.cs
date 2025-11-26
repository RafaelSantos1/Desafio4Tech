using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.AutoMapper
{
    public class PlanoProfile : Profile
    {
        public PlanoProfile()
        {
            CreateMap<PlanoDto, PlanoModel>();
            CreateMap<PlanoModel, PlanoDto>();

            CreateMap<IQueryable<PlanoDto>, IQueryable<PlanoModel>>();
            CreateMap<IQueryable<PlanoModel>, IQueryable<PlanoDto>>();
        }
    }
}
