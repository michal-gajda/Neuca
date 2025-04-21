namespace Neuca.Domain.Interfaces;

using Neuca.Domain.Types;

public interface ITicketIdProvider
{
    TicketId GetNextId();
}
