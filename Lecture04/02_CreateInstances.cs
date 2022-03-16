using System.Runtime.Serialization;

namespace Lecture04;

class ObjectWithCtor
{
    private readonly int _value;

    public static void Show()
    {
        new ObjectWithCtor().Write();
        var typeless = FormatterServices
            .GetUninitializedObject(typeof(ObjectWithCtor));
        var value = (ObjectWithCtor)typeless;
        value.Write();
    }

    public ObjectWithCtor() => _value = 10;
    public bool Ok() => _value == 10;
    public void Write() => Console.WriteLine(_value);
}