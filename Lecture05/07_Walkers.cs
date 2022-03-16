using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Roslyn;

public class Walkers
{
    
}

public sealed class MethodWalker
    : CSharpSyntaxWalker
{
    public MethodWalker(SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
        : base(depth)
    { }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        var parameters = new List<string>();

        foreach (var parameter in node.ParameterList.Parameters)
        {
            parameters.Add(
                $"{parameter.Type.ToFullString().Trim()} {parameter.Identifier.Text}");
        }

        Console.Out.WriteLine(
            $"{node.Identifier.Text}({string.Join(", ", parameters)})");

        base.VisitMethodDeclaration(node);
    }
}