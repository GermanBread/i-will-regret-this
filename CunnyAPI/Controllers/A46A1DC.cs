using CunnyApi.Globals;

using Microsoft.AspNetCore.Mvc;

namespace CunnyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class A46A1DC : ControllerBase
{
    [HttpPatch]
    [Route("DD6FDE92")]
    public string Get()
    {
        var uuid = Guid.NewGuid().ToString();
        BackendGlobals.UuidList.Add(uuid);
        return uuid;
    }
}