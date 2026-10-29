using Abstracciones.Agentes;
using Abstracciones.Excepciones;
using Abstracciones.Tickets;
using Flujo.Agentes;
using Moq;

namespace Pruebas;

public class AgenteFlujoTests
{
    private readonly Mock<IAgenteDA> _da = new();
    private readonly AgentesFlujo _flujo;

    public AgenteFlujoTests()
    {
        _flujo = new AgentesFlujo(_da.Object);
    }

    [Fact]
    public async Task ObtenerPorId_ConTickets_MapeaCorrectamenteListaTickets()
    {
        var agente = new Agente
        {
            Id = 1,
            Correo = "agente@soporte.com",
            Telefono = "88887777",
            CapacidadMaxima = 10,
            Activo = true,
            FechaIngreso = DateTime.UtcNow,
            Tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 101,
                    Codigo = "TK-2026-0001",
                    Titulo = "Error de red",
                    Descripcion = "Falla de conexión",
                    Prioridad = PrioridadTicket.Alta,
                    Estado = EstadoTicket.EnProceso,
                    ClienteId = 1,
                    AgenteId = 1
                }
            }
        };

        _da.Setup(d => d.ObtenerPorIdAsync(1, It.IsAny<CancellationToken>()))
           .ReturnsAsync(agente);

        var resultado = await _flujo.ObtenerPorIdAsync(1, CancellationToken.None);

        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.Id);
        Assert.Equal("agente@soporte.com", resultado.Correo);
        Assert.Single(resultado.Tickets);
        Assert.Equal("TK-2026-0001", resultado.Tickets[0].Codigo);
    }

    [Fact]
    public async Task Eliminar_ConTicketsAsociados_LanzaConflicto()
    {
        _da.Setup(d => d.TieneTicketsAsync(1, It.IsAny<CancellationToken>()))
           .ReturnsAsync(true);

        await Assert.ThrowsAsync<ConflictoException>(() =>
            _flujo.EliminarAsync(1, CancellationToken.None));

        _da.Verify(d => d.EliminarAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Eliminar_SinTickets_EliminaExitosamente()
    {
        _da.Setup(d => d.TieneTicketsAsync(1, It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);
        _da.Setup(d => d.EliminarAsync(1, It.IsAny<CancellationToken>()))
           .ReturnsAsync(true);

        await _flujo.EliminarAsync(1, CancellationToken.None);

        _da.Verify(d => d.EliminarAsync(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Actualizar_Inexistente_LanzaNoEncontrado()
    {
        _da.Setup(d => d.ActualizarAsync(It.IsAny<Agente>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);

        await Assert.ThrowsAsync<NoEncontradoException>(() =>
            _flujo.ActualizarAsync(99, new ActualizarAgenteDtos
            {
                Correo = "nuevo@soporte.com",
                Telefono = "88889999",
                CapacidadMaxima = 5,
                Activo = true
            }, CancellationToken.None));
    }
}
