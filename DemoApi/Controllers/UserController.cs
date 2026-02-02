using Microsoft.AspNetCore.Mvc;
using DemoApi.Services;
using DemoApi.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DemoApi.Controllers;

[Authorize(Policy = "CanViewUsers")]
[ApiController]
[Route("users")]
// [Authorize(Roles = "User")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<PagedResult<UserResponse>>> Get(
    [FromQuery] string? name,
    [FromQuery] int? minAge,
    [FromQuery] string sortBy = "id",
    [FromQuery] string sortDir = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var result = await _userService.GetUsersAsync(
            name, minAge, sortBy, sortDir, page, pageSize);

        return result;
    }


    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        await _userService.CreateUserAsync(request);
        return CreatedAtAction(nameof(Get), null);
    }



}
