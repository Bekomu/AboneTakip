using AboneTakip.Business.Abstract;
using AboneTakip.Business.Concrete;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DataAccess.EntitiyFramework.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AboneTakip.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IReadingRepository, ReadingRepository>();
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IReadingService, ReadingService>();
        }

        public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining(typeof(IValidator));

            return services;
        }
    }
}
