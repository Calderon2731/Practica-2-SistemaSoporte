using System;
using System.Collections.Generic;
using System.Text;

namespace Abstracciones.Agentes
{
    public interface IAgenteFlujo
    {
        Task<List<Agente>> ObtenerTodosAsync(CancellationToken ct);
        Task<Agente?> ObtenerPorIdAsync(int id, CancellationToken ct);  
        Task<AgenteDto> AgregarAsync(CrearAgenteDtos dto, CancellationToken ct);
        Task ActualizarAsync(int id, ActualizarAgenteDtos dto, CancellationToken ct);
        Task EliminarAsync(int id, CancellationToken ct);
    }
}
