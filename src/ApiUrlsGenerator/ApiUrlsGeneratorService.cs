using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiUrlsGenerator;

/// <summary>
///     Converts all of the routable action methods to a class containing their URLs list.
/// </summary>
public class ApiUrlsGeneratorService : IApiUrlsGeneratorService
{
    private readonly IApiRoutesFinderService _apiRoutesFinderService;
    private readonly ILogger<ApiUrlsGeneratorService> _logger;
    private readonly IOptions<ApiUrlsGeneratorOptions> _options;

    /// <summary>
    ///     Converts all of the routable action methods to a class containing their URLs list.
    /// </summary>
    public ApiUrlsGeneratorService(IApiRoutesFinderService apiRoutesFinderService,
                                   IOptions<ApiUrlsGeneratorOptions> options,
                                   ILogger<ApiUrlsGeneratorService> logger)
    {
        _apiRoutesFinderService =
            apiRoutesFinderService ?? throw new ArgumentNullException(nameof(apiRoutesFinderService));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    ///     Converts all of the routable action methods to a class containing their URLs list.
    /// </summary>
    public void Execute()
    {
        var outputFolder = _options.Value.OutputFolder;
        if (string.IsNullOrWhiteSpace(outputFolder) || !Directory.Exists(outputFolder))
        {
            _logger.LogWarning("The OutputFolder `{OutputFolder}` doesn't exist.", outputFolder);
            return;
        }

        var outputFileName = _options.Value.OutputFileName;
        if (string.IsNullOrWhiteSpace(outputFileName))
        {
            _logger.LogWarning("The OutputFileName is empty.");
            return;
        }

        var sourceText = CreateSourceText();
        File.WriteAllText(Path.Combine(outputFolder, outputFileName), sourceText);
    }

    private string CreateSourceText()
    {
        var source = new StringBuilder();
        source.AppendLine("//------------------------------------------------------------------------------");
        source.AppendLine("// <auto-generated>");
        source.AppendLine("//     This code was generated by a tool.");
        source.AppendLine("//");
        source.AppendLine("//     Changes to this file may cause incorrect behavior and will be lost if");
        source.AppendLine("//     the code is regenerated.");
        source.AppendLine("// </auto-generated>");
        source.AppendLine("//------------------------------------------------------------------------------")
              .AppendLine();

        var nameSpace = string.IsNullOrWhiteSpace(_options.Value.DefaultNamespace)
                            ? "StronglyTypedApiUrls"
                            : _options.Value.DefaultNamespace;
        source.Append(CultureInfo.InvariantCulture, @$"namespace {nameSpace}
{{
  public static class ApiUrls
  {{
");

        foreach (var routeController in _apiRoutesFinderService.AllRoutes)
        {
            var code = GenerateUrlsClass(routeController);
            source.AppendLine(code);
        }

        source.AppendLine(@"  }
}");

        var sourceText = source.ToString();
        return sourceText;
    }

    private static string GenerateUrlsClass(ControllerModel controllerRoute)
    {
        var urls = new StringBuilder();

        urls.AppendLine("    /// <summary>");
        urls.AppendLine(CultureInfo.InvariantCulture, $"    /// {controllerRoute.ControllerMethodName}");
        urls.AppendLine("    /// </summary>");
        urls.AppendLine(CultureInfo.InvariantCulture,
                        $@"    public static class {controllerRoute.AreaName}{controllerRoute.ControllerName} 
    {{");
        foreach (var httpGroup in controllerRoute.ApiActions
                                                 .GroupBy(x => x.HttpMethod, StringComparer.OrdinalIgnoreCase))
        {
            var method =
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(httpGroup.Key.ToLower(CultureInfo.InvariantCulture));

            urls.AppendLine("        /// <summary>");
            urls.AppendLine("        /// The supported HTTP method");
            urls.AppendLine("        /// </summary>");
            urls.AppendLine(CultureInfo.InvariantCulture, $@"        public static class Http{method} 
        {{");
            foreach (var action in httpGroup)
            {
                urls.AppendLine("           /// <summary>");
                urls.AppendLine(CultureInfo.InvariantCulture, $"           /// {action.Route}");
                urls.AppendLine(CultureInfo.InvariantCulture, $"           /// <para>{action.ActionMethodName}</para>");
                urls.AppendLine("           /// </summary>");
                urls.AppendLine(CultureInfo.InvariantCulture,
                                $"           public const string {action.ActionName} = \"{action.Route}\";");
                urls.AppendLine();
            }

            urls.AppendLine("        }").AppendLine();
        }

        urls.AppendLine("    }");
        return urls.ToString();
    }
}