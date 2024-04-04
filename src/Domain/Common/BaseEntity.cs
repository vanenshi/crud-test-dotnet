namespace Domain.Common;

public abstract class BaseEntity<TKey>: Entity
{
    public TKey Id { get; set; }
}
