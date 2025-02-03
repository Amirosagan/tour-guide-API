using Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/upload")]
public class UploadController(IStorageService storageService) : Controller
{
    private readonly IStorageService _storageService = storageService;

    [HttpPost]
    [RequestSizeLimit(5*1024*1024)]
    public async Task<IActionResult> Index(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var guid = Guid.NewGuid().ToString();
        var result = await _storageService.UploadAsync(guid, stream);
        return Ok(result);
    }
}