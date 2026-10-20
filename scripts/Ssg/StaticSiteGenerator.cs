using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using CestaMatfyzaka.Model;

namespace CestaMatfyzaka.Ssg;

public partial class StaticSiteGenerator {
	public async Task Start() {
		Stopwatch sw = new();
		sw.Start();
		LogTimed(sw, "Starting processing");
		SchemaDefinition? sd = await LoadSchemaDefinition();
		if (sd == null) {
			throw new StaticSiteGeneratorException("Schema definition could not be located!");
		}
		LogTimed(sw, "Schema definition");

		if (!sd.IsConsistent()) {
			throw new StaticSiteGeneratorException("Schema is not consistent!");
		}

		LogTimed(sw, "Schema consistency");
		List<Team> teams = await LoadTeams();
		LogTimed(sw, "Teams");
		List<TeamScore> teamScores = await ComputeScored(teams, sd);
		LogTimed(sw, "Scores");

		if (Directory.Exists("./public")) {
			Directory.Delete("./public", true);
		}

		Directory.CreateDirectory("./public");

		LinkedList<Task> pageRenders = [];

		pageRenders.AddLast(RenderIndex(teamScores, sd));
		pageRenders.AddLast(RenderGameIndex(sd));
		foreach (TeamScore score in teamScores) {
			pageRenders.AddLast(RenderTeamPage(score, sd));
		}

		await Task.WhenAll(pageRenders);
		LogTimed(sw, "Render finished!");
	}

	private void LogTimed(Stopwatch sw, string msg) {
		double elapsed = sw.Elapsed.TotalMicroseconds / 1000d;

		Console.WriteLine($"[{elapsed:F1} ms] {msg}");
		sw.Restart();
	}

	private async Task<List<TeamScore>> ComputeScored(List<Team> teams, SchemaDefinition sd) {
		return [
			.. await Task.WhenAll(teams.Select(async team => await Evaluate(team, sd)))
		];
	}

	private async Task<TeamScore> Evaluate(Team team, SchemaDefinition sd) {
		Dictionary<string, int> categoryScores = [];
		foreach (Category category in sd.Categories) {
			categoryScores.TryAdd(category.Identifier, category.RequiredFinished);
		}

		foreach (string x in team.CompletedGames) {
			Category? category = sd.Categories.FirstOrDefault(y => y.Games.Any(z => z.Identifier == x));
			if (category == null) {
				throw new StaticSiteGeneratorException($"Team {team} has game {x} which isn't in any category!");
			}

			categoryScores[category.Identifier] -= 1;
		}

		return new TeamScore {
			Team = team,
			ObtainedCategories = [
				.. categoryScores
					.Where(x => x.Value <= 0)
					.Select(x => x.Key)
			]
		};
	}

	[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")] //linq is stupid, can't be null
	private async Task<List<Team>> LoadTeams() {
		return [
			.. (await Task.WhenAll(
				Directory.EnumerateFiles("./scores")
					.Where(path => path.EndsWith(".team"))
					.Select(async filePath => {
							await using FileStream fs = File.OpenRead(filePath);
							return await JsonSerializer.DeserializeAsync<Team>(fs);
						}
					)
			)).Where(x => x != null).Select(x => x!)
		];
	}

	private async Task<SchemaDefinition?> LoadSchemaDefinition() {
		const string path = "./schema.json";
		return await JsonSerializer.DeserializeAsync<SchemaDefinition>(File.OpenRead(path));
	}
}