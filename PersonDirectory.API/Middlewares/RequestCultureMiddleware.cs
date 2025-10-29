using System.Globalization;

public class RequestCultureMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var acceptLanguage = context.Request.Headers["Accept-Language"].FirstOrDefault();

        var supportedLanguages = new[] { "ka", "en" };
        var defaultCulture = new CultureInfo("ka");

        CultureInfo selectedCulture = defaultCulture;

        if (!string.IsNullOrEmpty(acceptLanguage))
        {
            var languages = acceptLanguage.Split(',')
                .Select(l => l.Split(';').FirstOrDefault()?.Trim())
                .Where(l => !string.IsNullOrEmpty(l));

            foreach (var lang in languages)
            {
                try
                {
                    var culture = new CultureInfo(lang);
                    if (supportedLanguages.Contains(culture.TwoLetterISOLanguageName))
                    {
                        selectedCulture = culture;
                        break;
                    }
                }
                catch (CultureNotFoundException)
                {
                    continue;
                }
            }
        }

        CultureInfo.CurrentCulture = selectedCulture;
        CultureInfo.CurrentUICulture = selectedCulture;
        context.Items["Culture"] = selectedCulture.Name;

        await _next(context);
    }
}