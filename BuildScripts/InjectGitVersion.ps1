param (
    [Parameter(Mandatory=$true)][string]$Version
)

$PSDefaultParameterValues = @{ '*:Encoding' = 'utf8' }
$productVersion = "1.5.";
$revision = "0";

$fileNames = Get-ChildItem -Path (Split-Path -Parent $PSScriptRoot) -Recurse -Include *.template

$gitVersion = git describe --all --long --always --first-parent;
$match = $gitVersion -match '.*/([\w\d-]+)-(\d+)-[g](\w+)$';
if ( -not $match ){
    Write-Host ( "Can not determine git version: {0}" -f $gitVersion );
    exit 1;
}

$gitTag = $Matches[1];
$gitSHA1 = $Matches[3];

$fileVersion = $productVersion + $Version + "." + $revision;
$InfoVersion = $productVersion + $Version + "." + $revision + "-" + $gitTag + "-" + $gitSHA1;

Write-Host "File Version: " + $fileVersion;
Write-Host "Info Version: " + $InfoVersion;

foreach ($fileName in $fileNames) {
    # Define file variables
    $file = Get-Item $fileName;
    $assemblyFile = ( Join-Path $file.DirectoryName  $file.BaseName );

    # Read template file, overwrite place holders with git version info
    $newAssemblyContent = Get-Content $fileName |
        %{$_ -replace '\$FILEVERSION\$', ($productVersion + $Version + "." + $revision) } |
        %{$_ -replace '\$INFOVERSION\$', ($productVersion + $Version + "." + $revision + "-" + $gitTag + "-" + $gitSHA1) };
	
    # Write AssemblyInfo.cs file only if there are changes
    If (-not (Test-Path $assemblyFile) -or ((Compare-Object (Get-Content $assemblyFile) $newAssemblyContent))) {
        Write-Host "Injecting Git Version Info to $assemblyFile" ;
        $newAssemblyContent > $assemblyFile;       
    }  
}


