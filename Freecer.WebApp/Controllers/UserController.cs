using Freecer.Domain.Entities;
using Freecer.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freecer.WebApp.Controllers;

public class UserController : ApiController
{
    private readonly UnitOfWork _unitOfWork;

    public UserController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var users = await _unitOfWork.Context.Users.ToListAsync();
        return Ok(users);
    }

}