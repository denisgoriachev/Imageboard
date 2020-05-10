param ([Parameter(Mandatory)][string] $name)
dotnet ef migrations add "$name" --project "Imageboard.Infrastructure" --startup-project "Imageboard.Api" --output-dir "Data\Migrations" --verbose

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');