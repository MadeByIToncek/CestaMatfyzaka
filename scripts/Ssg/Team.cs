namespace CestaMatfyzaka.Ssg;

public class Team {
	public required string Identifier { get; init; }
	public required string PrettyName { get; init; }
	public List<string> CompletedGames { get; init; } = [];
}