using LibCore.Web.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleInjector;
using System;

namespace LibCore.Web.Services
{
    public class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
    {
        private readonly Container _container = null;

        public ConfigureMvcOptions(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void Configure(MvcOptions options)
        {
            var errorInfoFactory = _container.GetInstance<IApiErrorInfoFactory>();
            options.Filters.Add(new LibCore.Web.Filters.ExceptionFilter(errorInfoFactory));
        }
    }
}
