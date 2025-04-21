namespace Neuca.Domain.Interfaces;

using Neuca.Domain.Services;

public interface IDiscountPolicy
{
    bool TryApplyDiscount(DiscountContext context, out AppliedDiscount? discount);
}
