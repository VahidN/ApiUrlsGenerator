namespace ApiUrlsGenerator;

/// <summary>
///     Finds all of the routable action methods
/// </summary>
public interface IApiRoutesFinderService
{
    /// <summary>
    ///     All of the routable action methods
    /// </summary>
    IReadOnlyList<ControllerModel> AllRoutes { get; }
}