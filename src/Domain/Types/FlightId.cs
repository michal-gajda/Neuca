namespace Neuca.Domain.Types;

using System.ComponentModel.DataAnnotations;

public readonly record struct FlightId
{
    [Required(ErrorMessage = "Flight Id cannot be empty")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Flight Id must be exactly 11 characters long")]
    [RegularExpression("(?i)^(?<IATA>[A-Z]{3})(?<Code>[0-9]{5}[A-Z]{3})$", ErrorMessage = "Invalid Flight Id (should match '^[A-Z]{3}[0-9]{5}[A-Z]{3}$')", MatchTimeoutInMilliseconds = 10)]
    public string Value { get; private init; }

    public FlightId(string value)
    {
        var context = new ValidationContext(this)
        {
            MemberName = nameof(Value),
        };

        Validator.ValidateProperty(value, context);

        this.Value = value.ToUpper();
    }
}
