using AboneTakip.Business.Abstract;
using AboneTakip.Business.Concrete;
using AboneTakip.DataAccess.Abstract;
using AboneTakip.DataAccess.EntitiyFramework.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AboneTakip.API.Extensions
{
    public static class FluentValidationExtension
    {
        public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining(typeof(IValidator));

            return services;
        }
    }
}
