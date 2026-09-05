using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        private const int MinSupportedMajor = 6;
        private const int MinSupportedMinor = 1; // Windows 7
        private const int MinSupportedBuild = 7601; // Windows 7 SP1

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!IsSupportedOSVersion())
            {
                MessageBox.Show(
                    "This application requires Windows 7 SP1 or later.",
                    "Unsupported Operating System",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            IconGeneratorHelper.GenerateAndSaveIcon("app.ico"); // UNCOMMENT THIS

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Create_System_Restore_Point_Tool());
        }

        private static bool IsSupportedOSVersion()
        {
            var os = Environment.OSVersion;
            if (os.Platform != PlatformID.Win32NT)
                return false;

            var v = os.Version;

            if (v.Major > MinSupportedMajor)
                return true; // Windows 8 and later (6.2+), Windows 10/11 (10.x)

            if (v.Major == MinSupportedMajor && v.Minor > MinSupportedMinor)
                return true; // Windows 8 / 8.1 (6.2 / 6.3)

            if (v.Major == MinSupportedMajor && v.Minor == MinSupportedMinor)
                return v.Build >= MinSupportedBuild; // Windows 7: require SP1

            return false; // Windows Vista and older
        }
    }
}
