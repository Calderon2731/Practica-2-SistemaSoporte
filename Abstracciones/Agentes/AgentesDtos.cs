using System.ComponentModel.DataAnnotations;
using Abstracciones.Tickets;

namespace Abstracciones.Agentes
{
    public class CrearAgenteDtos
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [StringLength(50, ErrorMessage = "El correo no puede exceder los 50 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El teléfono debe tener exactamente 8 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "La capacidad máxima debe estar entre 1 y 50.")]
        public int CapacidadMaxima { get; set; }
    }

    public class ActualizarAgenteDtos
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [StringLength(50, ErrorMessage = "El correo no puede exceder los 50 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El teléfono debe tener exactamente 8 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "La capacidad máxima debe estar entre 1 y 50.")]
        public int CapacidadMaxima { get; set; }

        [Required]
        public bool Activo { get; set; }
    }

    public record AgenteDto
    {
        public int Id { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int CapacidadMaxima { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public IReadOnlyList<TicketDto> Tickets { get; set; } = [];
    }
}
