namespace Neuca.Application;

using Microsoft.Extensions.DependencyInjection;
using Neuca.Domain.Interfaces;
using Neuca.Domain.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ITicketService>(sp =>
        {
            var discountPolicies = new List<IDiscountPolicy>
            {
                new AfricaThursdayDiscountPolicy(),
                new CustomerBirthdayDiscountPolicy(),
            };

            var flightRepository = sp.GetRequiredService<IFlightRepository>();
            var flightTicketRepository = sp.GetRequiredService<IFlightTicketRepository>();
            var ticketIdProvider = sp.GetRequiredService<ITicketIdProvider>();

            return new TicketService(discountPolicies, flightRepository, flightTicketRepository, ticketIdProvider);
        });

        return services;
    }
}
