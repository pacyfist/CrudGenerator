using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Pacyfist.CrudGenerator.Helpers;
using Scriban;
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

			var modelTemplate = Template.Parse("""
				namespace {{models_namespace}};

				public partial record {{singular_name}}
				{
					required public int Id { get; set; }

					required public Guid {{singular_name}}Id { get; set; }
				}
				""");

			context.RegisterSourceOutput(pipeline,
				(ctx, source) =>
				{
					ctx.AddSource(
						$"Models/{source.SingularName}.g.cs",
						SourceText.From(modelTemplate.Render(source), Encoding.UTF8));
				}
				);

			var modelContext = Template.Parse("""
				namespace {{base_namespace}};
				
				using {{base_namespace}}.Models;
				using Microsoft.EntityFrameworkCore;
				
				public partial class CustomDbContext : DbContext
				{
				{{ for source in sources }}
					public DbSet<{{source.singular_name}}> {{source.plural_name}} { get; set; }
				{{ end }}
				    public CustomDbContext(DbContextOptions<CustomDbContext> options)
				        : base(options)
				    {
				    }
				}
				""");

			context.RegisterSourceOutput(pipeline.Collect(),
				(ctx, sources) =>
				{
					var firstSource = sources.FirstOrDefault();
					var baseNamespace = firstSource?.BaseNamespace ?? "--NAMESPACE--";

					ctx.AddSource(
						"CustomDbContext.g.cs",
						SourceText.From(modelContext.Render(new
						{
							BaseNamespace = baseNamespace,
							Sources = sources
						}), Encoding.UTF8));
				});
		}
	}
}