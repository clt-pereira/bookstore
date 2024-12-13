using BookStore.Business.Interfaces;
using BookStore.Business.Notificacoes;
using BookStore.Business.Services;
using BookStore.Data.Context;
using BookStore.Data.Repository;

namespace BookStore.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Data
        services.AddScoped<DatabaseContext>();
        services.AddScoped<IAutorRepository, AutorRepository>();
        services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        services.AddScoped<ILivroRepository, LivroRepository>();

        // Business
        services.AddScoped<IAutorService, AutorService>();
        services.AddScoped<IAssuntoService, AssuntoService>();
        services.AddScoped<ILivroService, LivroService>();
        services.AddScoped<INotificador, Notificador>();

        return services;
    }
}
