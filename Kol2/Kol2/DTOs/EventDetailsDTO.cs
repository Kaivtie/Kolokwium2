namespace Kol2.DTOs;

public class EventDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public UserDto Organizer { get; set; } = null!;
    public List<UserDto> Participants { get; set; } = new();
    public List<TagDto> Tags { get; set; } = new();
}

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
}

public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}