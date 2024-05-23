namespace WebApplication1.Models;

using CrudFactory;

[Crudify(SingularName: "User", PluralName: "Users")]
public partial record User
{
    required public string Name { get; set; }

    required public string Email { get; set; }
}
