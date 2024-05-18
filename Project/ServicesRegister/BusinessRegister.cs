using Business.Interfaces;
using Business.Services;

namespace Project.ServicesRegister
{
    public static class BusinessRegister
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ITestService, TestService>();

            return services;
        }
    }
}
