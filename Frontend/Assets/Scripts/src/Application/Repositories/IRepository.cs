using TechnicalEvaluation.Domain.Core;

namespace TechnicalEvaluation.Application.Repositories
{
    public interface IRepository<TAggregateRoot, TId>
        where TAggregateRoot : AggregateRoot<TId>
        where TId : ValueObject
    {
    }
}
