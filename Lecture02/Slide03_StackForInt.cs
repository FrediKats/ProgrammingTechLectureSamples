namespace Lecture02;

public class Slide03_StackForInt
{
    public static void Show()
    {

        var stack = new Stack();
        stack.Add(1);
        stack.Add(2);
        stack.Add(3);
        object value = stack.Pop();
    }
}