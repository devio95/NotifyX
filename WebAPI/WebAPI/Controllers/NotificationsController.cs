using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class NotificationsController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> GetTestsAsync()
        {
            await Task.Delay(1);
            return Ok(new List<int>() { 1, 2, 3 });
        }
    }
}
