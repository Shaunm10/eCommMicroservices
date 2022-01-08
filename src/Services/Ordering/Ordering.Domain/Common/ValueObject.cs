namespace Ordering.Domain.Common;

/// <summary>
/// Base for all Value Objects entities. Value Objects should be Immutable and not have an Identifier.
/// <see cref="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects"/>
/// </summary>
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        // if just 1 of the objects are NULL
        if (left is null ^ right is null)
        {
            // than return false, no more comparison is needed.
            return false;
        }

        // than do the 'equal' comparison.
        return left?.Equals(right) != false;
    }

    /// <summary>
    /// If the objects are Not Equal
    /// </summary>
    /// <param name="left">left object in comparison</param>
    /// <param name="right">right object in comparison</param>
    /// <returns>If they are the same.</returns>
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != this.GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return this.GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}