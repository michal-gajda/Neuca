namespace Neuca.Domain.Interfaces;

using Neuca.Domain.Types;

public interface ITicketService
{
    Task BuyTicketAsync(Tenant tenant, FlightId flightId, DateOnly date, CancellationToken cancellationToken = default);
}
