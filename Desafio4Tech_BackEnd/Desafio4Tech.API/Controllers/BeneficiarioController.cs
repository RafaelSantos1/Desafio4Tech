using Desafio4Tech.Aplicacao.Dto;
using Desafio4Tech.Aplicacao.Interface.Facade;
using Microsoft.AspNetCore.Mvc;

namespace Desafio4Tech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : ControllerBase
    {
        private readonly IBeneficiarioFacade _facade;

        public BeneficiarioController(IBeneficiarioFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Lista todos os beneficiários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarBeneficiarios()
        {
            var beneficiario = await _facade.Listar();

            if (!beneficiario.Status)
                return StatusCode(StatusCodes.Status500InternalServerError, beneficiario);

            return Ok(beneficiario);
        }

        /// <summary>
        /// Retorna um beneficiário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detalhe(long id)
        {
            var beneficiario = await _facade.BuscarPorId(id);

            if (!beneficiario.Status)
            {
                if (beneficiario.Error == "ValidationError")
                    return NotFound(beneficiario);

                return StatusCode(StatusCodes.Status500InternalServerError, beneficiario);
            }

            return Ok(beneficiario);
        }

        /// <summary>
        /// Criar um beneficiário
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarBeneficiario([FromBody] BeneficiarioDto dto)
        {
            var response = await _facade.Criar(dto);

            if (!response.Status)
            {
                if (response.Error == "ValidationError")
                    return Conflict(response);

                if (response.Error == "ValidationPlanError")
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, response); 

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

            return CreatedAtAction(nameof(CriarBeneficiario), new { id = response.Dados.Id }, response);
        }

        /// <summary>
        /// Edita um beneficiário existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarBeneficiario([FromBody] BeneficiarioDto dto)
        {
            var beneficiario = await _facade.Editar(dto.Id.Value, dto);

            if (!beneficiario.Status)
            {
                if (beneficiario.Error == "ValidationError")
                    return NotFound(beneficiario);

                return StatusCode(StatusCodes.Status500InternalServerError, beneficiario);
            }

            return Ok(beneficiario);
        }

        /// <summary>
        /// Deleta um beneficiário existente
        /// </summary>
        [HttpPut("deletar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletaBeneficiario(long id)
        {
            var beneficiario = await _facade.Deletar(id);

            if (!beneficiario.Status)
            {
                if (beneficiario.Error == "ValidationError")
                    return NotFound(beneficiario);

                return StatusCode(StatusCodes.Status500InternalServerError, beneficiario);
            }

            return Ok(beneficiario);
        }


        ///// <summary>
        ///// Deleta um beneficiário pelo ID
        ///// </summary>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> DeletarBeneficiario(int id)
        //{
        //    var beneficiario = await _facade.Deletar(id);

        //    if (!beneficiario.Status)
        //    {
        //        if (beneficiario.Error == "ValidationError")
        //            return NotFound(beneficiario);

        //        return StatusCode(StatusCodes.Status500InternalServerError, beneficiario);
        //    }

        //    return Ok(beneficiario);
        //}
    }
}
