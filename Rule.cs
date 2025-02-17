using System.Text.RegularExpressions;

namespace RadzenBlazorUpgradeChecker
{
	enum RuleAction { Deleted, Updated }


	class Rule
	{
		public RuleAction Action { get; set; }
		public string StringA { get; set; }
		public string StringB { get; set; }


		public Rule() { }


		public IEnumerable<int> Check( string file )
		{
			int index = 1;

			foreach( var line in File.ReadLines( file ) )
			{
				if( Regex.IsMatch( line, StringA, RegexOptions.IgnoreCase ) ) yield return index;
				index++;
			}
		}
	}
}
