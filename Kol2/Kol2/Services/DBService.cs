using Kol2.Data;
using Kol2.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<EventDetailsDto>> GetEventDetailsAsync()
    {
        var order = await _context.Events
            .Select(e => new EventDetailsDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Organizer = new UserDto
                {
                    Id = e.Organizer.Id,
                    Username = e.Organizer.Username
                },
                Participants = e.EventParticipants
                    .Select(p => new UserDto { Id = p.ParticipantId, Username = p.Participant.Username }).ToList(),
                Tags = e.EventTags
                    .Select(t => new TagDto { Id = t.TagId, Name = t.Tag.Name }).ToList()
            })
            .ToListAsync();
    }

    public async Task<bool> UpdateEventAsync(int id, UpdateEventDto dto)
    {
        var ev = await _context.Events
            .Include(e => e.EventParticipants)
            .Include(e => e.EventTags)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (ev == null) return false;

        ev.Title = dto.Title;
        ev.Description = dto.Description;
        ev.Date = dto.Date;

        _context.EventParticipants.RemoveRange(ev.EventParticipants);
        _context.EventTags.RemoveRange(ev.EventTags);

        ev.EventParticipants = dto.ParticipantIds.Select(pid => new EventParticipant
        {
            EventId = id,
            ParticipantId = pid
        }).ToList();

        ev.EventTags = dto.TagIds.Select(tid => new EventTag
        {
            EventId = id,
            TagId = tid
        }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }
}
