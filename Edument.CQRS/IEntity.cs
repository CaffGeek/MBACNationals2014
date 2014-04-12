using System;

namespace Edument.CQRS
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
