using System.Text;
using CestaMatfyzaka.Model;

namespace CestaMatfyzaka.Ssg;

public partial class StaticSiteGenerator {
	private static async Task RenderIndex(List<TeamScore> teamScores, SchemaDefinition sd) {
		StringBuilder sb = new();
		foreach (string formattedRow in teamScores.Select(x => FormatTeam(x, sd))) {
			sb.AppendLine(formattedRow);
		}

		StringBuilder sb2 = new();
		sb2.AppendLine(string.Format(ColumnCell, "Name"));
		foreach (Category t in sd.Categories.OrderBy(x => x.Order)) {
			sb2.AppendLine(string.Format(ColumnCell, t.PrettyName));
		}

		string formatedTable = string.Format(Table, sb, sb2);
		string formatedPage = string.Format(Page, formatedTable, "Výsledky");

		await using StreamWriter sw = new(File.OpenWrite("./public/index.html"));
		await sw.WriteAsync(formatedPage);
	}

	private static string FormatTeam(TeamScore team, SchemaDefinition sd) {
		StringBuilder sb = new();

		foreach (Category sdCat in sd.Categories) {
			bool completed = team.ObtainedCategories.Contains(sdCat.Identifier);
			string checkmark = completed ? "✅" : "❌";

			sb.AppendLine(string.Format(completed ? CellOk : Cell, checkmark));
		}

		return string.Format(Row, string.Format(Link, team.Team.PrettyName, $"{team.Team.Identifier}.html"), sb);
	}
}