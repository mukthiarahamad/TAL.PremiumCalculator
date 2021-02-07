using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.ComponentModel;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using TAL.PremiumCalculator.DAL.DataModel;

namespace TAL.PremiumCalculator.WebAPI
{ 
    public class AutofacWebApiConfig
    {
        public static Autofac.IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, Autofac.IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static Autofac.IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<PremiumDBEntities>()
                   .As<DbContext>()
                   .InstancePerLifetimeScope();

            // BLL Services
            builder.RegisterAssemblyTypes(Assembly.Load("TAL.PremiumCalculator.BLL"))
                   .Where(t => t.Name.EndsWith("BLL"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // DAL Services
            builder.RegisterAssemblyTypes(Assembly.Load("TAL.PremiumCalculator.DAL"))
                .Where(t => t.Name.EndsWith("DAL"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            return Container;
        }
    }
}

