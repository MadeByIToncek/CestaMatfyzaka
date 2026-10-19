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

	private const string Cell = "<td>{0}</td>";
	private const string Link = "<a href=\"{1}\" class=\"theme-secondary underline-offset-2 underline-20 hover:underline-100\">{0}</a>";
	private const string CellOk = "<td class=\"theme-success\">{0}</td>";

	private const string Table = """
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