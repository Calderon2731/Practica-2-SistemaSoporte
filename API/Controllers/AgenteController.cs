using Abstracciones.Agentes;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenteController : ControllerBase
    {
        private readonly IAgenteFlujo _flujo;
        public AgenteController(IAgenteFlujo flujo) => _flujo = flujo;



        // GET: api/<AgenteController>
        [HttpGet]
        [ProducesResponseType<IReadOnlyList<AgenteDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyList<AgenteDto>>> Listar(CancellationToken ct)
        {
            var agentes = await _flujo.ObtenerTodosAsync(ct);
            return Ok(agentes);
        }


        // GET api/<AgenteController>/5
        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType<AgenteDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgenteDto>> ObtenerPorId(int id, CancellationToken ct)
        {
            var agente = await _flujo.ObtenerPorIdAsync(id, ct);
            if (agente == null)
            {
                return NotFound();
            }
            return Ok(agente);
        }

        // POST api/<AgenteController>
        [HttpPost]
        public async Task<ActionResult<AgenteDto>> CrearAgente([FromBody] CrearAgenteDtos dto, CancellationToken ct)
        {
            var agente = await _flujo.AgregarAsync(dto, ct);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = agente.Id }, agente);
        }

        // PUT api/<AgenteController>/5
        [HttpPut("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Actualizar(int id,ActualizarAgenteDtos dto, CancellationToken ct)
        {
            await _flujo.ActualizarAsync(id, dto,ct);
            return NoContent();
        }


        // DELETE api/<AgenteController>/5
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        {
            await _flujo.EliminarAsync(id, ct);
            return NoContent();
        }
    }
}
