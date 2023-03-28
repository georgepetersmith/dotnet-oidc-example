using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("identity")]
[Authorize]
public class IdentityController : ControllerBase
{
    public IActionResult Get()
    {
        return new JsonResult(User.Claims.Select(claim => new {claim.Type, claim.Value}));
    }
}
