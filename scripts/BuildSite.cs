#!/usr/bin/dotnet run

using System.Text;
using BuildSite;

const int columnCount = 10;

const string row = """
                   <tr>
                     <th scope="row">{0}</th>
                     {1}
                   </tr>
                   """;
const string columnCell = """
                          <th scope="col">{0}</th>
                          """;
const string cell = "<td>{0}</td>";
const string cellOk = "<td class=\"theme-success\">{0}</td>";

const string table = """
                     <table class="table">
                       <thead>
                         <tr>
                           <th scope="col">Name</th>
                           {1}
                         </tr>
                       </thead>
                       <tbody>
                       {0}
                       </tbody>
                     </table>
                     """;

const string page = """
                    <!doctype html>
                    <html lang="en" data-bs-theme="dark">
                    <head>
                        <meta charset="utf-8">
                        <meta name="viewport" content="width=device-width, initial-scale=1">
                        <title>{1}</title>
                        <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-B/GM4XqrwHnWXNOWMbloTmrYXZg10cakYGmpfsR/bbzQ6JAJI4ihuyADKLnBgrCe" crossorigin="anonymous">
                    </head>
                    <body>

                    <nav class="navbar bg-body-tertiary">
                        <div class="container-fluid">
                            <a class="navbar-brand" href="/">
                                {1}
                            </a>
                        </div>
                    </nav>

                    {0}

                    <script type="module" src="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-1a/pXj49ZQ1aHEmrJ+gMw1otqoVsYwlEnlD8mIfY2TV03r20Y0CN7uqx1tQogjPL" crossorigin="anonymous"></script>
                    </body>
                    </html>

                    """;

Directory.CreateDirectory("./public");
File.Delete("./public/index.html");


StringBuilder sb = new();
foreach (string formattedRow in ListScores().Select(ReadProgress).Select(TeamProgressToTableRow)) {
	sb.AppendLine(formattedRow);
}

StringBuilder sb2 = new();
for (int i = 0; i < columnCount; i++) {
	sb2.AppendLine(string.Format(columnCell, i));
}

string formatedTable = string.Format(table, sb, sb2);
string formatedPage = string.Format(page, formatedTable, "Výsledky");

await using StreamWriter sw = new StreamWriter(File.OpenWrite("./public/index.html"));

await sw.WriteAsync(formatedPage);

return;

IEnumerable<string> ListScores() {
	return Directory.EnumerateFiles("./scores").Where(x => x.Contains(".md")).Order();
}

TeamProgress? ReadProgress(string file) {
	using StreamReader sr = new StreamReader(File.OpenRead(file));
	Dictionary<int, bool> progress = [];

	string? header = sr.ReadLine();
	if (header == null) {
		return null;
	}

	while (sr.ReadLine()?.Trim() is { } line) {
		string[] parts = line.Split("|");
		if (!int.TryParse(parts[0], out int ident)) {
			return null;
		}

		bool isFinished = parts[1].Trim().Length != 0;
		progress.Add(ident, isFinished);
	}

	return new TeamProgress {
		Progress = progress,
		Name = header
	};
}

string TeamProgressToTableRow(TeamProgress? progress) {
	StringBuilder sb = new();
	if (progress == null) {
		for (int i = 0; i < columnCount; i++) {
			sb.AppendLine(string.Format(cell, "Error"));
		}

		return string.Format(row, "Team", sb);
	}

	for (int i = 0; i < columnCount; i++) {
		bool completed = progress.Progress[i];
		string checkmark = completed ? "✅" : "❌";

		sb.AppendLine(string.Format(completed ? cellOk : cell, checkmark));
	}

	return string.Format(row, progress.Name, sb);
}

namespace BuildSite {
	public class TeamProgress {
		public required Dictionary<int, bool> Progress { get; init; }
		public required string Name { get; set; }
	}
}