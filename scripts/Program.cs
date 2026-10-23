using System.Data;
using System.Text;
using System.Text.Json;
using CestaMatfyzaka.Model;
using CestaMatfyzaka.Ssg;

namespace CestaMatfyzaka;

class Program {
	static async Task Main(string[] args) {
		if (args.Length > 0) {
			string? simpleOuts = args[0] switch {
				"--help" or "-h"           => PrintHelp(),
				"--example-schema" or "-e" => PrintExampleSchema(),
				"--example-team" or "-t"   => PrintExampleTeam(),
				_                          => null
			};

			if (simpleOuts != null) {
				Console.WriteLine(simpleOuts);
				return;
			}
		}

		StaticSiteGenerator ssg = new();
		await ssg.Start();
	}

	private static string PrintExampleTeam() {
		Team team = new() {
			Identifier = "team",
			PrettyName = "Team",
			CompletedGames = ["game1", "game3"]
		};

		return JsonSerializer.Serialize(team, new JsonSerializerOptions { WriteIndented = true });
	}

	private static string PrintHelp() {
		return """
		       =================== CestaMatfyzaka ===================
		       -e / --example-schema      Output example schema to stdout
		       -t / --example-team        Output example team to stdout
		       """;
	}

	private static string PrintExampleSchema() {
		SchemaDefinition sd = new() {
			Categories = [
				new Category {
					Identifier = "test1",
					PrettyName = "Category 1",
					RequiredFinished = 2,
					Order = 1,
					Games = [
						new Game {
							Order = 1,
							Identifier = "game1",
							PrettyName = "Game 1"
						},
						new Game {
							Order = 2,
							Identifier = "game2",
							PrettyName = "Game 2"
						}
					]
				},
				new Category {
					Identifier = "test2",
					PrettyName = "Category 2",
					RequiredFinished = 1,
					Order = 2,
					Games = [
						new Game {
							Order = 1,
							Identifier = "game3",
							PrettyName = "Game 3"
						},
						new Game {
							Order = 2,
							Identifier = "game4",
							PrettyName = "Game 4"
						}
					]
				}
			]
		};

		if (!sd.IsConsistent()) {
			throw new ConstraintException("Schema is not consistent!");
		}

		return JsonSerializer.Serialize(sd, new JsonSerializerOptions { WriteIndented = true });
	}
}