using System;
using System.Collections.Generic;
using System.Text;

namespace Abstracciones.Agentes
{
    public interface IAgenteDA
    {
        Task<List<Agente>> ObtenerTodosAsync(CancellationToken ct);

        Task<Agente?> ObtenerPorIdAsync(int id, CancellationToken ct);

        Task<Agente> AgregarAsync(Agente agente, CancellationToken ct);

        Task<bool> ActualizarAsync(Agente datos, CancellationToken ct);

        Task<bool> EliminarAsync(int id, CancellationToken ct);
    }
}
