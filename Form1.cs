using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WindowsFormsApp1
{
    // Native Methods for icon extraction
    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
    }

    public partial class Form1 : Form
    {
        private const string REGISTRY_KEY = @"HKEY_CURRENT_USER\Software\WindowsFormsApp1\RestorePoint";
        private const string LAST_CREATE_DATE = "LastCreateDate";
        private const int DAYS_BETWEEN_CREATES = 90; // 3 months

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set form icon
            SetFormIcon();

            // Check if user can create restore point
            CheckCreateRestriction();

            // Check system status
            CheckSystemStatus();
        }

        private void CheckCreateRestriction()
        {
            try
            {
                string registryPath = REGISTRY_KEY.Replace(@"HKEY_CURRENT_USER\", "");
                object lastCreateObj = Registry.GetValue(REGISTRY_KEY, LAST_CREATE_DATE, null);

                if (lastCreateObj != null && DateTime.TryParse(lastCreateObj.ToString(), out DateTime lastCreateDate))
                {
                    TimeSpan timeSinceLastCreate = DateTime.Now - lastCreateDate;

                    if (timeSinceLastCreate.TotalDays < DAYS_BETWEEN_CREATES)
                    {
                        // Still within 3-month restriction
                        btnCreate.Enabled = false;
                        btnCreate.Text = $"⏳ Available in {(int)(DAYS_BETWEEN_CREATES - timeSinceLastCreate.TotalDays)} days";
                        lblStatus.Text = $"Next restore point available: {lastCreateDate.AddDays(DAYS_BETWEEN_CREATES):MMM dd, yyyy}";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        txtDescription.Enabled = false;

                        Debug.WriteLine($"Create restricted. Last create: {lastCreateDate}. Can create again: {lastCreateDate.AddDays(DAYS_BETWEEN_CREATES)}");
                    }
                    else
                    {
                        // 3 months passed, enable button
                        btnCreate.Enabled = true;
                        lblStatus.Text = "Ready";
                        lblStatus.ForeColor = System.Drawing.Color.Gray;
                        Debug.WriteLine("3-month restriction expired. Button enabled.");
                    }
                }
                else
                {
                    // First time creating
                    btnCreate.Enabled = true;
                    lblStatus.Text = "Ready";
                    lblStatus.ForeColor = System.Drawing.Color.Gray;
                    Debug.WriteLine("First time create allowed.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking restriction: {ex.Message}");
                // If error reading registry, allow create anyway
                btnCreate.Enabled = true;
            }
        }

        private void SaveLastCreateDate()
        {
            try
            {
                // Save to HKEY_CURRENT_USER
                Registry.SetValue(REGISTRY_KEY, LAST_CREATE_DATE, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), RegistryValueKind.String);
                Debug.WriteLine($"Last create date saved to registry: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving to registry: {ex.Message}");
                // Fallback: save to config file
                SaveToConfigFile();
            }
        }

        private void SaveToConfigFile()
        {
            try
            {
                string appDataPath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "WindowsFormsApp1"
                );

                if (!System.IO.Directory.Exists(appDataPath))
                    System.IO.Directory.CreateDirectory(appDataPath);

                string configFile = System.IO.Path.Combine(appDataPath, "restorepoint.cfg");
                System.IO.File.WriteAllText(configFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Debug.WriteLine($"Config saved to: {configFile}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving config: {ex.Message}");
            }
        }

        private void SetFormIcon()
        {
            try
            {
                // Try to use Backup icon from Windows system resources
                string[] possibleIconPaths = new[]
                {
                    System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "imageres.dll"),
                    System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll"),
                    System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "advapi32.dll")
                };

                foreach (var iconPath in possibleIconPaths)
                {
                    if (System.IO.File.Exists(iconPath))
                    {
                        try
                        {
                            var iconHandle = NativeMethods.ExtractIcon(IntPtr.Zero, iconPath, 222);
                            if (iconHandle != IntPtr.Zero)
                            {
                                this.Icon = System.Drawing.Icon.FromHandle(iconHandle);
                                Debug.WriteLine($"Icon loaded from: {iconPath}");
                                return;
                            }
                        }
                        catch { }
                    }
                }

                CreateDefaultIcon();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error setting icon: {ex.Message}");
                CreateDefaultIcon();
            }
        }

        private void CreateDefaultIcon()
        {
            try
            {
                using (var bitmap = new System.Drawing.Bitmap(32, 32))
                using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    graphics.Clear(System.Drawing.Color.White);

                    using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 120, 215)))
                    {
                        graphics.FillRectangle(brush, 2, 2, 28, 28);
                    }

                    using (var pen = new System.Drawing.Pen(System.Drawing.Color.White, 2))
                    {
                        graphics.DrawRectangle(pen, 6, 10, 20, 15);
                        graphics.DrawLine(pen, 6, 10, 14, 2);
                        graphics.DrawLine(pen, 14, 2, 14, 10);
                    }

                    this.Icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
                    Debug.WriteLine("Default icon created");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating default icon: {ex.Message}");
            }
        }

        private void CheckSystemStatus()
        {
            try
            {
                CheckSystemProtection();
                CheckDiskSpace();
                lblWindowsVersion.Text = $"✓ Windows: {Environment.OSVersion.VersionString}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking system status: {ex.Message}");
            }
        }

        private void CheckSystemProtection()
        {
            try
            {
                var psi = new ProcessStartInfo("powershell.exe")
                {
                    Arguments = "-NoProfile -Command \"Get-ComputerRestorePoint -ErrorAction SilentlyContinue | Select-Object -First 1\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();

                    if (!string.IsNullOrEmpty(output))
                    {
                        lblProtectionStatus.Text = "✓ System Protection: Enabled on C:";
                    }
                    else
                    {
                        lblProtectionStatus.Text = "⚠ System Protection: Disabled or No Restore Points";
                    }
                }
            }
            catch
            {
                lblProtectionStatus.Text = "⚠ Unable to check System Protection";
            }
        }

        private void CheckDiskSpace()
        {
            try
            {
                System.IO.DriveInfo drive = new System.IO.DriveInfo("C");
                double freeSpace = drive.AvailableFreeSpace / (1024 * 1024 * 1024);
                lblDiskSpace.Text = $"✓ Free Disk Space: {freeSpace:F1} GB";
            }
            catch
            {
                lblDiskSpace.Text = "⚠ Unable to check disk space";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customDescription = txtDescription.Text.Trim();
            string description = !string.IsNullOrEmpty(customDescription) 
                ? customDescription 
                : "Manual_Restore_" + DateTime.Now.ToString("ddMMyyyy_HHmm");

            string escapedDescription = description.Replace("'", "''");

            try
            {
                progressBar.Visible = true;
                btnCreate.Enabled = false;
                btnCancel.Enabled = false;
                lblStatus.Text = "Creating restore point... Please wait.";
                this.Cursor = Cursors.WaitCursor;

                int exitCode = RunElevatedWithExitCode("powershell.exe", 
                    $"-NoProfile -ExecutionPolicy Bypass -Command \"Checkpoint-Computer -Description '{escapedDescription}' -RestorePointType 'Manual' -ErrorAction Stop\"");

                progressBar.Visible = false;

                if (exitCode == 0)
                {
                    // Save the creation date
                    SaveLastCreateDate();

                    lblStatus.Text = "✓ Restore point created successfully!";
                    MessageBox.Show(
                        $"✓ Restore point created successfully!\n\nDescription: {description}\n\nNext restore point will be available in {DAYS_BETWEEN_CREATES} days.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Auto-close after 2 seconds
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => this.Close()));
                        }
                        else
                        {
                            this.Close();
                        }
                    });
                }
                else
                {
                    lblStatus.Text = $"✕ Failed with error code: {exitCode}";
                    MessageBox.Show(
                        $"✕ Failed to create restore point (Error: {exitCode})\n\nPlease check:\n• System Protection is enabled\n• Sufficient disk space available\n• Admin privileges granted",
                        "Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    btnCreate.Enabled = true;
                    btnCancel.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblStatus.Text = "✕ Error occurred";
                MessageBox.Show(
                    $"✕ Error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                btnCreate.Enabled = true;
                btnCancel.Enabled = true;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int RunElevatedWithExitCode(string fileName, string arguments)
        {
            try
            {
                string tempDir = System.IO.Path.GetTempPath();
                string scriptPath = System.IO.Path.Combine(
                    tempDir,
                    $"restore_{Guid.NewGuid().ToString().Substring(0, 8)}.ps1"
                );

                string scriptContent = $"Checkpoint-Computer -Description 'Manual_Restore_{DateTime.Now:ddMMyyyy_HHmm}' -RestorePointType Manual -ErrorAction Stop; exit $LASTEXITCODE";
                System.IO.File.WriteAllText(scriptPath, scriptContent);

                var psi = new ProcessStartInfo("powershell.exe")
                {
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        int exitCode = process.ExitCode;
                        Debug.WriteLine($"PowerShell Exit Code: {exitCode}");

                        try 
                        { 
                            System.IO.File.Delete(scriptPath);
                            Debug.WriteLine($"Cleaned up temp script: {scriptPath}");
                        } 
                        catch (Exception cleanupEx) 
                        { 
                            Debug.WriteLine($"Warning: Could not delete temp script: {cleanupEx.Message}"); 
                        }

                        return exitCode;
                    }

                    Debug.WriteLine("PowerShell process is null (UAC may have been cancelled)");
                    try 
                    { 
                        System.IO.File.Delete(scriptPath); 
                    } 
                    catch { }

                    return -1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                return -1;
            }
        }
    }
}
