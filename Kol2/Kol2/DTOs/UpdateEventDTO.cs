namespace Kol2.DTOs;

public class UpdateEventDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public List<int> TagIds { get; set; } = new();
    public List<int> ParticipantIds { get; set; } = new();
}