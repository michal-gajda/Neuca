namespace Neuca.Domain.Services;

using Neuca.Domain.Entities;
using Neuca.Domain.Exceptions;
using Neuca.Domain.Interfaces;
using Neuca.Domain.Types;

public sealed class TicketService : ITicketService
{
    private readonly IEnumerable<IDiscountPolicy> discountPolicies;
    private readonly IFlightRepository flightRepository;
    private readonly IFlightTicketRepository flightTicketRepository;
    private readonly ITicketIdProvider ticketIdProvider;

    public TicketService(IEnumerable<IDiscountPolicy> discountPolicies, IFlightRepository flightRepository, IFlightTicketRepository flightTicketRepository, ITicketIdProvider ticketIdProvider)
    {
        this.discountPolicies = discountPolicies;
        this.flightRepository = flightRepository;
        this.flightTicketRepository = flightTicketRepository;
        this.ticketIdProvider = ticketIdProvider;
    }

    public async Task BuyTicketAsync(Tenant tenant, FlightId flightId, DateOnly date, CancellationToken cancellationToken = default)
    {
        var flight = await this.flightRepository.LoadAsync(flightId, cancellationToken);

        if (flight is null)
        {
            throw new FlightIdNotFoundException(flightId);
        }

        var dayOfWeek = date.DayOfWeek;

        if (flight.Days.ToList().Contains(dayOfWeek) is false)
        {
            throw new UnavailableFlightDayException(flightId, dayOfWeek);
        }

        var ticketId = this.ticketIdProvider.GetNextId();

        var ticket = new FlightTicketEntity(ticketId, flightId, tenant);

        ticket.SetPrice(flight.BasePrice);

        var context = new DiscountContext(flight.BasePrice, date, flight, tenant);

        foreach (var policy in this.discountPolicies)
        {
            if (policy.TryApplyDiscount(context, out var discount))
            {
                if (context.CurrentPrice < context.MinimumPrice)
                {
                    break;
                }

                ticket.SetPrice(context.CurrentPrice);
                ticket.ApplyDiscount(discount);
            }
        }

        await this.flightTicketRepository.SaveAsync(ticket, cancellationToken);
    }
}
