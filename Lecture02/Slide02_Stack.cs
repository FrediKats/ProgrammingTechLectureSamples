namespace Lecture02;

public class Slide02_Stack
{
    public static void Show()
    {
        var stringStack = new Stack();
        stringStack.Add("q");
        stringStack.Add("w");
        stringStack.Add("e");
    }
}

public class Stack
{
    private readonly object[] _data = new object[10];
    private int _index = 0;

    public void Add(object value)
    {
        _data[_index] = value;
        ++_index;
    }

    public object Pop()
    {
        _index--;
        object result = _data[_index];
        return result;
    }
}