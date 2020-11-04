using System;

namespace HomeBudget.Core
{
    public interface IEntity
    {
        int? Id { get; }
        DateTime Created { get; }
        DateTime Modified { get; }
        int AuthorId { get; }
    }
}
