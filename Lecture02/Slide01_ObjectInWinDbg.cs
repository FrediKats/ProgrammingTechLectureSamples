namespace Lecture02;

public class Slide01_ObjectInWinDbg
{
    public static void Show()
    {
        var justObject = new object();
        var boxed = (object)1;
    }
}