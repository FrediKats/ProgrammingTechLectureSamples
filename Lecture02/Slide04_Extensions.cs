

namespace Lecture02;

public class Slide04_Extensions
{
    
}

public enum Student
{
    Ru,
    Other
}

public static class StudentExtensions
{
    public static string Hello(this Student student)
    {
        return student switch
        {
            Student.Ru => "Hello",
            Student.Other => "Соболезную",
        };
    }

    public static void F()
    {
        var s = "qwer";

        Console.WriteLine(Student.Ru.Hello());
    }
}