namespace Neuca.Domain.Types;

public readonly record struct TicketId
{
    public Guid Value { get; private init; }

    public TicketId(Guid value) => this.Value = value;
}
