using AutoMapper;
using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Dominio.Models;

namespace Desafio4Tech.Aplicacao.AutoMapper
{
    public class BeneficiarioProfile : Profile
    {
        public BeneficiarioProfile()
        {
            CreateMap<BeneficiarioDto, BeneficiarioModel>()
                  .ForMember(dest => dest.Plano, opt => opt.Ignore());
            CreateMap<BeneficiarioModel, BeneficiarioDto>()
                 .ForMember(dest => dest.Plano, opt => opt.MapFrom(src => src.Plano.Nome));
        }
    }
}
