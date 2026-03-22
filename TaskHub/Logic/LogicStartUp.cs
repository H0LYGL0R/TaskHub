using Logic.Users.Services;
using Logic.Users.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Api.Services.TaskService;

namespace Logic;

/// <summary>
/// Регистрация зависимостей слоя логики
/// </summary>
public static class LogicStartUp
{
    /// <summary>
    /// Добавить зависимости логики: сервисы
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();
    }
}