using Abstracciones.Agentes;
using Abstracciones.Excepciones;
using DA.Comunes;
using DA.Contexto;
using Microsoft.EntityFrameworkCore;

namespace DA.Agentes
{
    public class AgenteDA : IAgenteDA
    {
        private readonly SistemaSoporteContexto _contexto;

        public AgenteDA(SistemaSoporteContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ActualizarAsync(Agente datos, CancellationToken ct)
        {
            var agenteExistente = await _contexto.Agentes.FirstOrDefaultAsync(a => a.Id == datos.Id, ct);

            if (agenteExistente == null)
            {
                return false;
            }

            agenteExistente.Correo = datos.Correo;
            agenteExistente.Activo = datos.Activo;
            agenteExistente.CapacidadMaxima = datos.CapacidadMaxima;
            agenteExistente.Telefono = datos.Telefono;

            try
            {
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateException ex) when (ErroresSql.EsDuplicado(ex))
            {
                throw new ConflictoException($"Ya existe otro agente con el correo '{datos.Correo}'.");
            }
        }

        public async Task<Agente> AgregarAsync(Agente agente, CancellationToken ct)
        {
            _contexto.Agentes.Add(agente);

            try
            {
                await _contexto.SaveChangesAsync(ct);
                return agente;
            }
            catch (DbUpdateException ex) when (ErroresSql.EsDuplicado(ex))
            {
                throw new ConflictoException($"Ya existe un agente con el correo '{agente.Correo}'.");
            }
        }

        public async Task<bool> EliminarAsync(int id, CancellationToken ct)
        {
            var agente = await _contexto.Agentes.FirstOrDefaultAsync(a => a.Id == id, ct);
            if (agente == null)
            {
                return false;
            }

            _contexto.Agentes.Remove(agente);
            try
            {
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (DbUpdateException ex) when (ErroresSql.EsLlaveForanea(ex))
            {
                throw new ConflictoException("El agente tiene tickets asociados y no se puede eliminar.");
            }
        }

        public async Task<Agente?> ObtenerPorIdAsync(int id, CancellationToken ct)
        {
            return await _contexto.Agentes
                .AsNoTracking()
                .Include(a => a.Tickets)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task<List<Agente>> ObtenerTodosAsync(CancellationToken ct)
        {
            return await _contexto.Agentes
                .AsNoTracking()
                .Include(a => a.Tickets)
                .ToListAsync(ct);
        }

        public Task<bool> TieneTicketsAsync(int agenteId, CancellationToken ct) =>
            _contexto.Tickets.AnyAsync(t => t.AgenteId == agenteId, ct);
    }
}
