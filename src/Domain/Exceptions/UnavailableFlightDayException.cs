namespace Neuca.Domain.Exceptions;

using Neuca.Domain.Types;

public sealed class UnavailableFlightDayException(FlightId flightId, DayOfWeek dayOfWeek) : Exception($"Flight with Id '{flightId.Value}' unavailable on '{dayOfWeek}'")
{
}
