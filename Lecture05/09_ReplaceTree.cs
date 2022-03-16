using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Roslyn;

public class ReplaceTree
{
    private static void ModifyTreeViaTree(SyntaxTree tree)
    {
        Console.WriteLine(tree);
        var methods = tree.GetRoot()
            .DescendantNodes(_ => true)
            .OfType<MethodDeclarationSyntax>();

        var newTree = tree.GetRoot().ReplaceNodes(methods, ComputeReplacementNode);
        Console.WriteLine(newTree);

        SyntaxNode ComputeReplacementNode(MethodDeclarationSyntax method, MethodDeclarationSyntax methodWithReplacements)
        {
            var visibilityTokens = method.DescendantTokens(_ => true)
                .Where(_ => _.IsKind(SyntaxKind.PublicKeyword)
                            || _.IsKind(SyntaxKind.PrivateKeyword)
                            || _.IsKind(SyntaxKind.ProtectedKeyword)
                            || _.IsKind(SyntaxKind.InternalKeyword)).ToList();

            if (!visibilityTokens.Any(_ => _.IsKind(SyntaxKind.PublicKeyword)))
            {
                var tokenPosition = 0;

                var newMethod = method.ReplaceTokens(visibilityTokens, (_, __) =>
                {
                    tokenPosition++;

                    return tokenPosition == 1
                        ? SyntaxFactory.Token(_.LeadingTrivia, SyntaxKind.PublicKeyword, _.TrailingTrivia)
                        : new SyntaxToken();
                });
                return newMethod;
            }
            else
            {
                return method;
            }
        }
    }
}

public class Rewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        var visibilityTokens = node.DescendantTokens(_ => true)
            .Where(_ => _.IsKind(SyntaxKind.PublicKeyword) ||
                        _.IsKind(SyntaxKind.PrivateKeyword) ||
                        _.IsKind(SyntaxKind.ProtectedKeyword) ||
                        _.IsKind(SyntaxKind.InternalKeyword)).ToImmutableList();

        if (!visibilityTokens.Any(_ => _.IsKind(SyntaxKind.PublicKeyword)))
        {
            var tokenPosition = 0;

            var newMethod = node.ReplaceTokens(visibilityTokens,
                (_, __) =>
                {
                    tokenPosition++;

                    return tokenPosition == 1 ?
                        SyntaxFactory.Token(
                            _.LeadingTrivia,
                            SyntaxKind.PublicKeyword,
                            _.TrailingTrivia) :
                        new SyntaxToken();
                });
            return newMethod;
        }
        else
        {
            return node;
        }
    }
}