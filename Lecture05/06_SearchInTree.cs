using Microsoft.CodeAnalysis.CSharp;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Roslyn;

public class SearchInTree
{
    public static void Show()
    {
        var code = @"
using System;

public class ContainsMethods
{
  public void Method1() { }
  public void Method2(int a, Guid b) { }
  public void Method3(string a) { }
  public void Method4(ref string a) { }
}";

        var tree = SyntaxFactory.ParseSyntaxTree(code);
        int count = tree
            .GetRoot()
            .DescendantNodesAndTokensAndSelf(
                _ => true, true)
            .Count();

        Console.WriteLine(count);
    }
}