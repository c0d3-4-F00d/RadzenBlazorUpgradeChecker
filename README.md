# RadzenBlazorUpgradeChecker

RadzenBlazorUpgradeChecker is a simple command line tool to check your project's source code to see if it needs modifications for a successful upgrade of Radzen Blazor Components.
I tried to keep the software as universal as possible so that it can also be used for other upgrade projects.

The supplied file *RadzenVersion5.json* is intended to be used before (or after) you upgrade your project from *Radzen Blazor Components* Version 4.x to 5. Most of the changes (updates and deletions) made during that upgrade should be covered by this and displayed to you. Making the upgrade hopefully a bit less painfull. However, use this tool at your own risk.


## Usage

After you have compiled the software, open a command line, navigate to the output folder of the executable you just created.

If you run it without parameters, it will show you such an output:
~~~ console
RadzenBlazorUpgradeChecker 1.0.0+bc6df23f0f4bb239ea2bb39291ffaad09f870ba1
Copyright (C) 2025 RadzenBlazorUpgradeChecker

ERROR(S):
  Required option 'd, directory' is missing.
  Required option 'j, json' is missing.

  -d, --directory    Required. Name of the directory to check

  -j, --json         Required. The .json file containing the checks

  --help             Display this help screen.

  --version          Display version information.
~~~

The parameters *-d* and *-j* are required (if you did not notice).

- *-d* is the path to the project you wanna check.
- *-j* is the path and name of the json file with the parameters for the checks. This is for now *RadzenVersion5.json*.

Just call it again with the additional argumens (fill in the values in the square brackets):
~~~ console
RadzenBlazorUpgradeChecker.exe -d [Path to the project to be reviewed] -j [RadzenVersion5.json]
~~~

This program will run several checks and output the result like this:
~~~ console
C:\Users\c0d3-4-f00d>C:\foo\RadzenBlazorUpgradeChecker\bin\Debug\net6.0\RadzenBlazorUpgradeChecker.exe -d C:\foo\FancyProject -j C:\foo\RadzenBlazorUpgradeChecker\bin\Debug\net6.0\RadzenVersion5.json
Checking for Dependencies...
C:\foo\FancyProject\Pages\Error.cshtml, line 11: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\Pages\_Layout.cshtml, line 11: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\Pages\_Layout.cshtml, line 33: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\wwwroot\css\site.css, line 1: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\wwwroot\templates\print\print.html, line 7: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\templates\print\print.html, line 163: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\wwwroot\css\font-awesome\css\all.css, line 6507: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\wwwroot\css\font-awesome\css\all.min.css, line 6: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\wwwroot\css\font-awesome\css\brands.css, line 156: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
C:\foo\FancyProject\css\font-awesome\css\brands.min.css, line 6: [Deleted] "bootstrap" "Dropped Bootstrap - including the Bootsrap CSS is not mandatory any more."
Checking for Typography...
Checking for Icons...
Checking for DataGrid...
Checking for DataList...
Checking for DataFilter...
Checking for Pager...
Checking for Scheduler...
Checking for Tree...
Checking for Dialog...
Checking for Sidebar...
Checking for Accordion...
Checking for ContextMenu...
Checking for Login...
Checking for Menu...
Checking for PanelMenu...
Checking for ProfileMenu...
Checking for Steps...
Checking for Tabs...
Checking for CheckBox...
Checking for ColorPicker...
Checking for DatePicker...
Checking for DropDown...
Checking for Fieldset...
Checking for FormField...
Checking for ListBox...
Checking for Numeric...
Checking for RadioButtonList...
Checking for SplitButton...
Checking for Switch...
Checking for TextArea...
Checking for TextBox...
Checking for Chip...
Checking for Notification...
Checking for Alert...
C:\Users\c0d3-4-f00d>
~~~

## Configuration file

~~~ json
[
	{
		"TestTitle": "Checking for Dependencies...",
		"FilePattern": "\\.(html|cshtml|css|scss|less|razor|ts|js)$",
		"IgnoreDirectories": [ "\\PubTmp", "\\wwwroot\\css\\bootstrap", "\\wwwroot\\lib\\bootstrap" ],
		"Rules": [
			{
				"Action": 0,
				"StringA": "RadzenGrid",
				"StringB": "Dropped the obsolete RadzenGrid component"
			},
			{
				"Action": 0,
				"StringA": "bootstrap",
				"StringB": "Dropped Bootstrap - including the Bootstrap CSS is not mandatory any more."
			}
		]
	},
//Comments are allowed by the way...
//...
]
~~~

The configuration file consists of one or more Check objects. Each Check object can contain one or more Rule objects.

### Check Object
Name | Type | Description
--- | --- | ---
TestTitle | String | Name of the test that gets displayed during execution
FilePattern | RegEx[^1] | Is matching the file extension against this
IgnoreDirectories | Array of RegEx | An array with RegEx, which directories should be ignored during the check
Rules | Rules Object | One or more rules, that should be applied to the files found by this check


### Rule Object
Name | Type | Description
--- | --- | ---
Action | enum of type RuleAction | 0 = Deleted, 1 = Updated
StringA | Regex[^1] | Every line in a matched file, will be matched against this
StringB | String | This text will be outputted as additional info. For *Action = Updated* this is a must. For *Action = Updated* this is optional.

[^1]: RegexOptions.IgnoreCase is set
