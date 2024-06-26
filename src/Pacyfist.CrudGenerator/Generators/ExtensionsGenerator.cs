﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Pacyfist.CrudGenerator.Helpers;
using System.Linq;
using System.Text;

namespace Pacyfist.CrudGenerator.Generators
{
    [Generator]
    public class ExtensionsGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var pipeline = context.SyntaxProvider.GetModelProperties();

            context.RegisterSourceOutput(pipeline.Collect(),
                static (ctx, source) =>
                {
                    var baseNamespace = source.FirstOrDefault()?.BaseNamespace ?? "--NAMESPACE--";

                    var services = source
                        .Select(s => $"services.AddTransient<I{s.SingularName}Service, {s.SingularName}Service>();")
                        .AddTabulation(8)
                        .JoinLines(2);

                    ctx.AddSource(
                        "ServiceCollectionExtensions.g.cs",
                        SourceText.From($$"""
                        namespace {{baseNamespace}};

                        using {{baseNamespace}}.Interfaces;
                        using {{baseNamespace}}.Services;

                        public static partial class ServiceCollectionExtensions
                        {
                            public static IServiceCollection RegisterCrudServices(this IServiceCollection services)
                            {
                        {{services}}

                                return services;
                            }
                        }
                        """, Encoding.UTF8));
                });
        }
    }
}