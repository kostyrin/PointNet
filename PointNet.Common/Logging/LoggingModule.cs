using System;
using System.Linq;
using System.Reflection;
using Autofac.Core;
using log4net;

namespace PointNet.Common.Logging
{
    public class LoggingModule : Autofac.Module
    {
        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.Register((c, p) => GetLogger(p.TypedAs<Type>()));
        //}

        //protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        //{
        //    registration.Preparing +=
        //        (sender, args) =>
        //        {
        //            var forType = args.Component.Activator.LimitType;

        //            var logParameter = new ResolvedParameter(
        //                (p, c) => p.ParameterType == typeof(ILog),
        //                (p, c) => c.Resolve<ILog>(TypedParameter.From(forType)));

        //            args.Parameters = args.Parameters.Union(new[] { logParameter });
        //        };
        //}

        public static ILogger GetLogger(Type type)
        {
            return new Log4NetAdapter(type);
        }

        private static void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, LogManager.GetLogger(instanceType), null);
            }
        }

        private static void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter((p, i) => p.ParameterType == typeof (ILog), (p, i) => LogManager.GetLogger(t)),
                });
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {

            // Handle constructor parameters.
            registration.Preparing += OnComponentPreparing;

            // Handle properties.
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }
    }
}
