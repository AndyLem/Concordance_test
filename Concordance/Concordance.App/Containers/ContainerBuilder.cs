using Autofac;
using Concordance.App.Builders;
using Concordance.App.Interfaces;
using Concordance.App.Providers;
using Concordance.Core;

namespace Concordance.App.Containers
{
    public static class ContainerBuilder
    {
        public static IContainer BuildContainer()
        {
            var builder = new Autofac.ContainerBuilder();
            RegisterDependencies(builder);
            return builder.Build();
        }

        private static void RegisterDependencies(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<ConfigProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<WordsCalculator>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<FormattedOutputBuilder>().AsImplementedInterfaces();

            RegisterInputProviders(builder);
        }

        private static void RegisterInputProviders(Autofac.ContainerBuilder builder)
        {
            builder.RegisterInstance(new TextReaderInputProvider(System.Console.In, System.Console.Out)).As<IInputProvider>();
            builder.RegisterType<DemoInputProvider>().As<IInputProvider>();

            builder.RegisterType<InputProviderFactory>().AsImplementedInterfaces();
        }
    }
}