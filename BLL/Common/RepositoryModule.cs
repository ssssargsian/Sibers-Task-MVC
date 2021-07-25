
using Autofac;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Repositories;

namespace Repository.Common
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<SibersContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(SibersContext)).AsSelf().As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<EmployeeRepository>().As<IRepositoryBase<Employee>>().InstancePerLifetimeScope();
            builder.RegisterType<ProjectRepository>().As<IRepositoryBase<Project>>().InstancePerLifetimeScope();
            
            base.Load(builder);
        }
    }
}
