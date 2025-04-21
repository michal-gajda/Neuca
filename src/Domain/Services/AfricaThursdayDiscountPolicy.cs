namespace Neuca.Domain.Services;

using Neuca.Domain.Interfaces;

public sealed class AfricaThursdayDiscountPolicy : IDiscountPolicy
{
    private const decimal AMOUNT = 5m;
    private const string CONTINENT = "Africe";
    private const DayOfWeek DAY_OF_WEEK = DayOfWeek.Thursday;

    public bool TryApplyDiscount(DiscountContext context, out AppliedDiscount? discount)
    {
        var dayOfWeek = context.Date.DayOfWeek;

        if (context.Flight.To.Equals(CONTINENT, StringComparison.OrdinalIgnoreCase) && dayOfWeek is DAY_OF_WEEK)
        {
            discount = new AppliedDiscount($"Africe Thursday Discount", AMOUNT, $"Applies to flights to {CONTINENT} on {DAY_OF_WEEK}s");
            context.CurrentPrice -= AMOUNT;

            return true;
        }

        discount = null;

        return false;
    }
}
