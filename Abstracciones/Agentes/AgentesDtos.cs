using Abstracciones.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Abstracciones.Agentes
{
    public class CrearAgenteDtos
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Correo { get; set; }
        [StringLength(8, ErrorMessage = "El teléfono debe tener exactamente 8 caracteres")]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public int CapacidadMaxima { get; set; }
    }

    public class ActualizarAgenteDtos
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Correo { get; set; }
        [StringLength(8, ErrorMessage = "El teléfono debe tener exactamente 8 caracteres")]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public int CapacidadMaxima { get; set; }
        [Required]
        public bool Activo { get; set; }
    }

    public record AgenteDto
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int CapacidadMaxima { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
