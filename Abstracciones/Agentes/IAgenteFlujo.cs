using System;
using System.Collections.Generic;
using System.Text;

namespace Abstracciones.Agentes
{
    public interface IAgenteFlujo
    {
        Task<List<AgenteDto>> ObtenerTodosAsync(CancellationToken ct);
        Task<AgenteDto?> ObtenerPorIdAsync(int id, CancellationToken ct);  
        Task<AgenteDto> AgregarAsync(CrearAgenteDtos dto, CancellationToken ct);
        Task ActualizarAsync(int id, ActualizarAgenteDtos dto, CancellationToken ct);
        Task EliminarAsync(int id, CancellationToken ct);
    }
}
