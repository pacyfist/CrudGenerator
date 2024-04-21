namespace Pacyfist.CrudGenerator.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Pacyfist.CrudGenerator.Helpers;
using System.Linq;
using System.Text;

[Generator]
public class ControllersGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var pipeline = context.SyntaxProvider.GetModelProperties();

        context.RegisterSourceOutput(pipeline,
            static (ctx, source) =>
            {
                var baseNamespace = source?.BaseNamespace ?? "--NAMESPACE--";

                ctx.AddSource(
                    $"{source?.SingularName}Controller.g.cs",
                    SourceText.From($$"""
                        using Microsoft.AspNetCore.Mvc;
                        
                        namespace {{baseNamespace}}.Controllers;

                        [ApiController]
                        [Route("[controller]")]
                        public class {{source?.SingularName}}Controller : ControllerBase
                        {
                            private static readonly string[] Summaries = new[]
                            {
                                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                            };
                        
                            private readonly ILogger<{{source?.SingularName}}Controller> _logger;
                        
                            public {{source?.SingularName}}Controller(ILogger<{{source?.SingularName}}Controller> logger)
                            {
                                _logger = logger;
                            }
                        
                            [HttpGet]
                            public IEnumerable<string> Get()
                            {
                                return Summaries;
                            }
                        }
                        """, Encoding.UTF8));
            });
    }
}
