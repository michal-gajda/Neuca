namespace Neuca.Domain.Services;

using Neuca.Domain.Interfaces;

public sealed class CustomerBirthdayDiscountPolicy : IDiscountPolicy
{
    private const decimal AMOUNT = 5m;

    public bool TryApplyDiscount(DiscountContext context, out AppliedDiscount? discount)
    {
        var customerBirthday = context.Tenant.DateOfBirth;
        var flightDate = context.Date;

        if (flightDate.Month == customerBirthday.Month && flightDate.Day == customerBirthday.Day)
        {
            discount = new AppliedDiscount("Birthday Discount", AMOUNT, "Flight on customer's birthday");
            context.CurrentPrice -= AMOUNT;

            return true;
        }

        discount = null;

        return false;
    }
}
