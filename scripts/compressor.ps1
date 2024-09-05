param (
    [string]$FolderPath  
)

$backupFolder = Join-Path $env:USERPROFILE "Documents\OLTAB_Backup"
$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Definition
$dataFolder = Join-Path $scriptDirectory "data"
$logFilePath = Join-Path $dataFolder "backup_log.txt"

if (-not (Test-Path $dataFolder)) {
    New-Item -ItemType Directory -Path $dataFolder | Out-Null
}

Write-Host "============================================="
Write-Host "  Welcome to OLTAB - Offline Test Auto Backup"
Write-Host "============================================="
Write-Host "`nStarting the backup process for folder: $FolderPath"

Add-Content -Path $logFilePath -Value "[$(Get-Date)] Starting the backup process for folder: $FolderPath"

if (-not (Test-Path $backupFolder)) {
    New-Item -ItemType Directory -Path $backupFolder | Out-Null
    Add-Content -Path $logFilePath -Value "[$(Get-Date)] Created backup folder: $backupFolder"
} else {
    Add-Content -Path $logFilePath -Value "[$(Get-Date)] Backup folder already exists: $backupFolder"
}

$date = Get-Date -Format "yyyy-MM-dd_HH-mm"
$folderName = Split-Path $FolderPath -Leaf
$archiveName = "backup_${folderName}_$date.zip"
$archivePath = Join-Path $backupFolder $archiveName

Add-Content -Path $logFilePath -Value "[$(Get-Date)] Preparing to create archive: $archivePath"

try {
    Compress-Archive -Path $FolderPath -DestinationPath $archivePath -Force
    Add-Content -Path $logFilePath -Value "[$(Get-Date)] Backup successful: $archiveName"
    Write-Host "`nBackup successful: $archiveName"
} catch {
    Add-Content -Path $logFilePath -Value "[$(Get-Date)] Backup failed: $_"
    Write-Host "`nBackup failed: $_"
}

Add-Content -Path $logFilePath -Value "[$(Get-Date)] Completed backup process for folder: $FolderPath"
Write-Host "`nBackup process completed."

Write-Host "`nExiting in 5 seconds..."
Start-Sleep -Seconds 5 
