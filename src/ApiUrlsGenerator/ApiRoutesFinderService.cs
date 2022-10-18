using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ApiUrlsGenerator;

/// <summary>
///     Finds all of the routable action methods
/// </summary>
public class ApiRoutesFinderService : IApiRoutesFinderService
{
    private readonly IActionDescriptorCollectionProvider _actionsProvider;

    /// <summary>
    ///     Finds all of the routable action methods
    /// </summary>
    public ApiRoutesFinderService(IActionDescriptorCollectionProvider actionsProvider)
    {
        _actionsProvider = actionsProvider ?? throw new ArgumentNullException(nameof(actionsProvider));
        AllRoutes = FindAllRoutes().ToList();
    }

    /// <summary>
    ///     All of the routable action methods
    /// </summary>
    public IReadOnlyList<ControllerModel> AllRoutes { get; }

    private IReadOnlySet<ControllerModel> FindAllRoutes()
    {
        var apiControllers = new HashSet<ControllerModel>(new ApiControllerEqualityComparer());

        foreach (var descriptor in _actionsProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>())
        {
            var controllerTypeInfo = descriptor.ControllerTypeInfo;
            var controllerAttributes = controllerTypeInfo.GetCustomAttributes(true);
            var currentController = new ControllerModel
                                    {
                                        AreaName =
                                            controllerAttributes.OfType<AreaAttribute>().FirstOrDefault()?.RouteValue ??
                                            string.Empty,
                                        ControllerName = descriptor.ControllerName,
                                        ControllerMethodName = controllerTypeInfo.ToString(),
                                    };
            if (apiControllers.TryGetValue(currentController, out var actualController))
            {
                currentController = actualController;
            }
            else
            {
                apiControllers.Add(currentController);
            }

            var mvcActionItem = new ActionModel
                                {
                                    ActionName = descriptor.ActionName,
                                    ActionMethodName =
                                        $"{descriptor.MethodInfo.DeclaringType?.ToString()} {descriptor.MethodInfo}",
                                    HttpMethod =
                                        descriptor.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                                                  .FirstOrDefault()?.HttpMethods
                                                  .First() ?? "Any",
                                    Route = descriptor.AttributeRouteInfo?.Template ?? "",
                                };
            currentController.ApiActions.Add(mvcActionItem);
        }

        return apiControllers;
    }
}