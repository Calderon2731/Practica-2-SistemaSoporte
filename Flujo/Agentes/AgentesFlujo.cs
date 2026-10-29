using Abstracciones.Agentes;
using Abstracciones.Excepciones;

namespace Flujo.Agentes
{
    public class AgentesFlujo : IAgenteFlujo
    {
        private readonly IAgenteDA _da;

        public AgentesFlujo(IAgenteDA da)
        {
            _da = da;
        }

        public async Task ActualizarAsync(int id, ActualizarAgenteDtos datos, CancellationToken ct)
        {
            var correo = NormalizarCorreo(datos.Correo);

            var actualizado = await _da.ActualizarAsync(new Agente
            {
                Id = id,
                Correo = correo,
                Telefono = datos.Telefono,
                CapacidadMaxima = datos.CapacidadMaxima,
                Activo = datos.Activo
            }, ct);

            if (!actualizado) 
            {
                throw new NoEncontradoException("Agente", id);
            }
        }

        public async Task<AgenteDto> AgregarAsync(CrearAgenteDtos dto, CancellationToken ct)
        {
            var correo = NormalizarCorreo(dto.Correo);

            var agente = new Agente
            {
                Correo = correo,
                Telefono = dto.Telefono,
                CapacidadMaxima = dto.CapacidadMaxima,
                Activo = true,
                FechaIngreso = DateTime.UtcNow,
            };
            await _da.AgregarAsync(agente, ct);
            return AgenteMapeador.ADto(agente);
        }

        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            if (await _da.TieneTicketsAsync(id, ct))
            {
                throw new ConflictoException("El agente tiene tickets asociados y no se puede eliminar.");
            }

            var eliminado = await _da.EliminarAsync(id, ct);
            if (!eliminado)
            {
                throw new NoEncontradoException("Agente", id);
            }
        }

        public async Task<AgenteDto?> ObtenerPorIdAsync(int id, CancellationToken ct)
        {
            var agente = await _da.ObtenerPorIdAsync(id, ct);
            return agente is null ? null : AgenteMapeador.ADto(agente);
        }

        public async Task<List<AgenteDto>> ObtenerTodosAsync(CancellationToken ct)
        {
            var agentes = await _da.ObtenerTodosAsync(ct);
            return agentes.Select(AgenteMapeador.ADto).ToList();
        }

        private static string NormalizarCorreo(string correo) => correo.Trim().ToLowerInvariant();
    }
}
