using BitEx.IRepository.User;
using BitEx.Repository.User;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BitEx.Repository
{
    public static class Extensions
    {
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUserVipRepository, UserVipRepository>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            serviceCollection.AddSingleton<IPointsRepository, PointsRepository>();
        }
        public static void Init(IServiceProvider provider)
        {
            IocProvider = provider;
        }
        public static IServiceProvider IocProvider { get; set; }
    }
}
