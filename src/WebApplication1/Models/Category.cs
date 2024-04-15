namespace WebApplication1.Models;

using CrudFactory;

[Crudify(SingularName: "Category", PluralName: "Categories")]
public partial record Category
{
    required public Guid Id { get; set; }

    required public string Name { get; set; }

    required public string Email { get; set; }
}
