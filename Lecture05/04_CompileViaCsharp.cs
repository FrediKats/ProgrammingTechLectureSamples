using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
// ReSharper disable ConvertToUsingDeclaration
// ReSharper disable HeapView.ObjectAllocation.Evident
// ReSharper disable SuggestVarOrType_SimpleTypes
#pragma warning disable CS8625
#pragma warning disable CS8602

namespace Roslyn;

public class CompileViaCsharp
{
    public static void Show()
    {
        var code =
            @"
using System;

namespace HelloWorld
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Out.WriteLine(""Hello compiled world"");
    }
  }
}";

        var tree = SyntaxFactory.ParseSyntaxTree(code);

        var compilation = CSharpCompilation.Create(
            "HelloWorldCompiled.exe",
            options: new CSharpCompilationOptions(
                OutputKind.ConsoleApplication),
            syntaxTrees: new[] { tree },
            references: new[]
            {
                MetadataReference.CreateFromFile(
                    typeof(object).Assembly.Location)
            });

        using var stream = new MemoryStream();
        var compileResult = compilation.Emit(stream);
        var assembly = Assembly.Load(stream.GetBuffer());
        assembly.EntryPoint.Invoke(null,
            BindingFlags.NonPublic
                        | BindingFlags.Static,
            null,
            new object[] { null },
            null);
    }
}