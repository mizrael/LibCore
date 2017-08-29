using System;
using SimpleInjector;
using LibCore.Web.ErrorHandling.Builders;

namespace LibCore.Web.ErrorHandling
{
    public class ApiErrorInfoFactory : IApiErrorInfoFactory
    {
        private readonly Container _container = null;
        private readonly ApiErroInfoBuilderOptions _defaultOptions;

        public ApiErrorInfoFactory(Container container, ApiErroInfoBuilderOptions defaultOptions)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _defaultOptions = defaultOptions ?? new ApiErroInfoBuilderOptions(ApiErroInfoBuilderOptions.LoggingLevels.Minimal);
        }

        public ApiErrorInfo Create<TEx>(TEx ex) where TEx : Exception
        {
            if (null == ex)
                throw new ArgumentNullException(nameof(ex));
            
            var builder = _container.GetInstance<IApiErrorInfoBuilder<TEx>>();
            var result = builder.Build(ex, _defaultOptions);
            return result;
        }
    }
}