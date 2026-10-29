using Abstracciones.Agentes;
using Flujo.Tickets;

namespace Flujo.Agentes
{
    internal static class AgenteMapeador
    {
        public static AgenteDto ADto(Agente agente) =>
            new AgenteDto
            {
                Id = agente.Id,
                Correo = agente.Correo,
                Telefono = agente.Telefono,
                CapacidadMaxima = agente.CapacidadMaxima,
                Activo = agente.Activo,
                FechaIngreso = agente.FechaIngreso,
                Tickets = agente.Tickets?.Select(TicketMapeador.ADto).ToList() ?? []
            };
    }
}
