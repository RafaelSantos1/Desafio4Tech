using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.AutoMapper
{
    public class PlanoProfile : Profile
    {
        public PlanoProfile()
        {
            CreateMap<PlanoDto, PlanoModel>()
                .ForMember(dest => dest.CodigoRegistroAns, opt => opt.MapFrom(src => src.Codigo_registro_ans));
            CreateMap<PlanoModel, PlanoDto>()
                .ForMember(dest => dest.Codigo_registro_ans, opt => opt.MapFrom(src => src.CodigoRegistroAns));

            CreateMap<IQueryable<PlanoDto>, IQueryable<PlanoModel>>();
            CreateMap<IQueryable<PlanoModel>, IQueryable<PlanoDto>>();
        }
    }
}
