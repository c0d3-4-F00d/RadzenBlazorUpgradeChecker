using System.Text.RegularExpressions;


namespace RadzenBlazorUpgradeChecker
{
	public static class DirectoryInfoEx
	{
		public static IEnumerable<FileInfo> GetFilesByExtensions( this DirectoryInfo dir, string file_extensions )
		{
			if( string.IsNullOrWhiteSpace( file_extensions ) ) throw new ArgumentNullException( nameof( file_extensions ) );
			var files = dir.EnumerateFiles( "*.*", SearchOption.AllDirectories );
			return files.Where( x => Regex.IsMatch( x.Extension, file_extensions, RegexOptions.IgnoreCase ) );
		}
	}
}
