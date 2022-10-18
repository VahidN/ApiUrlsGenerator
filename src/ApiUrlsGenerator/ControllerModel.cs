namespace ApiUrlsGenerator;

/// <summary>
///     A Controller Dto
/// </summary>
public class ControllerModel
{
    /// <summary>
    ///     Return `AreaAttribute.RouteValue`
    /// </summary>
    public string AreaName { get; set; } = default!;

    /// <summary>
    ///     Return ControllerActionDescriptor.ControllerName
    /// </summary>
    public string ControllerName { get; set; } = default!;

    /// <summary>
    ///     Return the fully qualified method name of the controller
    /// </summary>
    public string ControllerMethodName { get; set; } = default!;

    /// <summary>
    ///     Returns the list of the Controller's action methods.
    /// </summary>
    public IList<ActionModel> ApiActions { get; } = new List<ActionModel>();
}