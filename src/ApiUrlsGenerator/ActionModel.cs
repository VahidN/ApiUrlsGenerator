namespace ApiUrlsGenerator;

/// <summary>
///     An Action Dto
/// </summary>
public class ActionModel
{
    /// <summary>
    ///     Return ControllerActionDescriptor.ActionName
    /// </summary>
    public string ActionName { get; set; } = default!;

    /// <summary>
    ///     Returns the fully qualified name of the action method
    /// </summary>
    public string ActionMethodName { get; set; } = default!;

    /// <summary>
    ///     Return endpoint.RoutePattern.RawText
    /// </summary>
    public string Route { get; set; } = default!;

    /// <summary>
    ///     The action method's HTTP method
    /// </summary>
    public string HttpMethod { set; get; } = default!;
}