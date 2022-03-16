using System.Reflection;

namespace Lecture04;

public interface IAlgorithm { }

public class AnAlgorithm : IAlgorithm { }
public class BnAlgorithm : IAlgorithm { }


public class ImplementationAttribute : Attribute
{
    public Type Type { get; }

    public ImplementationAttribute(Type type)
    {
        Type = type;
    }
}

public enum Implementations
{
    [Implementation(typeof(AnAlgorithm))]
    A,

    [Implementation(typeof(BnAlgorithm))]
    B,
}

public static class ImplementationExtensions
{
    public static IAlgorithm GetImplementation(this Implementations value)
    {
        var type = value.GetType();
        var memInfo = type.GetMember(value.ToString());
        var attribute = memInfo[0].GetCustomAttribute<ImplementationAttribute>();
        IAlgorithm algorithm = (IAlgorithm)Activator.CreateInstance(attribute.Type);
        return algorithm;
    }
}

public class ImplementationFromEnum
{
    public static void Show()
    {
        Implementations value = Implementations.A;
        IAlgorithm implementation = value.GetImplementation();
    }
}