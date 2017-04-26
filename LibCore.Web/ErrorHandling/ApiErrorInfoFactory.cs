using System;
using SimpleInjector;
using LibCore.Web.ErrorHandling.Builders;

namespace LibCore.Web.ErrorHandling
{
    public class ApiErrorInfoFactory : IApiErrorInfoFactory
    {
        private readonly Container _container = null;

        public ApiErrorInfoFactory(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public ApiErrorInfo Create<TEx>(TEx ex) where TEx : Exception
        {
            if (null == ex)
                throw new ArgumentNullException(nameof(ex));
            
            var builder = _container.GetInstance<IApiErrorInfoBuilder<TEx>>();
            var result = builder.Build(ex);
            return result;
        }
    }
}