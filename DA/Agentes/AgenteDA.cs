using Abstracciones.Agentes;
using DA.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

            var AgenteExistente = _contexto.Agentes.FirstOrDefault(a => a.Id == datos.Id);

            if (AgenteExistente == null)
            {
                return false;
            }

            // Actualizar las propiedades del agente
            AgenteExistente.Correo = datos.Correo;
            AgenteExistente.Activo = datos.Activo;
            AgenteExistente.CapacidadMaxima = datos.CapacidadMaxima;
            AgenteExistente.Telefono = datos.Telefono;
            
            // ... actualizar otras propiedades según sea necesario

            try
            {
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new ValidationException("Error al actualizar el agente.", ex);
            }
        }

        public async Task<Agente> AgregarAsync(Agente agente, CancellationToken ct)
        {
            _contexto.Agentes.Add(agente);

            try
            {
                return await _contexto.SaveChangesAsync(ct)
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted)
                        {
                            throw new ValidationException("Error al guardar el agente.");
                        }
                        return agente;
                    }, ct);

            }
            catch (Exception ex)
            {
                throw new ValidationException("Error al guardar el agente.", ex);
            }
        }

        public async Task<bool> EliminarAsync(int id, CancellationToken ct)
        {

            var agente = await _contexto.Agentes.FindAsync(id);
            if (agente == null)
            {
                return false;
            }
            else
            {
                _contexto.Agentes.Remove(agente);
                await _contexto.SaveChangesAsync(ct);
                return true;
            }
        }

        public async Task<Agente?> ObtenerPorIdAsync(int id, CancellationToken ct)
        {
            var agente = await _contexto.Agentes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, ct);
            return agente;
        }

        public async Task<List<Agente>>  ObtenerTodosAsync(CancellationToken ct)
        {
            return  await _contexto.Agentes.ToListAsync(ct);
        }
                       
    }
}
