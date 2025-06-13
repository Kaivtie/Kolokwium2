using Kol2.DTOs;

namespace EventApi.Services;

public interface IDbService
{
    Task<List<EventDetailsDto>> GetEventDetailsAsync();
    Task<bool> UpdateEventAsync(int id, UpdateEventDto dto);
}