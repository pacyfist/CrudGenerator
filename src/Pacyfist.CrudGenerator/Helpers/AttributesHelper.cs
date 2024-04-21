using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pacyfist.CrudGenerator.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Pacyfist.CrudGenerator.Helpers
{
    public static class AttributesHelper
    {
        public static IncrementalValuesProvider<ModelDto> GetModelProperties(this SyntaxValueProvider provider)
        {
            return provider.ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: "CrudFactory.CrudifyAttribute",
                predicate: static (syntaxNode, cancellationToken) => true,
                transform: static (context, cancellationToken) =>
                {
                    var attr = context.Attributes.First(a => a.AttributeClass?.Name == "CrudifyAttribute");

                    return new ModelDto()
                    {
                        BaseNamespace = GetBaseNamespace(context),
                        SingularName = attr.ConstructorArguments[0].Value?.ToString(),
                        PluralName = attr.ConstructorArguments[1].Value?.ToString()
                    };
                });
        }

        private static string GetBaseNamespace(GeneratorAttributeSyntaxContext context)
        {
            var attributeSyntax = context.TargetNode;
            SyntaxNode? current = attributeSyntax;
            while (current != null)
            {
                if (current is BaseNamespaceDeclarationSyntax namespaceDeclaration)
                {
                    var namespaceParts = namespaceDeclaration.Name.ToString().Split('.').ToList();

                    if (namespaceParts[namespaceParts.Count - 1] == "Model" ||
                        namespaceParts[namespaceParts.Count - 1] == "Models")
                    {
                        namespaceParts.RemoveAt(namespaceParts.Count - 1);
                    }

                    return string.Join(".", namespaceParts);
                }

                current = current.Parent;
            }

            return "--FAIL--";
        }

        public static IEnumerable<string> AddTabulation<T>(this IEnumerable<T> enumerable, int count = 0)
        {
            string tabulation = new string(' ', count);
            return enumerable.Select(e => $"{tabulation}{e}");
        }

        public static string JoinLines(this IEnumerable<string> enumerable, int count = 1)
        {
            const string newline = """


                """;

            if (count == 1)
            {
                return string.Join(newline, enumerable);
            }
            else
            {
                string newlines = string.Concat(Enumerable.Repeat(newline, count));

                return string.Join(newlines, enumerable);
            }
        }
    }
}
