
namespace Desafio4Tech.Dominio.Models
{
    public class PlanoModel : ModelBase
    {
        public string Nome { get; set; }
        public string CodigoRegistroAns { get; set; }
        public int Prioridade { get; set; } = 5;
        public ICollection<BeneficiarioModel> Beneficiarios { get; set; }

    }
}
