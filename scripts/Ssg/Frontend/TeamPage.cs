using System.Text;
using CestaMatfyzaka.Model;

namespace CestaMatfyzaka.Ssg;

public partial class StaticSiteGenerator {
	private static async Task RenderTeamPage(TeamScore score, SchemaDefinition sd) {
		StringBuilder sb = new();

		foreach (Category sdCategory in sd.Categories) {
			StringBuilder sb3 = new();
			sb3.AppendFormat(ColumnCell, sdCategory.PrettyName);
			if (score.ObtainedCategories.Contains(sdCategory.Identifier)) {
				sb3.AppendFormat(ColumnCellOk, "✅ Category Completed");
			} else {
				sb3.AppendFormat(ColumnCellFail, "❌ Category Not Completed");
			}

			StringBuilder sb4 = new();
			foreach (Game game in sdCategory.Games) {
				bool completed = score.Team.CompletedGames.Contains(game.Identifier);
				string checkmark = completed ? "✅" : "❌";
				string cell = string.Format(completed ? CellOk : Cell, checkmark);

				sb4.AppendFormat(Row, game.PrettyName, cell);
			}

			sb.AppendFormat(Table, sb4, sb3);

			sb.AppendLine(Separator);
		}

		string formatedTable = string.Format(
			Container, string.Format(Title, "h1", score.Team.PrettyName) + "\n" + sb
		);
		string formatedPage = string.Format(Page, formatedTable, "Výsledky");

		await using StreamWriter sw = new(File.OpenWrite($"./public/{score.Team.Identifier}.html"));
		await sw.WriteAsync(formatedPage);
	}
}