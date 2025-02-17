using CommandLine;
using System.Text.Json;
using RadzenBlazorUpgradeChecker;


using( var writer = new StringWriter() )
{
	var parser = new Parser( config => { config.EnableDashDash = true; config.AutoHelp = true; config.HelpWriter = writer; } );
	Parser.Default.ParseArguments<Options>( args ).WithParsed( options =>
	{
		var json_deserializer_options = new JsonSerializerOptions(){ ReadCommentHandling = JsonCommentHandling.Skip };
		var checker = JsonSerializer.Deserialize<List<Check>>( File.ReadAllText( options.Json ), json_deserializer_options );

		foreach( var check in checker )
		{
			check.DirectoryName = options.Directory;
			check.RunCheck( ( rule, file, line ) =>
			{
				if( rule.Action == RuleAction.Updated )
				{
					Console.WriteLine( $"{file}, line {line}: [{rule.Action}] \"{rule.StringA}\" is now \"{rule.StringB}\"" );
				}
				else //Removed:
				{
					if( string.IsNullOrEmpty( rule.StringB ) )
						Console.WriteLine( $"{file}, line {line}: [{rule.Action}] \"{rule.StringA}\"" );
					else
						Console.WriteLine( $"{file}, line {line}: [{rule.Action}] \"{rule.StringA}\" \"{rule.StringB}\"" );
				}
			} );
		}
	} );
}