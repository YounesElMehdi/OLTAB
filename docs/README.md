# OLTAB (Offline Test Auto Backup) - Version 1.0

## Overview

**OLTAB (Offline Test Auto Backup)** is a simple Windows application that allows users to schedule automatic backups of their folders. The application utilizes **Windows Task Scheduler** to automate the process and compresses files into ZIP format using **PowerShell**.

## Key Features

- **Automated Backups**: Schedule daily, weekly, or monthly backups using the Windows Task Scheduler.
- **Compressed Archives**: Backups are saved as compressed ZIP files to save disk space.
- **Custom Backup Folder**: Choose which folder you want to back up and schedule it easily.
- **Detailed Logging**: All actions, successes, and errors are logged to a `backup_log.txt` file for easy debugging and tracking.
- **Admin Privileges**: The application automatically requests administrator privileges to work with Windows Task Scheduler.

## Installation

1. **Download the latest release** from the [Releases page](https://github.com/YounesElMehdi/OLTAB/releases).
2. Extract the ZIP file to a desired location on your computer.
3. Run the application executable (`OLTAB.exe`).

### Prerequisites

- **Windows 10/11**: This application is designed for Windows 10 and 11.
- **Administrator Rights**: OLTAB requires administrator privileges to schedule tasks.

## Running the Application

When running the application:

1. The application will prompt for **administrator access** via the **User Account Control (UAC)**. This is required to interact with Windows Task Scheduler.
2. Select the folder you want to back up by clicking the **Browse** button.
3. Choose a backup schedule (Daily, Weekly, Monthly).
4. Set the time for the backup to occur.
5. Click **Schedule Backup** to create the task.

### Running as Administrator

The application automatically runs with administrator privileges, so you don't need to worry about manually configuring this.

## Logging

All events, including successes and errors, are logged in `data/backup_log.txt`. This log file is automatically created and updated with each backup process.

### Log Example:
[09/05/2024 06:45] Starting the backup process for folder: C:\Emdep-2\MyProject 
[09/05/2024 06:49] Backup successful: backup_Emdep-2_2024-09-05_06-45.zip 
[09/05/2024 06:50] Completed backup process for folder: C:\Emdep-2\MyProject

## Versioning

### Version 1.0

- **Admin Privileges**: Application now runs with administrator rights via `app.manifest`.
- **Logging Improvements**: Detailed logging has been added for easier tracking of operations.
- **Folder Management**: Ensured creation of necessary folders (e.g., `data` for logs) if they don't exist.
- **PowerShell Compression**: Optimized folder compression using PowerShell's `Compress-Archive`.

## Known Issues

- Compression may be slower for larger folders. Consider using external tools like **7-Zip** for better performance in future versions.

## Future Improvements

- **Incremental Backups**: Planned for Version 2.0.
- **Improved Task Scheduling Options**: Custom intervals and notifications will be added in future updates.

## Creator

- **Name**: Yelmehdi
- **Email**: [younes.elmehdi@outlook.com](mailto:younes.elmehdi@outlook.com)
- **GitHub Profile**: [Your GitHub Profile](https://github.com/YounesElMehdi)

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/YounesElMehdi/OLTAB/blob/master/LICENSE) file for details.

## Contact

For any issues or suggestions, please create a new issue on the [GitHub repository](https://github.com/YounesElMehdi/OLTAB/issues).
