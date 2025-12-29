using Microsoft.AspNetCore.Mvc;
using dotnet_project.DTOs;
using dotnet_project.DTOs.Enrollment;
using dotnet_project.Services.Interfaces;

namespace dotnet_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<EnrollmentResponseDto>>>> GetAll()
    {
        var result = await _enrollmentService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EnrollmentResponseDto>>> GetById(int id)
    {
        var result = await _enrollmentService.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<EnrollmentResponseDto>>> Create(CreateEnrollmentDto dto)
    {
        var result = await _enrollmentService.CreateAsync(dto);
        if (!result.Success)
        {
            // aynı kursa tekrar kayıt
            if (result.Message?.Contains("zaten") == true)
                return Conflict(result);
            return BadRequest(result);
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
    {
        var result = await _enrollmentService.DeleteAsync(id);
        if (!result.Success)
            return NotFound(result);
        return NoContent();
    }
}
