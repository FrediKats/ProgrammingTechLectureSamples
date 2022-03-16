namespace Roslyn;

public class ExpressionToSql
{
    public static void Show()
    {
        IQueryable<Student> query = GetDbQuery();
        query = query.Where(s => s.Id > 200);

        string sqlQuery = @"
SELECT *
FROM [dbo].[Students]
WHERE Id > 200

";
    }

    private static IQueryable<Student> GetDbQuery()
    {
        return new EnumerableQuery<Student>(new List<Student>());
    }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}