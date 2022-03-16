using System.Reflection;

namespace Lecture04;

public class CallOverReflection
{
    public static void Show()
    {
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

        var callOverReflection = new CallOverReflection();
        Type currentType = callOverReflection.GetType();
        MethodInfo methodInfo = currentType.GetMethod(
            "PrivateMethod",
            bindingFlags)!;
        methodInfo.Invoke(callOverReflection, Array.Empty<object?>());
    }

    private void PrivateMethod()
    {
        Console.WriteLine($"From reflection {GetType()}");
    }
}