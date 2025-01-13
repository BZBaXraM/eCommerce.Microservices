namespace eCommerce.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.EnableRetryOnFailure());
        });

        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}