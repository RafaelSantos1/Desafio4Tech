
using Desafio4Tech.Dominio.Enum;
using System.ComponentModel.DataAnnotations;

namespace Desafio4Tech.Aplicacao.Dto
{
    public class BeneficiarioDto : BaseDto
    {
        [Required]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [RegularExpression(@"(\d{3}\.\d{3}\.\d{3}-\d{2}|\d{11})", ErrorMessage = "CPF deve estar no formato 000.000.000-00 ou 00000000000")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }
        public Status Status { get; set; }
        public string? Plano { get; set; }
        public long? IdPlano { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
