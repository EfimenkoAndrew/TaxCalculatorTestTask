using System.Reflection;

namespace TestTask.Core.Common;

/// <summary>
/// See https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
/// </summary>
public abstract class Enumeration<TEnumeration, TValue> : IEnumeration, IComparable<TEnumeration>, IEquatable<TEnumeration>
    where TEnumeration : Enumeration<TEnumeration, TValue>
    where TValue : IComparable
{
    private static readonly TEnumeration[] s_enumerations = GetEnumerations();

    protected Enumeration(TValue id, string name)
    {
        Id = id;
        Name = name;
    }

    public TValue Id { get; private set; }

    public string Name { get; private set; }

    public static TEnumeration[] GetAll()
    {
        return s_enumerations;
    }

    public static TEnumeration FromValue(TValue value)
    {
        return Parse(value, "value", item => item.Id.Equals(value));
    }

    public static TEnumeration Parse(string? displayName)
    {
        return Parse(displayName, "display name",
            item => item.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase));
    }

    public static bool TryParse(TValue value, out TEnumeration? result)
    {
        return TryParse(x => x.Id.Equals(value), out result);
    }

    public static bool TryParse(string displayName, out TEnumeration? result)
    {
        return TryParse(x => x.Name.Equals(displayName, StringComparison.OrdinalIgnoreCase), out result);
    }

    public int CompareTo(TEnumeration? other)
    {
        return other == null ? 1 : Id.CompareTo(other.Id);
    }

    public sealed override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        return Equals((TEnumeration?)obj);
    }

    public bool Equals(TEnumeration? other)
    {
        return other != null && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public static Enumeration<TEnumeration, TValue>? ToBaseEnumeration(TValue value)
    {
        return FromValue(value);
    }

    public TValue ToTValue()
    {
        return Id;
    }

    public static explicit operator Enumeration<TEnumeration, TValue>(TValue value)
    {
        return FromValue(value);
    }

    public static implicit operator TValue(Enumeration<TEnumeration, TValue> enumeration)
    {
        return enumeration is null ? throw new ArgumentNullException(nameof(enumeration)) : enumeration.Id;
    }

    public static implicit operator string(Enumeration<TEnumeration, TValue> enumeration)
    {
        return enumeration.Name;
    }

    public static bool operator ==(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right)
    {
        return !(left == right);
    }

    public static bool operator <(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right)
    {
        return left is null ? right is not null : left.CompareTo((TEnumeration?)right) < 0;
    }

    public static bool operator <=(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right)
    {
        return left is null || left.CompareTo((TEnumeration?)right) <= 0;
    }

    public static bool operator >(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right)
    {
        return left is not null && left.CompareTo((TEnumeration?)right) > 0;
    }

    public static bool operator >=(Enumeration<TEnumeration, TValue>? left, Enumeration<TEnumeration, TValue>? right)
    {
        return left is null ? right is null : left.CompareTo((TEnumeration?)right) >= 0;
    }

    private static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration? result)
    {
        result = GetAll().FirstOrDefault(predicate);
        return result != null;
    }

    private static TEnumeration[] GetEnumerations()
    {
        var enumerationType = typeof(TEnumeration);
        return enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
            .Select(info => info.GetValue(null))
            .Cast<TEnumeration>()
            .ToArray();
    }

    private static TEnumeration Parse(object? value, string description, Func<TEnumeration, bool> predicate)
    {
        if (!TryParse(predicate, out var result))
        {
            var message = $"'{value}' is not a valid {description} in {typeof(TEnumeration)}";
            throw new ArgumentException(message, nameof(value));
        }

        return result!;
    }
}

public interface IEnumeration
{
}
