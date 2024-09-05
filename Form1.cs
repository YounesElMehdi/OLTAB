using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;  // Required for working with Windows Task Scheduler
using Newtonsoft.Json;  // Used for reading and writing config.json

namespace OLTAB_Manager
{
    public partial class Form1 : Form
    {
        // Define paths for the config file, log file, and script folder
        string configFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "backup_log.txt");
        string scriptFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");

        public Form1()
        {
            InitializeComponent();  // Initializes the components of the WinForms UI
            InitializeProjectFolders();  // Ensure necessary folders exist
            InitializeScheduleOptions();  // Initialize the scheduling options (Daily, Weekly, Monthly)
        }

        // Ensure "data" and "scripts" folders exist
        private void InitializeProjectFolders()
        {
            // Create the "data" folder for config and logs if it doesn't exist
            if (!Directory.Exists(configFolderPath))
            {
                Directory.CreateDirectory(configFolderPath);
            }

            // Create the "scripts" folder for the PowerShell script if it doesn't exist
            if (!Directory.Exists(scriptFolderPath))
            {
                Directory.CreateDirectory(scriptFolderPath);
            }
        }

        // Initialize schedule options (Daily, Weekly, Monthly)
        private void InitializeScheduleOptions()
        {
            cboSchedule.Items.AddRange(new string[] { "Daily", "Weekly", "Monthly" });
            cboSchedule.SelectedIndex = 0;  // Default to "Daily"
        }

        // Open folder browser to select folder for backup
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = fbd.SelectedPath;  // Display selected folder path
                }
            }
        }

        // Schedule the backup task
        private void btnSchedule_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;
            string scheduleOption = cboSchedule.SelectedItem.ToString();
            string time = timePicker.Value.ToString("HH:mm");

            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                MessageBox.Show("Please select a valid folder.", "Invalid Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save user settings to config.json
            SaveConfig(folderPath, scheduleOption, time);

            try
            {
                // Schedule the backup task
                ScheduleBackupTask(folderPath, scheduleOption, time);
                MessageBox.Show("Backup Scheduled Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to schedule backup. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Save user settings to config.json
        private void SaveConfig(string folderPath, string scheduleOption, string time)
        {
            var configData = new
            {
                FolderPath = folderPath,
                ScheduleOption = scheduleOption,
                Time = time
            };

            // Ensure the "data" folder exists
            if (!Directory.Exists(configFolderPath))
            {
                Directory.CreateDirectory(configFolderPath);
            }

            // Save config data to config.json
            string configFilePath = Path.Combine(configFolderPath, "config.json");
            string configJson = JsonConvert.SerializeObject(configData, Formatting.Indented);
            File.WriteAllText(configFilePath, configJson);
        }

        // Schedule the backup task in Windows Task Scheduler
        private void ScheduleBackupTask(string folderPath, string scheduleOption, string time)
        {
            using (TaskService ts = new TaskService())
            {
                string taskName = $"OLTAB_Backup_Task_{Path.GetFileName(folderPath)}";

                // If a task with the same name already exists, delete it
                if (ts.GetTask(taskName) != null)
                {
                    ts.RootFolder.DeleteTask(taskName);
                }

                // Create a new task definition
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = $"Scheduled Backup Task for {folderPath}";

                // Set the start time for the task
                string[] timeParts = time.Split(':');
                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);
                DateTime startBoundary = DateTime.Today.AddHours(hour).AddMinutes(minute);

                if (startBoundary < DateTime.Now)
                {
                    startBoundary = startBoundary.AddDays(1);
                }

                // Set the trigger based on the user's schedule selection
                switch (scheduleOption)
                {
                    case "Daily":
                        td.Triggers.Add(new DailyTrigger { DaysInterval = 1, StartBoundary = startBoundary });
                        break;
                    case "Weekly":
                        td.Triggers.Add(new WeeklyTrigger { WeeksInterval = 1, StartBoundary = startBoundary });
                        break;
                    case "Monthly":
                        td.Triggers.Add(new MonthlyTrigger { StartBoundary = startBoundary });
                        break;
                }

                // Set the action to run the PowerShell script with the folder path as an argument
                string scriptPath = Path.Combine(scriptFolderPath, "compressor.ps1");
                td.Actions.Add(new ExecAction("powershell.exe", $"-ExecutionPolicy Bypass -File \"{scriptPath}\" -FolderPath \"{folderPath}\""));

                // Register the task
                ts.RootFolder.RegisterTaskDefinition(taskName, td);
            }
        }
    }
}
