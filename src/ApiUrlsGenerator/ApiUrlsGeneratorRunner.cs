using Microsoft.Extensions.Hosting;

namespace ApiUrlsGenerator;

/// <summary>
///     Runs at startup automatically
/// </summary>
public class ApiUrlsGeneratorRunner : IHostedService
{
    private readonly IApiUrlsGeneratorService _apiUrlsGeneratorService;

    /// <summary>
    ///     Runs at startup automatically
    /// </summary>
    public ApiUrlsGeneratorRunner(IApiUrlsGeneratorService apiUrlsGeneratorService) =>
        _apiUrlsGeneratorService =
            apiUrlsGeneratorService ?? throw new ArgumentNullException(nameof(apiUrlsGeneratorService));

    /// <summary>
    ///     Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _apiUrlsGeneratorService.Execute();
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}