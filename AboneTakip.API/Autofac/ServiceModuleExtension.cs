using AboneTakip.Business.Abstract;
using AboneTakip.Business.AutoMapper.Profiles;
using AboneTakip.Business.Concrete;
using AboneTakip.DataAccess.EntitiyFramework.Context;
using Module = Autofac.Module;
using System.Reflection;
using Autofac;
using AboneTakip.Core.DataAccess;

namespace AboneTakip.API.Autofac
{
    public class ServiceModuleExtension : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var apiAssembly = Assembly.GetEntryAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AboneTakipDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(CustomerProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>().As<ICustomerService>();

            base.Load(builder);
        }
    }
}
