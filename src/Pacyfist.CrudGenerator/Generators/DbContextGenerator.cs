using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Pacyfist.CrudGenerator.Helpers;
using System.Linq;
using System.Text;

namespace Pacyfist.CrudGenerator.Generators
{
    [Generator]
    public class DbContextGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var pipeline = context.SyntaxProvider.GetModelProperties();

            context.RegisterSourceOutput(pipeline.Collect(),
                static (ctx, source) =>
                {
                    var baseNamespace = source.FirstOrDefault()?.BaseNamespace ?? "--NAMESPACE--";

                    var properties = source
                        .Select(s => $"public DbSet<{s.SingularName}> {s.PluralName} {{ get; set; }}")
                        .AddTabulation(4)
                        .JoinLines(2);

                    ctx.AddSource(
                        "CustomDbContext.g.cs",
                        SourceText.From($$"""
                        namespace {{baseNamespace}};

                        using Microsoft.EntityFrameworkCore;
                        using {{baseNamespace}}.Models;

                        public partial class CustomDbContext : DbContext
                        {
                        {{properties}}

                            public CustomDbContext(DbContextOptions<CustomDbContext> options)
                                : base(options)
                            {
                            }
                        }
                        """, Encoding.UTF8));
                });
        }
    }
}