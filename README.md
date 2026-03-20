# 💾 System Restore Point Creator

A professional Windows Forms application that makes creating system restore points easy and safe. Create manual restore points before making system changes with a modern, user-friendly interface.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-7.3-green)](https://docs.microsoft.com/en-us/dotnet/csharp/)

## ✨ Features

- **🖥️ Professional UI** - Modern Windows Forms interface with Segoe UI font
- **📊 System Status Display** - Real-time system information:
  - System Protection status
  - Free disk space
  - Windows version
- **💾 Create Restore Points** - One-click restore point creation with admin elevation
- **🔒 3-Month Restriction** - Automatic limitation of one restore point per 3 months
- **⏰ Auto-Close** - Application automatically closes 2 seconds after successful creation
- **🛡️ Error Handling** - Comprehensive error messages and fallback mechanisms
- **📝 Custom Descriptions** - Add custom names to your restore points
- **🎨 Dynamic Icons** - Professional icons loaded from system resources
- **💾 Registry Persistence** - Creates registry entries in `HKEY_CURRENT_USER\Software\WindowsFormsApp1`

## 🖼️ Screenshots

```
┌─────────────────────────────────────────┐
│  💾 System Restore Point Creator        │
├─────────────────────────────────────────┤
│                                         │
│  Custom Description (Optional):         │
│  [Manual Restore Point................] │
│                                         │
│  📊 System Status                       │
│  ├─ ✓ System Protection: Enabled on C: │
│  ├─ ✓ Free Disk Space: 150.5 GB        │
│  └─ ✓ Windows: Windows 10 19045.0      │
│                                         │
│  [✓ Create Restore Point] [✕ Exit]    │
│                                         │
│  Ready                                  │
└─────────────────────────────────────────┘
```

## 🚀 Installation

### Requirements
- **Windows 7** or later
- **.NET Framework 4.8**
- **Administrator privileges** (for creating restore points)
- **System Protection enabled** on C: drive

### Download
1. Download the latest release from [Releases](https://github.com/yourusername/SystemRestorePointCreator/releases)
2. Extract the files
3. Run `WindowsFormsApp1.exe`

### Build from Source
```bash
# Clone the repository
git clone https://github.com/yourusername/SystemRestorePointCreator.git
cd SystemRestorePointCreator

# Build with Visual Studio 2022+
# Open WindowsFormsApp1.sln
# Press Ctrl+Shift+B to build
# Press F5 to run
```

## 📋 Usage

### Basic Usage
1. **Launch the application** - Run `WindowsFormsApp1.exe`
2. **(Optional) Enter custom description** - Default is "Manual Restore Point"
3. **Click "Create Restore Point"** button
4. **Approve UAC prompt** - Admin elevation is required
5. **Wait for completion** - Progress bar will show the operation status
6. **Success message** - Application will auto-close after 2 seconds

### 3-Month Restriction
- First restore point: **Always allowed**
- Subsequent attempts within 90 days: **Button will be disabled**
- Status shows: **"⏳ Available in X days"**
- After 90 days: **Button re-enables automatically**

### System Information
The application displays real-time system information:
- **System Protection**: Shows if enabled or disabled
- **Free Disk Space**: Available space on C: drive in GB
- **Windows Version**: Current Windows OS version

## 🔧 Advanced Configuration

### Registry Location
```
HKEY_CURRENT_USER\Software\WindowsFormsApp1\RestorePoint
```

**Values:**
- `LastCreateDate` (REG_SZ): Last restore point creation timestamp
  - Format: `yyyy-MM-dd HH:mm:ss`
  - Example: `2024-03-20 14:30:45`

### Configuration File (Fallback)
If registry access fails, a config file is created:
```
%APPDATA%\WindowsFormsApp1\restorepoint.cfg
```

### Resetting the Restriction
To reset the 3-month timer, delete the registry value:
```powershell
# PowerShell (as Administrator)
Remove-Item -Path 'Registry::HKEY_CURRENT_USER\Software\WindowsFormsApp1\RestorePoint' -Force
```

Or delete the config file:
```powershell
Remove-Item -Path "$env:APPDATA\WindowsFormsApp1\restorepoint.cfg" -Force
```

## 📝 System Requirements

| Requirement | Minimum | Recommended |
|-------------|---------|-------------|
| OS | Windows 7 SP1 | Windows 10/11 |
| .NET Framework | 4.8 | 4.8+ |
| RAM | 512 MB | 2 GB |
| Disk Space | 50 MB | 100 MB |
| Admin Rights | Required | Required |
| System Protection | Required | Enabled |

## 🐛 Troubleshooting

### Problem: "System Protection: Disabled or No Restore Points"
**Solution:** Enable System Protection on C: drive
1. Right-click "This PC" → Properties
2. Click "System Protection"
3. Select C: drive → Click "Configure"
4. Enable "Turn on system protection"
5. Set maximum storage space
6. Click OK and retry

### Problem: "Button is disabled / Available in X days"
**Solution:** Wait for the 90-day period to expire
- Check "Next restore point available: [Date]"
- To reset: Delete registry entry or config file (see Advanced Configuration)

### Problem: "Insufficient disk space"
**Solution:** Free up disk space on C: drive
- Run Disk Cleanup: `cleanmgr`
- Or: Delete unnecessary files/folders
- Recommended: At least 100 MB free space

### Problem: UAC dialog doesn't appear
**Solution:** Check User Account Control settings
1. Search for "UAC" in Windows
2. Move slider to "Always notify"
3. Restart the application

### Problem: "Access Denied" error
**Solution:** Run as Administrator
1. Right-click `WindowsFormsApp1.exe`
2. Select "Run as administrator"
3. Retry creating restore point

## 💻 Technical Details

### Architecture
- **Language**: C# 7.3
- **Framework**: .NET Framework 4.8
- **UI Framework**: Windows Forms
- **Process Management**: System.Diagnostics.Process
- **Registry Access**: Microsoft.Win32.Registry

### Key Technologies
- **PowerShell Integration**: For executing restore point commands
- **UAC Elevation**: Using Verb="runas" for admin privileges
- **Registry Persistence**: HKEY_CURRENT_USER storage
- **System Icon Extraction**: Shell32.dll icon resources

### Code Structure
```
Form1.cs
├─ Form1_Load()              - Initialize and check restrictions
├─ CheckCreateRestriction()  - 3-month limitation check
├─ CheckSystemStatus()       - Display system information
├─ button1_Click()           - Create restore point handler
├─ SaveLastCreateDate()      - Persist creation timestamp
├─ RunElevatedWithExitCode() - Execute PowerShell as admin
└─ SetFormIcon()             - Load and set application icon

Form1.Designer.cs
└─ InitializeComponent()     - UI layout and styling
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Steps to contribute:
1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ⚠️ Disclaimer

- **Use at your own risk** - Always create backups before making system changes
- **Admin privileges required** - This application requires administrator access
- **Windows only** - Designed and tested for Windows 7, 10, and 11
- **System Protection required** - Must be enabled on C: drive
- **No warranty** - The author is not responsible for system issues

## 🙏 Acknowledgments

- Microsoft Windows System Restore documentation
- .NET Framework documentation
- Community feedback and bug reports

## 📞 Support

For issues, questions, or suggestions:
- **GitHub Issues**: [Report a bug](https://github.com/yourusername/SystemRestorePointCreator/issues)
- **GitHub Discussions**: [Start a discussion](https://github.com/yourusername/SystemRestorePointCreator/discussions)

## 🔄 Version History

### v1.0.0 (Current)
- ✅ Initial release
- ✅ Create restore points
- ✅ 3-month restriction
- ✅ System status display
- ✅ Professional UI
- ✅ Auto-close on success
- ✅ Registry persistence

## 📅 Roadmap

- [ ] Add recent restore points list view
- [ ] Add restore point browser
- [ ] Support other drives (D:, E:, etc.)
- [ ] Dark mode support
- [ ] Multiple language support
- [ ] Settings dialog
- [ ] Scheduled creation option
- [ ] Tray icon support

## 🌟 If you like this project, please give it a star! ⭐

---

**Created with ❤️ by [Your Name]**

Last updated: 2024-03-20
