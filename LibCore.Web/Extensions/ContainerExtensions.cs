using LibCore.CQRS.Validation;
using LibCore.Web.ErrorHandling;
using LibCore.Web.ErrorHandling.Builders;
using LibCore.Web.Exceptions;
using SimpleInjector;

namespace LibCore.Web.Extensions
{
    public static class ContainerExtensions
    {
        public static void RegisterErrorFilter(this Container container)
        {
            container.Register<IApiErrorInfoBuilder<ValidationException>, ValidationApiErrorInfoBuilder>();
            container.Register<IApiErrorInfoBuilder<ApiException>, ApiErrorInfoBuilder>();

            container.RegisterConditional(typeof(IApiErrorInfoBuilder<>), typeof(DefaultApiErrorInfoBuilder<>), c => !c.Handled);

            var options = new ApiErroInfoBuilderOptions(ApiErroInfoBuilderOptions.LoggingLevels.Minimal);
            container.Register<IApiErrorInfoFactory>(() => new ApiErrorInfoFactory(container, options));
        }
    }
}
