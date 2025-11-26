using Desafio4Tech.Dominio.Enum;

namespace Desafio4Tech.Dominio.Models
{
    public class BeneficiarioModel : ModelBase
    {
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.ATIVO;
        public long IdPlano { get; set; }
        public PlanoModel Plano { get; set; }
    }

}
