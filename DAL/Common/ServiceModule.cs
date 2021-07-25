using Autofac;
using Repository.Common;
using Services.Services;

namespace Services.Common
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterGeneric(typeof(ServiceBase<,>)).AsSelf();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
