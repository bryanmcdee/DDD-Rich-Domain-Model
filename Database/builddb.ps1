$ErrorActionPreference="stop";

Clear-Host

# set current dir to same as where the script is running
[Environment]::CurrentDirectory=(Get-Location -PSProvider FileSystem).ProviderPath

$loc = Get-Location

if ((Get-Module -Name "runSqlScript")) {
    remove-module runSqlScript
}

import-module .\PowershellModules\runSqlScript

$appSettings = Get-Content database.config.json | Out-String | ConvertFrom-Json	

$cs = $appSettings.ConnectionString
$ds = $appSettings.DataSource;
$db = $appSettings.Database;
$un = $appSettings.DatabaseUserName;
$pw = $appSettings.DatabasePassword;

# Create Database
.\Scripts\createNewDatabase.ps1 $cs $db

# Apply Patches
$patches = Get-ChildItem .\Patches\

for ($i=0; $i -lt $patches.Count; $i++) {
    $folder = $patches[$i].FullName
    $folderName = $patches[$i].Name

    $files = @(Get-ChildItem $folder)
    foreach ($file in $files) {
        $fileName = $file.Name;
        echo "$folderName -- $fname"
        $scriptPath = "{0}\Patches\{1}\{2}" -f $loc.Path,$folderName,$fileName

        runSqlScript $scriptPath $ds $db $un $pw;
		
        if($LASTEXITCODE -ne 0)
        {
            exit -1
        }
    }

}

exit $LASTEXITCODE


