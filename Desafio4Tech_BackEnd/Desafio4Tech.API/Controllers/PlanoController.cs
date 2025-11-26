using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace Desafio4Tech.Controllers
{
    [Route("api/plano")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoFacade _facade;

        public PlanoController(IPlanoFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Lista todos os planos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPlanos()
        {
            var plano = await _facade.Listar();

            if (!plano.Status)
                return StatusCode(StatusCodes.Status500InternalServerError, plano);

            return Ok(plano);
        }

        /// <summary>
        /// Retorna um plano pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detalhe(long id)
        {
            var response = await _facade.BuscarPorId(id);

            if (!response.Status)
            {
                if (response.Error == "ValidationError")
                    return NotFound(response);

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Criar Plano
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarPlano([FromBody] PlanoDto planodto)
        {
            var plano = await _facade.Criar(planodto);

            if (!plano.Status)
            {
                if (plano.Error == "ValidationError")
                    return Conflict(plano);

                return StatusCode(StatusCodes.Status500InternalServerError, plano);
            }

            return CreatedAtAction(nameof(CriarPlano), new { id = plano.Dados.Id }, plano);
        }



        /// <summary>
        /// Edita um plano existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarPlano([FromBody] PlanoDto planodto)
        {
            var plano = await _facade.Editar(planodto.Id.Value, planodto);

            if (!plano.Status)
            {
                if (plano.Error == "ValidationError")
                    return NotFound(plano);

                return StatusCode(StatusCodes.Status500InternalServerError, plano);
            }

            return Ok(plano);
        }

        /// <summary>
        /// Deleta um plano pelo ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarPlano(long id)
        {
            var plano = await _facade.Deletar(id);

            if (!plano.Status)
            {
                if (plano.Error == "ValidationError")
                    return NotFound(plano);

                return StatusCode(StatusCodes.Status500InternalServerError, plano);
            }

            return Ok(plano);
        }
    }
}
