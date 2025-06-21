using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class NotificationsController : NotifyXController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<int>>> GetTestsAsync()
    {
        await Task.Delay(1);
        return Ok(new List<int>() { 1, 2, 3 });
    }
}
