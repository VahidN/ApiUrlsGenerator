namespace ApiUrlsGenerator;

/// <summary>
///     Converts all of the routable action methods to a class containing their URLs list.
/// </summary>
public interface IApiUrlsGeneratorService
{
    /// <summary>
    ///     Converts all of the routable action methods to a class containing their URLs list.
    /// </summary>
    void Execute();
}