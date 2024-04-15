namespace Pacyfist.CrudGenerator.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

[Generator]
public class AttributesGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
        {
            ctx.AddSource(
                "CrudifyAttribute.g.cs",
                SourceText.From("""
                    namespace CrudFactory;

                    [AttributeUsage(AttributeTargets.Class)]
                    public partial class CrudifyAttribute(string SingularName, string PluralName)
                        : Attribute
                    {
                    }
                    """, Encoding.UTF8));
        });
    }
}
