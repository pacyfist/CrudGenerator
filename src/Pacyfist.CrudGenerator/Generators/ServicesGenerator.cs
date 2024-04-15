namespace Pacyfist.CrudGenerator.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Pacyfist.CrudGenerator.Helpers;
using System.Text;


[Generator]
public class ServicesGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var pipeline = context.SyntaxProvider.GetModelProperties();

        context.RegisterSourceOutput(pipeline,
            static (ctx, model) =>
            {
                ctx.AddSource(
                    $"I{model.SingularName}Service.g.cs",
                    SourceText.From($$"""
                        namespace {{model.BaseNamespace}}.Interfaces;

                        public partial interface I{{model.SingularName}}Service
                        {
                        }
                        """, Encoding.UTF8));
            });

        context.RegisterSourceOutput(pipeline,
            static (ctx, model) =>
            {
                ctx.AddSource(
                    $"{model.SingularName}Service.g.cs",
                    SourceText.From($$"""
                        namespace {{model.BaseNamespace}}.Services;

                        using {{model.BaseNamespace}}.Interfaces;

                        public partial class {{model.SingularName}}Service : I{{model.SingularName}}Service
                        {
                        }
                        """, Encoding.UTF8));
            });
    }
}
