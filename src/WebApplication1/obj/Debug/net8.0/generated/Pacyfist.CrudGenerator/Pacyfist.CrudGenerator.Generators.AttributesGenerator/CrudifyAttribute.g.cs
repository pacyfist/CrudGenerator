namespace CrudFactory;

[AttributeUsage(AttributeTargets.Class)]
public partial class CrudifyAttribute(string SingularName, string PluralName)
    : Attribute
{
}