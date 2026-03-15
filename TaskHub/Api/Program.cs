using Api.DI;
using Api.Services.DisposedServices.ScopedServices.FirstScopedService;
using Api.Services.DisposedServices.ScopedServices.SecondScopedService;
using Api.Services.DisposedServices.SingletonServices.FirstSingletonService;
using Api.Services.DisposedServices.SingletonServices.SecondSingletonService;
using Api.Services.DisposedServices.TransientServices.FirstTransientService;
using Api.Services.DisposedServices.TransientServices.SecondTransientService;
using LoggingLibrary;

namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        

        Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build()
            .Run();

        ServiceCollection services = new();

        services.AddSingleton<IFirstSingletonService, FirstSingletonService>();
        services.AddSingleton<ISecondSingletonService, SecondSingletonService>();

        services.AddScoped<IFirstScopedService, FirstScopedService>();
        services.AddScoped<ISecondScopedService, SecondScopedService>();

        services.AddTransient<IFirstTransientService, FirstTransientService>();
        services.AddTransient<ISecondTransientService, SecondTransientService>();

        Console.WriteLine("___First_Scope___");

        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {

            using (IServiceScope firstScope = serviceProvider.CreateScope())
            {
                IServiceProvider provider = firstScope.ServiceProvider;

                provider.TestResolution<IFirstSingletonService>();
                provider.TestResolution<ISecondSingletonService>();

                provider.TestResolution<IFirstScopedService>();
                provider.TestResolution<ISecondScopedService>();

                provider.TestResolution<IFirstTransientService>();
                provider.TestResolution<ISecondTransientService>();
            }

            Console.WriteLine("___Second_Scope___");

            using (IServiceScope secondScope = serviceProvider.CreateScope())
            {
                IServiceProvider provider = secondScope.ServiceProvider;

                provider.TestResolution<IFirstSingletonService>();
                provider.TestResolution<ISecondSingletonService>();

                provider.TestResolution<IFirstScopedService>();
                provider.TestResolution<ISecondScopedService>();

                provider.TestResolution<IFirstTransientService>();
                provider.TestResolution<ISecondTransientService>();
            }
            Console.WriteLine();

        }

    }
}