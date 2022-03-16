using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
// ReSharper disable SuggestVarOrType_Elsewhere
#pragma warning disable CS8602

namespace Roslyn;

public class SemanticModelProviding
{
    public static void PrintMethodContentViaSemanticModel(SyntaxTree tree)
    {
        var compilation = CSharpCompilation.Create(
            "MethodContent",
            syntaxTrees: new[] { tree },
            references: new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            });

        var model = compilation.GetSemanticModel(tree, true);

        var methods = tree.GetRoot()
            .DescendantNodes(_ => true)
            .OfType<MethodDeclarationSyntax>();

        foreach (var method in methods)
        {
            var methodInfo = model.GetDeclaredSymbol(method);
            var parameters = new List<string>();

            foreach (var parameter in methodInfo.Parameters)
                parameters.Add($"{parameter.Type.Name} {parameter.Name}");

            Console.WriteLine($"{methodInfo.Name}({string.Join(", ", parameters)})");
        }
    }
}