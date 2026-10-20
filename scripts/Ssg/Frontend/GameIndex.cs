using System.Text;
using CestaMatfyzaka.Model;

namespace CestaMatfyzaka.Ssg;

public partial class StaticSiteGenerator {
	private async Task RenderGameIndex(SchemaDefinition sd) {
		StringBuilder sb = new();

		foreach (Category sdCategory in sd.Categories) {
			const int width = 40;
			sb.AppendLine(new string('-', width));
			sb.AppendLine(("Category: " + sdCategory.Identifier).PadBoth(width));
			sb.AppendLine(new string('-', width));
			foreach (Game game in sdCategory.Games) {
				sb.AppendLine(game.Identifier);
			}

			sb.AppendLine();
		}

		await using StreamWriter sw = new(File.OpenWrite("./public/games.txt"));
		await sw.WriteAsync(sb);
	}
}

public static class StringExtensions {
	public static string PadBoth(this string str, int totalWidth, char paddingChar = ' ') {
		int padding = totalWidth - str.Length;
		int padLeft = padding / 2 + str.Length;
		return str.PadLeft(padLeft, paddingChar).PadRight(totalWidth, paddingChar);
	}
}