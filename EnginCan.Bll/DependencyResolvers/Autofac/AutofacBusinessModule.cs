using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using EnginCan.Bll.EntityCore.Abstract.Abouts;
using EnginCan.Bll.EntityCore.Abstract.Systems;
using EnginCan.Bll.EntityCore.Abstract.Users;
using EnginCan.Bll.EntityCore.Concrete.Abouts;
using EnginCan.Bll.EntityCore.Concrete.Systems;
using EnginCan.Bll.EntityCore.Concrete.Users;
using EnginCan.Core.Helpers.Interceptors;
using EnginCan.Core.Middleware;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using EnginCan.Dal.EfCore;

namespace EnginCan.Bll.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(componentContext =>
            {
                var serviceProvider = componentContext.Resolve<IServiceProvider>();
                var configuration = componentContext.Resolve<IConfiguration>();
                var dbContextOptions = new DbContextOptions<EnginCanContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<EnginCanContext>(dbContextOptions)
                    .UseApplicationServiceProvider(serviceProvider)
                    .UseSqlServer(configuration.GetConnectionString("Default"),
                        serverOptions => serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null));
                return optionsBuilder.Options;
            }).As<DbContextOptions<EnginCanContext>>()
        .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<EnginCanContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EnginCanContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            #region Systems
            builder.RegisterType<CustomHttpContextAccessor>().As<ICustomHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<LookupRepository>().As<ILookupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LookupTypeRepository>().As<ILookupTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PageRepository>().As<IPageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PagePermissionRepository>().As<IPagePermissionRepository>().InstancePerLifetimeScope();
            #endregion

            #region User
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleRepository>().As<IUserRoleRepository>().InstancePerLifetimeScope();
            #endregion

            #region Abouts
            builder.RegisterType<AboutRepository>().As<IAboutRepository>().SingleInstance();
            #endregion

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerLifetimeScope();
        }
    }
}
