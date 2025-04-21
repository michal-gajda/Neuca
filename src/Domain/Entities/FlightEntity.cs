namespace Neuca.Domain.Entities;

using Neuca.Domain.Types;

public sealed record class FlightEntity
{
    public FlightId Id { get; private init; }
    public string From { get; private init; }
    public string To { get; private init; }
    public TimeOnly Time { get; private init; }
    public decimal BasePrice { get; private init; }
    public decimal MinimumPrice { get; private init; }
    public DayOfWeek[] Days { get; private init; }

    public FlightEntity(FlightId id, decimal basePrice, decimal minimumPrice, string from, string to, TimeOnly time, DayOfWeek[] days)
    {
        this.Id = id;
        this.BasePrice = basePrice;
        this.MinimumPrice = minimumPrice;
        this.From = from;
        this.To = to;
        this.Time = time;
        this.Days = days;
    }
}
