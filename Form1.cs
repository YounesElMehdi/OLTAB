using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;  
using Newtonsoft.Json; 

namespace OLTAB_Manager
{
    public partial class Form1 : Form
    {
        string configFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "backup_log.txt");
        string scriptFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");

        public Form1()
        {
            InitializeComponent();  
            InitializeProjectFolders();  
            InitializeScheduleOptions();  
        }

        private void InitializeProjectFolders()
        {
            if (!Directory.Exists(configFolderPath))
            {
                Directory.CreateDirectory(configFolderPath);
            }

            if (!Directory.Exists(scriptFolderPath))
            {
                Directory.CreateDirectory(scriptFolderPath);
            }
        }

        private void InitializeScheduleOptions()
        {
            cboSchedule.Items.AddRange(new string[] { "Daily", "Weekly", "Monthly" });
            cboSchedule.SelectedIndex = 0; 
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = fbd.SelectedPath;
                }
            }
        }

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

            SaveConfig(folderPath, scheduleOption, time);

            try
            {
                ScheduleBackupTask(folderPath, scheduleOption, time);
                MessageBox.Show("Backup Scheduled Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to schedule backup. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConfig(string folderPath, string scheduleOption, string time)
        {
            var configData = new
            {
                FolderPath = folderPath,
                ScheduleOption = scheduleOption,
                Time = time
            };

            if (!Directory.Exists(configFolderPath))
            {
                Directory.CreateDirectory(configFolderPath);
            }

            string configFilePath = Path.Combine(configFolderPath, "config.json");
            string configJson = JsonConvert.SerializeObject(configData, Formatting.Indented);
            File.WriteAllText(configFilePath, configJson);
        }

        private void ScheduleBackupTask(string folderPath, string scheduleOption, string time)
        {
            using (TaskService ts = new TaskService())
            {
                string taskName = $"OLTAB_Backup_Task_{Path.GetFileName(folderPath)}";

                try
                {
                    if (ts.GetTask(taskName) != null)
                    {
                        ts.RootFolder.DeleteTask(taskName);
                    }

                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = $"Scheduled Backup Task for {folderPath}";

                    string[] timeParts = time.Split(':');
                    int hour = int.Parse(timeParts[0]);
                    int minute = int.Parse(timeParts[1]);
                    DateTime startBoundary = DateTime.Today.AddHours(hour).AddMinutes(minute);

                    if (startBoundary < DateTime.Now)
                    {
                        startBoundary = startBoundary.AddDays(1);
                    }

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

                    td.Principal.RunLevel = TaskRunLevel.Highest; 

                    string scriptPath = Path.Combine(scriptFolderPath, "compressor.ps1");
                    td.Actions.Add(new ExecAction("powershell.exe", $"-ExecutionPolicy Bypass -File \"{scriptPath}\" -FolderPath \"{folderPath}\""));

                    ts.RootFolder.RegisterTaskDefinition(taskName, td);

                    File.AppendAllText(logFilePath, $"[{DateTime.Now}] Task scheduled: {taskName} for {scheduleOption} at {time}\n");
                }
                catch (Exception ex)
                {
                    File.AppendAllText(logFilePath, $"[{DateTime.Now}] Failed to schedule task: {taskName}. Error: {ex.Message}\n");
                    throw;
                }
            }
        }
    }
}
