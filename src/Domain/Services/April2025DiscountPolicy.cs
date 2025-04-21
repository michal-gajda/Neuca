namespace Neuca.Domain.Services;

using Neuca.Domain.Interfaces;

public sealed class April2025DiscountPolicy : IDiscountPolicy
{
    private const decimal AMOUNT = 5m;
    private const int APRIL = 4;
    private const int YEAR = 2025;

    public bool TryApplyDiscount(DiscountContext context, out AppliedDiscount? discount)
    {
        var flightDate = context.Date;

        if (flightDate is { Month: APRIL, Year: YEAR })
        {
            discount = new AppliedDiscount("April 2025 Discount", AMOUNT, "Flight on April 2025");
            context.CurrentPrice -= AMOUNT;

            return true;
        }

        discount = null;

        return false;
    }
}