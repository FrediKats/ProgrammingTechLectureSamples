using System.Linq.Expressions;
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable HeapView.ObjectAllocation

namespace Roslyn;

public class SumOverExpression
{
    //public T Sum<T>(T a, T b)
    //{
    //    return a + b;
    //}

    public T Sum<T>(T a, T b)
    {
        var parameterType = Expression.Parameter(typeof(T));
        BinaryExpression add = Expression.Add(parameterType, parameterType);
        Expression<Func<T, T, T>> lambda = Expression
            .Lambda<Func<T, T, T>>(
                add,
                parameterType,
                parameterType);
        return lambda.Compile()(a, b);
    }
}