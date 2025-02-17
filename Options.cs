using CommandLine;


namespace RadzenBlazorUpgradeChecker
{
	class Options
	{
		[Option( 'd', "directory", Required = true, HelpText = "Name of the directory to check" )]
		public string Directory { get; set; }

		[Option( 'j', "json", Required = true, HelpText = "The .json file containing the checks" )]
		public string Json { get; set; }
	}
}
