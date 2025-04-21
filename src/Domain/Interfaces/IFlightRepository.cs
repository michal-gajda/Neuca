namespace Neuca.Domain.Interfaces;

using Neuca.Domain.Entities;
using Neuca.Domain.Types;

public interface IFlightRepository
{
    Task<FlightEntity?> LoadAsync(FlightId flightId, CancellationToken cancellationToken = default);
    Task SaveAsync(FlightEntity entity, CancellationToken cancellationToken = default);
}
