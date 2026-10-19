namespace CestaMatfyzaka.Model;

public class SchemaDefinition {
	public List<Category> Categories { get; init; } = [];

	public bool IsConsistent() {
		List<Game> games = [];
		foreach (List<Game> gameset in Categories.Select(x => x.Games)) {
			games.AddRange(gameset);
		}

		return !games.Where((ga, a) =>
			       games.Where((gb, b) => a != b && ga.Identifier == gb.Identifier).Any()
		       ).Any()
		       &&
		       !Categories.Where((ca, a) =>
			       Categories.Where((cb, b) => a != b && ca.Identifier == cb.Identifier).Any()
		       ).Any()
		       &&
		       Categories.All(x => !x.Games.Where((ga, a) =>
				       x.Games.Where((gb, b) => a != b && ga.Order == gb.Order).Any()
			       ).Any()
		       )
		       &&
		       !Categories.Where((ca, a) =>
			       Categories.Where((cb, b) => a != b && ca.Order == cb.Order).Any()
		       ).Any();
	}
}

public class Category {
	public required string Identifier { get; init; }
	public required string PrettyName { get; init; }
	public required int RequiredFinished { get; init; }
	public required int Order { get; init; }
	public List<Game> Games { get; init; } = [];
}

public class Game {
	public required string Identifier { get; init; }
	public required string PrettyName { get; init; }
	public required int Order { get; init; }
}