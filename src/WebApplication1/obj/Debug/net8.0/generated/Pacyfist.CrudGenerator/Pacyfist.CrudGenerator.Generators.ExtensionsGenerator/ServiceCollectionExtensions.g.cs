namespace WebApplication1;

using WebApplication1.Interfaces;
using WebApplication1.Services;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCrudServices(this IServiceCollection services)
    {
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}