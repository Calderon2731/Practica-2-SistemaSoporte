using Abstracciones.Agentes;
using Abstracciones.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;


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
                throw new NoEncontradoException("cliente", id);
            }
        }

        public async Task<AgenteDto> AgregarAsync(CrearAgenteDtos dto, CancellationToken ct)
        {
            var correo = NormalizarCorreo(dto.Correo);

       
            var Agente = new Agente
            {
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                CapacidadMaxima = dto.CapacidadMaxima,
                Activo = true,
                FechaIngreso = DateTime.UtcNow,
            };
            await _da.AgregarAsync(Agente, ct);
            return AgenteMapeador.ADto(Agente);
        }

        public async Task EliminarAsync(int id, CancellationToken ct)
        {
            await _da.EliminarAsync(id, ct);
        }

        public async Task<Agente?> ObtenerPorIdAsync(int id, CancellationToken ct)
        {
            var agente = await _da.ObtenerPorIdAsync(id, ct);
            return agente;
        }

        public async Task<List<Agente>> ObtenerTodosAsync(CancellationToken ct)
        {
            var agentes = await _da.ObtenerTodosAsync(ct);
            return agentes;
        }

        private static string NormalizarCorreo(string correo) => correo.Trim().ToLowerInvariant();
    }

}
