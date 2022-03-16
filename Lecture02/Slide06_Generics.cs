using static System.Console;

namespace Lecture02;

public class GenericMethods
{
    public void Swap<T>(ref T a, ref T b)
    {
        (a, b) = (b, a);
    }

    //public void UseAndDispose<T>(T value)
    //{
    //    WriteLine(value.ToString());
    //    value.Dispose();
    //}

    public void UseAndDispose2<T>(T value)
        where T : IDisposable
    {
        WriteLine(value.ToString());
        value.Dispose();
    }
}

public class MyStack<T>
{
    private readonly T[] _data = new T[10];
    private int _index = 0;

    public void Add(T value)
    {
        _data[_index] = value;
        ++_index;
    }

    public T Pop()
    {
        T result = _data[_index];
        _index--;
        return result;
    }
}