using System.Text.Json.Serialization;


namespace RadzenBlazorUpgradeChecker
{
	class Check
	{
		[JsonIgnore]
		public string DirectoryName { get; set; }
		public string TestTitle { get; set; }
		public string FilePattern { get; set; }
		public string[] IgnoreDirectories { get; set; }
		public List<Rule> Rules { get; set; }


		public Check() { }


		public void RunCheck( Action<Rule,string,int> Match )
		{
			Console.WriteLine( TestTitle );
			var files = new DirectoryInfo( DirectoryName ).GetFilesByExtensions( FilePattern );
			var filtered_files = files.Where( file => !IgnoreDirectories.Any( dir => file.DirectoryName.IndexOf( dir, StringComparison.OrdinalIgnoreCase ) >= 0 ) );

			foreach( var file in filtered_files )
			{
				//Console.WriteLine( "Checking " + file + "..." );
				foreach( var rule in Rules )
				{
					var lines = rule.Check( file.FullName );

					if( lines != null )
					{
						foreach( var line in lines )
						{
							Match?.Invoke( rule, file.FullName, line );
						}
					}
				}
			}
		}
	}
}
