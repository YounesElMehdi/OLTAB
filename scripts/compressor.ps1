param (
    [string]$FolderPath  # Accepts the folder path as an argument from the Manager app
)

# Define the path for the backup folder and log file
$backupFolder = "$env:USERPROFILE\Documents\OLTAB_Backup"
$logFilePath = Join-Path (Get-Location) "data\backup_log.txt"

# Create the backup folder if it doesn't exist
if (-not (Test-Path $backupFolder)) {
    New-Item -ItemType Directory -Path $backupFolder | Out-Null
}

# Get the current date and time for the backup file name
$date = Get-Date -Format "yyyy-MM-dd_HH-mm"
$folderName = Split-Path $FolderPath -Leaf
$archiveName = "backup_${folderName}_$date.zip"
$archivePath = Join-Path $backupFolder $archiveName

# Compress the folder into a ZIP file and log the operation
try {
    Compress-Archive -Path $FolderPath -DestinationPath $archivePath -Force
    Write-Output "[$(Get-Date)] Backup successful: $archiveName"
    Add-Content -Path $logFilePath -Value "[$(Get-Date)] Backup successful: $archiveName"
}
catch {
    Write-Output "[$(Get-Date)] Backup failed: $_"
    Add-Content -Path $logFilePath -Value "[$(Get-Date)] Backup failed: $_"
}
