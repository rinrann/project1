# Run this once in PowerShell (as Administrator if needed)
# Moves the new ASP.NET Core project into its own clean folder: D:\Projects\ClinicMS\

$source = "D:\Projects\ClinicManagementSystem"
$dest   = "D:\Projects\ClinicMS"

New-Item -ItemType Directory -Force -Path $dest | Out-Null

Copy-Item -Path "$source\src"          -Destination "$dest\src"          -Recurse -Force
Copy-Item -Path "$source\ClinicMS.sln" -Destination "$dest\ClinicMS.sln" -Force
Copy-Item -Path "$source\SETUP.md"     -Destination "$dest\SETUP.md"     -Force

Remove-Item -Path "$source\src"          -Recurse -Force
Remove-Item -Path "$source\ClinicMS.sln" -Force
Remove-Item -Path "$source\SETUP.md"     -Force

Write-Host "`nDone! Open this in Visual Studio 2022:" -ForegroundColor Green
Write-Host "  $dest\ClinicMS.sln" -ForegroundColor Cyan
