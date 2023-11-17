using Challenge.Domain.Common;
using System.Threading.Tasks;

namespace Challenge.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
