using Microsoft.AspNetCore.Mvc;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Lesson;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonsController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<LessonResponseDto>>>> GetAll()
    {
        var result = await _lessonService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<LessonResponseDto>>> GetById(int id)
    {
        var result = await _lessonService.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<LessonResponseDto>>>> GetByCourseId(int courseId)
    {
        var result = await _lessonService.GetByCourseIdAsync(courseId);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<LessonResponseDto>>> Create(CreateLessonDto dto)
    {
        var result = await _lessonService.CreateAsync(dto);
        if (!result.Success)
            return BadRequest(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<LessonResponseDto>>> Update(int id, UpdateLessonDto dto)
    {
        var result = await _lessonService.UpdateAsync(id, dto);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _lessonService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result);
        return NoContent();
    }
}
