using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace ApiUrlsGenerator;

/// <summary>
///     Urls Generator ServiceCollection Extensions
/// </summary>
public static class ApiUrlsGeneratorServiceCollectionExtensions
{
    /// <summary>
    ///     Adds default Urls Generator providers.
    /// </summary>
    public static void AddApiUrlsGenerator(
        this IServiceCollection services,
        Action<ApiUrlsGeneratorOptions>? options = null)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        ConfigOptions(services, options);

        services.TryAddSingleton<IApiUrlsGeneratorService, ApiUrlsGeneratorService>();
        services.TryAddSingleton<IApiRoutesFinderService, ApiRoutesFinderService>();
        services.AddHostedService<ApiUrlsGeneratorRunner>();
    }

    private static void ConfigOptions(IServiceCollection services, Action<ApiUrlsGeneratorOptions>? options)
    {
        var captchaOptions = new ApiUrlsGeneratorOptions();
        options?.Invoke(captchaOptions);
        services.TryAddSingleton(Options.Create(captchaOptions));
    }
}