namespace Neuca.Domain.Exceptions;

using Neuca.Domain.Types;

public sealed class FlightIdNotFoundException(FlightId flightId) : Exception($"Flight with Id '{flightId.Value}' not found")
{
}
