using Freecer.Infra;
using Microsoft.AspNetCore.Mvc;

namespace Freecer.WebApp.Controllers;

public class ConfigurationController : ApiController
{
    private readonly UnitOfWork _unitOfWork;

    public ConfigurationController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
 
    [HttpGet("ping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> Ping()
    {
        return Task.FromResult<IActionResult>(Ok("Pong"));
    }
    
    [HttpGet("migrate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Migrate()
    {
        await _unitOfWork.Migrate();
        return Ok("Migrated");
    }
}