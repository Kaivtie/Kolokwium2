using Microsoft.AspNetCore.Mvc;
using Kol2.Sevices;
using Kol2.DTOs;

namespace EventApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IDbService _service;

    public EventsController(IDbService service)
    {
        _service = service;
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetDetails()
    {
        var result = await _service.GetEventDetailsAsync();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEventDto dto)
    {
        var updated = await _service.UpdateEventAsync(id, dto);
        if (!updated)
            return NotFound(new { message = "Nie znaleziono wydarzenia." });

        return Ok(new
        {
            message = "Wydarzenie zaktualizowane pomyślnie.",
            eventId = id,
            updatedTags = dto.TagIds,
            updatedParticipants = dto.ParticipantIds
        });
    }
}