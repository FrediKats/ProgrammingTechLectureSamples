using System.Linq.Expressions;

namespace Roslyn;

public class ExpressionVisiting
{
    public static void Show()
    {

    }
}

public class CustomVisitor : ExpressionVisitor
{
    protected override Expression VisitParameter(ParameterExpression node)
    {
        Console.WriteLine(node.Type);
        return base.VisitParameter(node);
    }
}