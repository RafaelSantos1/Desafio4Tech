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
                  .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome_completo))
                  .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.Data_nascimento))
                  .ForMember(dest => dest.Plano, opt => opt.Ignore());
            CreateMap<BeneficiarioModel, BeneficiarioDto>()
                 .ForMember(dest => dest.Nome_completo, opt => opt.MapFrom(src => src.NomeCompleto))
                 .ForMember(dest => dest.Data_nascimento, opt => opt.MapFrom(src => src.DataNascimento))
                 .ForMember(dest => dest.Plano, opt => opt.MapFrom(src => src.Plano.Nome));
        }
    }
}
