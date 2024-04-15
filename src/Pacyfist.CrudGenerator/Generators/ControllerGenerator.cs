//namespace Pacyfist.CrudGenerator.Generators;

//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.Text;
//using System.Text;

//[Generator]
//public class ControllerGenerator : IIncrementalGenerator
//{
//    public void Initialize(IncrementalGeneratorInitializationContext context)
//    {
//        context.RegisterPostInitializationOutput(static postInitializationContext =>
//        {
//            postInitializationContext.AddSource("Controllers/BestController.cs", SourceText.From("""
//                using Microsoft.AspNetCore.Mvc;
                
//                namespace WebApplication1.Controllers
//                {
//                    [ApiController]
//                    [Route("[controller]")]
//                    public class BestController : ControllerBase
//                    {
//                        private static readonly string[] Summaries = new[]
//                        {
//                            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//                        };
                
//                        private readonly ILogger<BestController> _logger;
                
//                        public BestController(ILogger<BestController> logger)
//                        {
//                            _logger = logger;
//                        }
                
//                        [HttpGet]
//                        public IEnumerable<WeatherForecast> Get()
//                        {
//                            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//                            {
//                                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                                TemperatureC = Random.Shared.Next(-20, 55),
//                                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//                            })
//                            .ToArray();
//                        }
//                    }
//                }
//                """, Encoding.UTF8));
//        });

//        context.RegisterPostInitializationOutput(static postInitializationContext =>
//        {
//            postInitializationContext.AddSource("Utilities/RegisterServices.cs", SourceText.From("""
//                namespace WebApplication1
//                {
//                    public static class MvcServiceCollectionExtensions
//                    {
//                        public static IServiceCollection CreateServicesFromModel(this IServiceCollection services)
//                        {
//                            return services;
//                        }
//                    }
                        
//                }
//                """, Encoding.UTF8));
//        });
//    }
//}
