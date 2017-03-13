using Microsoft.AspNetCore.Mvc;

namespace LibCore.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult OkOrNotFound<T>(this Controller controller, T data)
        {
            if (null == data) return controller.NotFound();
            return controller.Ok(data);
        }
    }
}