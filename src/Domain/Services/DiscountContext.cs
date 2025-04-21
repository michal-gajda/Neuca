namespace Neuca.Domain.Services;

using Neuca.Domain.Entities;
using Neuca.Domain.Types;

public sealed class DiscountContext
{
    public decimal CurrentPrice { get; set; }
    public decimal MinimumPrice => this.Flight.MinimumPrice;
    public FlightEntity Flight { get; private init; }
    public Tenant Tenant { get; private init; }
    public DateOnly Date { get; private init; }

    public DiscountContext(decimal basePrice, DateOnly date, FlightEntity flight, Tenant tenant)
    {
        this.CurrentPrice = basePrice;
        this.Date = date;
        this.Flight = flight;
        this.Tenant = tenant;
    }
}
