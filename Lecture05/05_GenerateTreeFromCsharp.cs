using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Roslyn;

public class GenerateTreeFromCsharp
{
    public static void Show()
    {
        var treeNamespace = SyntaxFactory.NamespaceDeclaration(
            SyntaxFactory.IdentifierName("BuildingTrees"));

        var doublerClass = SyntaxFactory.ClassDeclaration("Doubler");

        var doubleMethod = doublerClass.WithMembers(
            SyntaxFactory.SingletonList<MemberDeclarationSyntax>(
                SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.PredefinedType(
                        SyntaxFactory.Token(
                            SyntaxKind.IntKeyword)),
                    SyntaxFactory.Identifier("Double"))));

    }
}

public static class Doubler
{
    public static int Double(int a)
    {
        return 2 * a;
    }
}
