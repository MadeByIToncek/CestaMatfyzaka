namespace CestaMatfyzaka.Ssg;

public class TeamScore {
	public required Team Team { get; init; }
	public List<string> ObtainedCategories { get; init; } = [];
}