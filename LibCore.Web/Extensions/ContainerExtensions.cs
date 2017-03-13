using LibCore.CQRS.Validation;
using MediatR;
using SimpleInjector;
using System.Collections.Generic;
using System.Reflection;

namespace LibCore.Web.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterMediator(this Container container)
        {
            container.RegisterSingleton<IMediator, Mediator>();

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        public static void RegisterMediatorHandlers(this Container container, IEnumerable<Assembly> assemblies)
        {
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            container.Register(typeof(ICancellableAsyncRequestHandler<>), assemblies);
            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(ICancellableAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IPipelineBehavior<,>), assemblies);

            container.Register(typeof(IValidator<>), assemblies);
            container.RegisterConditional(typeof(IValidator<>), typeof(NullValidator<>), c => !c.Handled);
        }
    }
}
