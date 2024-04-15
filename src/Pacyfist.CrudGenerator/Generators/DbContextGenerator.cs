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

                    var services = source
                        .Select(s => $"services.AddTransient<I{s.SingularName}Service, {s.SingularName}Service>();")
                        .JoinWithTabulation("        ");

                    ctx.AddSource(
                        "CustomDbContext.g.cs",
                        SourceText.From($$"""
                        namespace {{baseNamespace}};

                        using Microsoft.EntityFrameworkCore;
                        using WebApplication1.Models;


                        public partial class CustomDbContext : DbContext
                        {
                            public DbSet<User> Users { get; set; }

                            public DbSet<Category> Categories { get; set; }

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