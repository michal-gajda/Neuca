namespace Neuca.Domain.Types;

using Neuca.Domain.Enums;

public readonly record struct Tenant
{
    public Guid Value { get; private init; }
    public Group Group { get; private init; }
    public DateOnly DateOfBirth { get; private init; }

    public Tenant(Guid value, Group group, DateOnly dateOfBirth) => (this.Value, this.Group, this.DateOfBirth) = (value, group, dateOfBirth);
}
