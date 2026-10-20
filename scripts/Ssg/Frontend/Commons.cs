namespace CestaMatfyzaka.Ssg;

public partial class StaticSiteGenerator {
	private const string Row = """
	                           <tr>
	                             <th scope="row">{0}</th>
	                             {1}
	                           </tr>
	                           """;

	private const string ColumnCell = """
	                                  <th scope="col">{0}</th>
	                                  """;
	private const string ColumnCellOk = """
	                                  <th scope="col" class="theme-success">{0}</th>
	                                  """;
	private const string ColumnCellFail = """
	                                  <th scope="col" class="theme-danger">{0}</th>
	                                  """;

	private const string Cell = "<td>{0}</td>";

	private const string Link =
		"<a href=\"./{1}\" class=\"theme-secondary underline-offset-2 underline-20 hover:underline-100\">{0}</a>";

	private const string CellOk = "<td class=\"theme-success\">{0}</td>";

	private const string Table = """
	                             <table class="table">
	                               <thead>
	                                 <tr>
	                                   {1}
	                                 </tr>
	                               </thead>
	                               <tbody>
	                               {0}
	                               </tbody>
	                             </table>
	                             """;

	private const string Separator = "<span class=\"mb-3\"></span>";

	private const string Container = """
	                                 		<div class="container">
	                                 			<button
	                                 				type="button"
	                                 				class="btn-outline theme-secondary btn-icon mb-3"
	                                 				aria-label="Auto theme"
	                                                 onclick="window.location = './index.html'"
	                                 			>
	                                 				<svg
	                                 					xmlns="http://www.w3.org/2000/svg"
	                                 					width="16"
	                                 					height="16"
	                                 					fill="currentColor"
	                                 					class="bi bi-arrow-left"
	                                 					viewBox="0 0 16 16"
	                                 				>
	                                 					<path
	                                 						fill-rule="evenodd"
	                                 						d="M15 8a.5.5 0 0 0-.5-.5H2.707l3.147-3.146a.5.5 0 1 0-.708-.708l-4 4a.5.5 0 0 0 0 .708l4 4a.5.5 0 0 0 .708-.708L2.707 8.5H14.5A.5.5 0 0 0 15 8"
	                                 					/>
	                                 				</svg>
	                                 			</button>
	                                 			<div class="card">
	                                 				<div class="card-body">
	                                 					{0}
	                                 				</div>
	                                 			</div>
	                                 		</div>
	                                 """;

	private const string Title = """
	                             <{0}>{1}</{0}>
	                             """;

	private const string Page = """
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
}