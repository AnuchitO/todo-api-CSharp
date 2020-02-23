using Microsoft.AspNetCore.Mvc;
using System;

namespace TodoApi.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Produces("application/json")]
  public class UserController : ControllerBase
  {
    [HttpGet]
    public ActionResult<string> GetUser()
    {
      var headers = Request.Headers;
      string rawId = headers["Accept-Language"];
      Console.WriteLine("headerss=====================================");
      Console.WriteLine(rawId);

      Response.Headers.Add("nong", "your header");
      return "token";
    }
  }
}