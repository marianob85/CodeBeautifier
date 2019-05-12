# InjectGitVersion.ps1
#
# Set the version in the projects AssemblyInfo.cs file
#

$productVersion = "1.4.0.";

$gitVersion = git describe --long --always;
$gitVersion -match '.*-(\d+)-[g](\w+)$';
$gitCount = $Matches[1];
$gitSHA1 = $Matches[2];

$gitBranch = git describe --all --exact-match
$gitBranch -match '.*/([\w\d]+)$';
$gitTag = $Matches[1];


# Define file variables
$assemblyFile = $args[0] + "\Properties\AssemblyInfo.cs";
$templateFile =  $args[0] + "\Properties\AssemblyInfo_template.cs";

# Read template file, overwrite place holders with git version info
$newAssemblyContent = Get-Content $templateFile |
    %{$_ -replace '\$FILEVERSION\$', ($productVersion + $gitCount) } |
    %{$_ -replace '\$INFOVERSION\$', ($productVersion + $gitCount + "-" + $gitTag + "-" + $gitSHA1) };
	
# Write AssemblyInfo.cs file only if there are changes
If (-not (Test-Path $assemblyFile) -or ((Compare-Object (Get-Content $assemblyFile) $newAssemblyContent))) {
    echo "Injecting Git Version Info to AssemblyInfo.cs"
    $newAssemblyContent > $assemblyFile;       
}