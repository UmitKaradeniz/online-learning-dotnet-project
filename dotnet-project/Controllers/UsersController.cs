using Microsoft.AspNetCore.Mvc;
using dotnet_project.DTOs;
using dotnet_project.DTOs.User;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>> GetAll()
    {
        var result = await _userService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetById(int id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Create(CreateUserDto dto)
    {
        var result = await _userService.CreateAsync(dto);
        if (!result.Success)
            return Conflict(result); // Email zaten kullanımda
        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Update(int id, UpdateUserDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _userService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result);
        return NoContent();
    }
}
