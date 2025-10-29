using System.Globalization;

namespace PersonDirectory.API.Middlewares
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] supportedLanguages = new[] { "ka", "en" };
        private readonly CultureInfo defaultCulture = new CultureInfo("ka");

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].FirstOrDefault();

            var selectedCulture = defaultCulture;

            if (!string.IsNullOrEmpty(acceptLanguage) && supportedLanguages.Contains(acceptLanguage))
                selectedCulture = new CultureInfo(acceptLanguage);

            CultureInfo.CurrentCulture = selectedCulture;
            CultureInfo.CurrentUICulture = selectedCulture;
            context.Items["Culture"] = selectedCulture.Name;

            await _next(context);
        }
    }
}