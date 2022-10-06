using Autofac;
using instaapp_backend.Core.IConfiguration;
using mf_backend.Data;

namespace mf_backend.Infrastructure.AutofacModules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Other Lifetime
            // Transient
            //builder.RegisterType<OneTimePassword>().As<IOneTimePassword>()
            //    .InstancePerDependency();

            //// Scoped
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();


            //// Singleton
            //builder.RegisterType<EmployeeService>().As<IEmployeeService>()
            //    .SingleInstance();

        }
    }
}
