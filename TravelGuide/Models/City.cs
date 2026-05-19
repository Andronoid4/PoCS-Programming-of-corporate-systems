namespace TravelGuide.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public int Population { get; set; }
    public string History { get; set; } = string.Empty;
    public string CoatOfArms { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    
    public List<Attraction> Attractions { get; set; } = new();
}
