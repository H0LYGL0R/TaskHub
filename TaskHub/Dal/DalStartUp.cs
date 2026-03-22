using Dal.Context;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using DatabaseLibrary;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

/// <summary>
/// Регистрация зависимостей слоя доступа к данным
/// </summary>
public static class DalStartUp
{
    /// <summary>
    /// Добавить зависимости DAL: DbContext и репозитории
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public static void AddDal(this IServiceCollection services)
    {
        services.AddDatabase<UserDbContext>();
        services.AddDatabase<TaskDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}
