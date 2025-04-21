namespace Neuca.Domain.UnitTests;

using Neuca.Domain.Entities;
using Neuca.Domain.Enums;
using Neuca.Domain.Exceptions;
using Neuca.Domain.Interfaces;
using Neuca.Domain.Services;
using Neuca.Domain.Types;

[TestClass]
public sealed class TicketServiceTests
{
    private readonly IFlightRepository flightRepository = Substitute.For<IFlightRepository>();
    private readonly IFlightTicketRepository flightTicketRepository = Substitute.For<IFlightTicketRepository>();
    private readonly ITicketIdProvider ticketIdProvider = Substitute.For<ITicketIdProvider>();
    private readonly List<IDiscountPolicy> discountPolicies = new();

    public TicketServiceTests()
    {
        var flightId = new FlightId("KLM12345BCA");
        var flightEntity = new FlightEntity(flightId, basePrice: 30, minimumPrice: 20, from: "Europe", to: "Africe", time: new TimeOnly(0, 0, 0), [DayOfWeek.Monday, DayOfWeek.Thursday,]);

        this.flightRepository.LoadAsync(flightId, Arg.Any<CancellationToken>()).Returns(flightEntity);

        this.ticketIdProvider.GetNextId().Returns(new TicketId(Guid.NewGuid()));

        this.discountPolicies.Add(new AfricaThursdayDiscountPolicy());
        this.discountPolicies.Add(new CustomerBirthdayDiscountPolicy());
    }

    [TestMethod]
    public async Task BuyTicketAsync_Should_Throw_FlightId_Not_Found_Exception()
    {
        // Given
        var ticketService = new TicketService(this.discountPolicies, this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.A, new DateOnly(1978, 12, 20));
        var flightId = new FlightId("KLM12345BCB");

        // When
        var sut = async () => await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(1978, 12, 20), CancellationToken.None);

        // Then
        await sut.ShouldThrowAsync<FlightIdNotFoundException>();
    }

    [TestMethod]
    public async Task BuyTicketAsync_Should_Throw_Unavailable_Flight_Day_Exception()
    {
        // Given
        var ticketService = new TicketService(this.discountPolicies, this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.A, new DateOnly(1978, 12, 20));
        var flightId = new FlightId("KLM12345BCA");

        // When
        var sut = async () => await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(1978, 12, 20), CancellationToken.None);

        // Then
        await sut.ShouldThrowAsync<UnavailableFlightDayException>();
    }

    [TestMethod]
    public async Task BuyTicketAsync_Should_SaveAsync_FlightTicketEntity()
    {
        // Given
        var ticketService = new TicketService([], this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.A, new DateOnly(1978, 12, 20));
        var flightId = new FlightId("KLM12345BCA");

        // When
        await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(2025, 4, 21), CancellationToken.None);

        // Then

        await flightTicketRepository
            .Received()
            .SaveAsync(Arg.Is<FlightTicketEntity>(e => e.FlightId == flightId && e.Price == 30 && e.Discounts.Any() == false), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task BuyTicketAsync_Should_SaveAsync_FlightTicketEntity_With_One_Discount()
    {
        // Given
        var ticketService = new TicketService([new AfricaThursdayDiscountPolicy()], this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.A, new DateOnly(1978, 12, 20));
        var flightId = new FlightId("KLM12345BCA");

        // When
        await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(2025, 4, 24), CancellationToken.None);

        // Then

        await flightTicketRepository
            .Received()
            .SaveAsync(Arg.Is<FlightTicketEntity>(e => e.FlightId == flightId && e.Price == 25 && e.Discounts.Count() == 1), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task BuyTicketAsync_Should_SaveAsync_FlightTicketEntity_With_Two_Discounts()
    {
        // Given
        var ticketService = new TicketService(this.discountPolicies, this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.A, new DateOnly(1978, 4, 24));
        var flightId = new FlightId("KLM12345BCA");

        // When
        await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(2025, 4, 24), CancellationToken.None);

        // Then

        await flightTicketRepository
            .Received()
            .SaveAsync(Arg.Is<FlightTicketEntity>(e => e.FlightId == flightId && e.Price == 20 && e.Discounts.Count() == 2), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task BuyTicketAsync_Should_SaveAsync_FlightTicketEntity_With_Only_Two_Discounts()
    {
        // Given
        var ticketService = new TicketService(this.discountPolicies.Append(new April2025DiscountPolicy()), this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.A, new DateOnly(1978, 4, 24));
        var flightId = new FlightId("KLM12345BCA");

        // When
        await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(2025, 4, 24), CancellationToken.None);

        // Then

        await flightTicketRepository
            .Received()
            .SaveAsync(Arg.Is<FlightTicketEntity>(e => e.FlightId == flightId && e.Price == 20 && e.Discounts.Count() == 2), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task BuyTicketAsync_By_Tenant_Group_B_Should_SaveAsync_FlightTicketEntity_With_Only_Two_Discounts()
    {
        // Given
        var ticketService = new TicketService(this.discountPolicies.Append(new April2025DiscountPolicy()), this.flightRepository, this.flightTicketRepository, this.ticketIdProvider);
        var tenant = new Tenant(Guid.NewGuid(), Group.B, new DateOnly(1978, 4, 24));
        var flightId = new FlightId("KLM12345BCA");

        // When
        await ticketService.BuyTicketAsync(tenant, flightId, new DateOnly(2025, 4, 24), CancellationToken.None);

        // Then

        await flightTicketRepository
            .Received()
            .SaveAsync(Arg.Is<FlightTicketEntity>(e => e.FlightId == flightId && e.Price == 20 && e.Discounts.Any() == false), Arg.Any<CancellationToken>());
    }
}
