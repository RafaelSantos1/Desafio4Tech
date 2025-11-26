using Desafio4Tech.Dominio.Interface.Servico;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Desafio4Tech.Worker.Workes
{
    public class ProcessamentoExclusaoBeneficiario : BackgroundService
    {
        private readonly ILogger<ProcessamentoExclusaoBeneficiario> _logger;
        private readonly IServiceProvider _serviceProvider;
        public ProcessamentoExclusaoBeneficiario(ILogger<ProcessamentoExclusaoBeneficiario> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            TimeSpan intervalo = TimeSpan.FromSeconds(1);
            using PeriodicTimer periodo = new PeriodicTimer(intervalo);
            long idBeneficiario = 0;
            try
            {
                _logger.LogInformation("Início Worker de exclusão : {hora}", DateTime.Now);

                while (!stoppingToken.IsCancellationRequested && await periodo.WaitForNextTickAsync(stoppingToken))
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        _logger.LogInformation("Início do processo de remoção {Hora}", DateTime.Now);
                        var beneficiarioServico = scope.ServiceProvider.GetRequiredService<IBeneficiarioServico>();

                        var beneficiario = (await beneficiarioServico.GetAsync(
                                                    x => x.DataExclusao != null && x.DataExclusao <= DateTime.Now
                                                ))
                                                .OrderBy(x => x.Plano.Prioridade)
                                                .ThenBy(x => x.DataExclusao)
                                                .FirstOrDefault();
                        if(beneficiario != null)
                        {
                            idBeneficiario = beneficiario.Id;

                            _logger.LogInformation(
                                      "Remoção do beneficiário: {Id} - {Nome} - Plano {Plano} - DataExclusao {DataExclusao}",
                                      beneficiario.Id,
                                      beneficiario.NomeCompleto,
                                      beneficiario.Plano.Nome,
                                      beneficiario.DataExclusao,
                                      DateTime.Now
                                  );

                           await beneficiarioServico.DeleteAsync(beneficiario.Id);

                            _logger.LogInformation("Beneficiario removido com sucesso");
                        }
                        else
                            _logger.LogInformation("Beneficiario não encontrado para remoção");

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover beneficiário com Id: {Id} às {Hora}", idBeneficiario, DateTime.Now);
            }

            _logger.LogInformation("Fim worker {Hora}", DateTime.Now);
        }
    }
}
