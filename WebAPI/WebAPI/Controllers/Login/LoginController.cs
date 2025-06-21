using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Login;

public class LoginController : NotifyXController
{
    [HttpPost]
    public async Task<ActionResult<int>> Token()
    {
        await Task.Delay(1);
        return 5;
    }
}
