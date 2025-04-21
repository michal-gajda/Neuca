namespace Neuca.Domain.Interfaces;

using Neuca.Domain.Entities;

public interface IFlightTicketRepository
{
    Task SaveAsync(FlightTicketEntity entity, CancellationToken cancellationToken = default);
}
