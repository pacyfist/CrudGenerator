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

					var ns = GetNamespace(context);
					var attr = context.Attributes.First(a => a.AttributeClass?.Name == "CrudifyAttribute");

					return new ModelDto()
					{
						ModelNamespace = ns,
						BaseNamespace = ExtractBaseNamespace(ns),
						SingularName = attr.ConstructorArguments[0].Value?.ToString(),
						PluralName = attr.ConstructorArguments[1].Value?.ToString()
					};
				});
		}

		private static string GetNamespace(GeneratorAttributeSyntaxContext context)
		{
			var attributeSyntax = context.TargetNode;
			var currentSyntax = attributeSyntax;
			while (currentSyntax != null)
			{
				if (currentSyntax is BaseNamespaceDeclarationSyntax namespaceDeclaration)
				{
					return namespaceDeclaration.Name.ToString();
				}

				currentSyntax = currentSyntax.Parent;
			}

			return "--FAIL--";
		}

		private static string ExtractBaseNamespace(string ns)
		{
			var namespaceParts = ns.Split('.').ToList();

			if (namespaceParts[namespaceParts.Count - 1] is "Model" or "Models")
			{
				namespaceParts.RemoveAt(namespaceParts.Count - 1);
			}

			return string.Join(".", namespaceParts);
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
