﻿using System;
using ConventionalRegistration.Configuration;
using ConventionalRegistration.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using TestAssembly;
using TestAssembly.Data;

namespace ConventionalRegistration.SimpleInjector.Tests
{
    [TestClass]
    public class ConventionRegistrationTests : InstanceResolverTests
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            var config = BehaviorConfiguration.Default;
            config.Log = (mode, message) =>
            {
                if (mode == DiagnosticLevel.Warning)
                {
                    TestContext.WriteLine(message);
                }
            };
            config.ExcludedServices = new[] { typeof(ICommand), typeof(IEntity), typeof(IQuery<>) };
            Container.RegistrationByConvention(config, builder =>
            {
                builder.ExplicitRegistration(c =>
                {
                    c.Register(() => new CustomerLocationValidator { Name = "abc" });
                    c.RegisterSingleton<ISingletonService, SingletonService>();
                    c.Register(() => new DbConnection("Datasource=source"), Lifestyle.Scoped);
                    c.Register<IDataProvider>(() => new XmlDataProvider("drive:\\path"));
                    c.Register<DbContext1>(Lifestyle.Scoped);
                    c.Register<DbContext2>(Lifestyle.Scoped);
                    c.RegisterMultipleServices(new[] { typeof(IFileRead), typeof(IFileWrite) }, typeof(FileOperation), Lifestyle.Singleton);
                    c.RegisterSingleton<Func<ITransientService>>(() => Container.GetInstance<ITransientService>());

                    c.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionCommandHandler<>));
                    c.RegisterDecorator(typeof(ICommandHandler<>), typeof(LoggerCommandHandler<>));
                });

                builder.RegisterAsCollection(typeof(IValidator<>));
                builder.RegisterAsCollection(typeof(ICommandValidator<>));

                builder.Define(source => source
                     .FromAssemblyContaining<ITransientService>()
                     .SelectAllClasses()
                     .WithoutAttribute<ExcludeFromRegistrationAttribute>()
                     .Excluding(typeof(TransientService2), typeof(Repository<>), typeof(XmlDataProvider))
                     .UsingAllInterfacesStrategy());
            });
            Container.RegisterConditional(typeof(IRepository<>), typeof(Repository<>), context => !context.Handled);
            Container.Verify();
        }
    }
}
