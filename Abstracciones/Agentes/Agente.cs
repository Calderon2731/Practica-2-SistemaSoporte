using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abstracciones.Tickets;

namespace Abstracciones.Agentes
{
    public class Agente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Correo { get; set; }


        [StringLength(8,ErrorMessage = "El teléfono debe tener exactamente 8 caracteres")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public int CapacidadMaxima { get; set; }

        [Required]
        public bool Activo { get; set; }

        public DateTime FechaIngreso { get; set; }

        // Navegaciones
        public List<Ticket> Tickets { get; set; } = new(); // 1-N
    }
}
