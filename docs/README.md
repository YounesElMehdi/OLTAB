# OLTAB (Offline Test Auto Backup) - Version 2.0

## Overview
OLTAB (Offline Test Auto Backup) is a Windows application that enables users to schedule automated backups of multiple folders. This tool uses Windows Task Scheduler to automate backups and can compress the files into both **ZIP** and **7Z** formats via PowerShell and 7-Zip Portable.

## Key Features
- **Multi-Folder Support**: Users can now schedule backups for multiple folders, each with its own schedule and settings.
- **Configurable Compression**: Supports both **ZIP** and **7Z** formats for compressed backups, reducing file size and saving disk space.
- **Flexible Scheduling**: Schedule daily, weekly, or monthly backups using the Windows Task Scheduler.
- **Customizable Configurations**: Store multiple backup configurations in a single JSON file.
- **Detailed Logging**: Logs all operations, successes, and errors in `backup_log.txt` for easy tracking and debugging.
- **Admin Privileges**: Automatically requests administrator rights to work with the Windows Task Scheduler.

## New in Version 2.0
- **Multiple Backup Configurations**: You can now schedule backups for more than one folder, each with different schedules.
- **7-Zip Compression**: Added support for 7Z compression using 7-Zip Portable.
- **Log Retention Policy**: Automatically deletes old log files based on user-defined retention periods.
- **Dynamic Config Handling**: Uses a configuration list stored in a JSON file to handle multiple backups.

## Installation
1. Download the latest release from the [Releases](https://github.com/YounesElMehdi/OLTAB/releases) page.
2. Extract the ZIP file to a location on your computer.
3. Run the application executable (`OLTAB.exe`).

## Prerequisites
- **Windows 10/11**: The application is designed for Windows 10 and 11.
- **Administrator Rights**: OLTAB requires administrator privileges to create scheduled tasks.

## Running the Application
1. Open the application and provide administrator access (prompted via UAC).
2. Select the folder(s) you want to back up using the "Browse" button.
3. Choose the backup schedule (Daily, Weekly, Monthly).
4. Set the backup time.
5. Click "Schedule Backup" to add the backup task to the scheduler.

## Multiple Folder Backup
You can schedule multiple folders for backup by adding new configurations. Each configuration is saved in a single `config.json` file, and OLTAB will handle each folder's backup process independently.

## Running as Administrator
The application will automatically request administrator access. It is necessary to work with Windows Task Scheduler to create scheduled tasks.

## Logging
All operations, including successes and failures, are logged in `data/backup_log.txt`. The log files are automatically maintained and old logs are deleted based on the configured retention period.

### Log Example:
[09/05/2024 06:45] Starting the backup process for folder: C:\Emdep-2\MyProject 
[09/05/2024 06:49] Backup successful: backup_Emdep-2_2024-09-05_06-45.zip 
[09/05/2024 06:50] Completed backup process for folder: C:\Emdep-2\MyProject


## Versioning
### Version 2.0:
- Multi-folder backup support.
- Compression format option: ZIP or 7Z.
- Enhanced logging with retention policy.
- Improved PowerShell script to handle dynamic configuration.

### Version 1.0:
- Admin Privileges: Application runs with administrator rights via `app.manifest`.
- Logging Improvements: Added detailed logging for easier tracking.
- Folder Management: Ensured creation of necessary folders for logs.
- PowerShell Compression: Used `Compress-Archive` for ZIP compression.

## Known Issues
- Large folders may result in slower compression times. Consider using 7-Zip for faster compression in future versions.

## Future Improvements
- **Incremental Backups**: Planned for Version 3.0.
- **Advanced Scheduling Options**: Custom intervals and notifications.
- **Remote Backup Support**: Backup to cloud storage or network drives.

## Creator
- **Name**: Yelmehdi
- **Email**: younes.elmehdi@outlook.com
- **GitHub Profile**: [Your GitHub Profile](https://github.com/YounesElMehdi/)

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact
For any issues or suggestions, please create a new issue on the [GitHub repository](https://github.com/YounesElMehdi/OLTAB/).
