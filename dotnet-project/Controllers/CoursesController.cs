using Microsoft.AspNetCore.Mvc;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Course;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CourseResponseDto>>>> GetAll()
    {
        var result = await _courseService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CourseResponseDto>>> GetById(int id)
    {
        var result = await _courseService.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<CourseResponseDto>>> Create(CreateCourseDto dto)
    {
        var result = await _courseService.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<CourseResponseDto>>> Update(int id, UpdateCourseDto dto)
    {
        var result = await _courseService.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _courseService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result);
        return NoContent();
    }
}
