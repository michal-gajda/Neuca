namespace Neuca.Domain.Entities;

using Neuca.Domain.Enums;
using Neuca.Domain.Services;
using Neuca.Domain.Types;

public sealed record class FlightTicketEntity
{
    public TicketId Id { get; private init; }
    public FlightId FlightId { get; private init; }
    public Tenant Tenant { get; private init; }
    public decimal Price { get; private set; } = 0;
    public IReadOnlyList<AppliedDiscount> Discounts => this.discounts.AsReadOnly();

    private readonly List<AppliedDiscount> discounts = [];

    public FlightTicketEntity(TicketId id, FlightId flightId, Tenant tenant)
    {
        this.Id = id;
        this.FlightId = flightId;
        this.Tenant = tenant;
    }

    internal void SetPrice(decimal price)
    {
        this.Price = price;
    }

    internal void ApplyDiscount(AppliedDiscount? discount)
    {
        if (discount is null || this.Tenant.Group is not Group.A)
        {
            return;
        }

        this.discounts.Add(discount);
    }
}
