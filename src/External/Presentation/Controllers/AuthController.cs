using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Hello World!");
    }
}