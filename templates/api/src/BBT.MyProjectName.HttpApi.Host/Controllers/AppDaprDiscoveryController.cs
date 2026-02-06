using BBT.Aether.AspNetCore.Events;
using BBT.Aether.Events;
using Microsoft.AspNetCore.Mvc;

namespace BBT.MyProjectName.Controllers;

[Route("dapr")]
public class AppDaprDiscoveryController(IDistributedEventInvokerRegistry invokerRegistry) 
    : DaprDiscoveryController(invokerRegistry)
{
    /// <summary>
    /// Returns Dapr subscription configuration for all registered event handlers.
    /// This endpoint is called by Dapr runtime to discover subscriptions.
    /// </summary>
    /// <returns>JSON result containing subscription configuration</returns>
    [HttpGet("subscribe", Order = int.MinValue)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Subscribe()
    {
        return GetSubscriptions();
    }

    // Example: Override GetSubscriptions() to customize behavior
    // Uncomment and modify as needed:

    // protected override IActionResult GetSubscriptions()
    // {
    //     // Custom pre-processing logic here
    //     var result = base.GetSubscriptions();
    //     // Custom post-processing logic here
    //     return result;
    // }

    // Example: Create alternative endpoints with different routes
    // [HttpGet("subscriptions")]
    // public IActionResult AlternativeRoute()
    // {
    //     return GetSubscriptions();
    // }
}