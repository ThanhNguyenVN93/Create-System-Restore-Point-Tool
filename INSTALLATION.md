# Installation Guide

## 📋 Prerequisites

Before installing System Restore Point Creator, ensure your system meets these requirements:

### System Requirements
- **Operating System**: Windows 7 SP1 or later (Windows 10/11 recommended)
- **Processor**: Intel Core 2 Duo or equivalent
- **RAM**: 512 MB minimum (2 GB recommended)
- **Disk Space**: 50 MB for application, additional space for restore points
- **Administrator Rights**: Required to create restore points

### Software Requirements
- **.NET Framework 4.8**: [Download](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- **System Protection**: Must be enabled on C: drive

## ✅ Pre-Installation Checklist

### 1. Check Windows Version
```powershell
# Open PowerShell and run:
[System.Environment]::OSVersion.VersionString
```

Should show Windows 7 SP1 or later.

### 2. Verify .NET Framework 4.8
```powershell
# Check if .NET Framework 4.8 is installed:
Get-ChildItem 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' | 
Get-ItemPropertyValue -Name Release | 
ForEach-Object { if ($_ -ge 533320) { "✓ .NET 4.8 installed" } else { "✗ .NET 4.8 not found" } }
```

If .NET 4.8 is not installed, [download and install it](https://dotnet.microsoft.com/download/dotnet-framework/net48).

### 3. Enable System Protection
```powershell
# Check if System Protection is enabled:
Get-ComputerRestorePoint -ErrorAction SilentlyContinue | Select-Object -First 1

# If no output, System Protection is disabled
```

**To enable System Protection:**
1. Right-click **This PC** → **Properties**
2. Click **System Protection** (left sidebar)
3. Select **C:** drive → Click **Configure**
4. Choose **Turn on system protection**
5. Set maximum storage space (15-30% of drive recommended)
6. Click **OK** and **Apply**

### 4. Enable Administrator Account
```powershell
# Check if you have admin rights:
$isAdmin = [Security.Principal.WindowsIdentity]::GetCurrent().Groups -contains 
    [Security.Principal.SecurityIdentifier]'S-1-5-32-544'
if ($isAdmin) { "✓ Administrator" } else { "✗ Not Administrator" }
```

## 🚀 Installation Steps

### Option 1: Direct Download (Recommended)

1. **Download Release**
   - Go to [GitHub Releases](https://github.com/yourusername/SystemRestorePointCreator/releases)
   - Download the latest `WindowsFormsApp1.exe` or `.zip` file

2. **Create Installation Folder**
   ```powershell
   New-Item -ItemType Directory -Path "C:\Program Files\SystemRestorePointCreator" -Force
   ```

3. **Copy Application**
   - Extract the `.zip` file (if applicable)
   - Copy `WindowsFormsApp1.exe` to the installation folder

4. **Create Shortcut (Optional)**
   ```powershell
   # Create desktop shortcut
   $WshShell = New-Object -ComObject WScript.Shell
   $Shortcut = $WshShell.CreateShortcut("$env:PUBLIC\Desktop\System Restore Point Creator.lnk")
   $Shortcut.TargetPath = "C:\Program Files\SystemRestorePointCreator\WindowsFormsApp1.exe"
   $Shortcut.IconLocation = "C:\Program Files\SystemRestorePointCreator\WindowsFormsApp1.exe,0"
   $Shortcut.Save()
   ```

5. **First Run**
   - Right-click application → **Run as administrator**
   - Click "Yes" on the UAC prompt
   - Application should open

### Option 2: Build from Source

**For developers who want to build from source:**

1. **Install Visual Studio**
   - Download [Visual Studio 2022 Community](https://visualstudio.microsoft.com/vs/community/) (free)
   - Install with ".NET desktop development" workload

2. **Clone Repository**
   ```powershell
   git clone https://github.com/yourusername/SystemRestorePointCreator.git
   cd SystemRestorePointCreator
   ```

3. **Open Solution**
   - Open `WindowsFormsApp1.sln` in Visual Studio
   - Wait for project to load

4. **Build Application**
   - Press **Ctrl+Shift+B** to build
   - Or: **Build** menu → **Build Solution**

5. **Run Application**
   - Press **F5** to run
   - Or: **Debug** menu → **Start Debugging**

6. **Release Build (Optional)**
   - Change configuration from **Debug** to **Release**
   - Press **Ctrl+Shift+B** to build
   - Executable in: `bin\Release\WindowsFormsApp1.exe`

## 🔧 Post-Installation Configuration

### 1. Create Application Folder in AppData
```powershell
# Create config folder (done automatically on first run)
$AppData = "$env:APPDATA\WindowsFormsApp1"
if (-not (Test-Path $AppData)) {
    New-Item -ItemType Directory -Path $AppData -Force
}
```

### 2. Verify Registry Access
```powershell
# Check if registry key can be created
New-Item -Path "HKCU:\Software\WindowsFormsApp1\RestorePoint" -Force -ErrorAction SilentlyContinue
```

### 3. Set File Permissions
```powershell
# Ensure user has read/write permissions
icacls "C:\Program Files\SystemRestorePointCreator" /grant:r "$env:USERNAME:(F)" /t
```

## ✨ Initial Setup

When you run the application for the first time:

1. **System Check**: App checks system protection status and disk space
2. **Allow UAC**: Click "Yes" when UAC prompt appears
3. **Create Restore Point**: Click the blue "Create Restore Point" button
4. **Success**: If successful, app closes after 2 seconds

## 🎯 Quick Start

1. **Run the application**
   ```bash
   WindowsFormsApp1.exe
   ```

2. **Review system status** (displayed in the info box)

3. **Enter custom description** (optional)

4. **Click "Create Restore Point"**

5. **Approve UAC prompt**

6. **Wait for completion** (progress bar shows status)

## ❓ Troubleshooting Installation

### Problem: ".NET Framework 4.8 not found"
**Solution**: [Download and install .NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

### Problem: "Access Denied" when copying files
**Solution**: Run Command Prompt as Administrator:
```cmd
REM Copy to Program Files
copy WindowsFormsApp1.exe "C:\Program Files\SystemRestorePointCreator\"
```

### Problem: Application won't run
**Solution**: Try running from Command Prompt to see error message:
```cmd
cd C:\Program Files\SystemRestorePointCreator
WindowsFormsApp1.exe
```

### Problem: "System Protection" error
**Solution**: Enable System Protection (see Pre-Installation section)

### Problem: "Admin privileges required"
**Solution**: Always run application as Administrator:
- Right-click application → **Run as administrator**

## 📁 File Structure

After installation, the application will create:

```
C:\Program Files\SystemRestorePointCreator\
├── WindowsFormsApp1.exe
└── [Other dependencies]

%APPDATA%\WindowsFormsApp1\
└── restorepoint.cfg (if Registry access fails)

HKEY_CURRENT_USER\Software\WindowsFormsApp1\RestorePoint\
└── LastCreateDate (stores timestamp)
```

## 🔄 Updating the Application

### From Release
1. Download latest version
2. Replace `WindowsFormsApp1.exe`
3. Restart application

### From Source
1. Pull latest changes: `git pull`
2. Rebuild in Visual Studio: **Ctrl+Shift+B**
3. Run the new version

## 🗑️ Uninstallation

### Remove Application Files
```powershell
# Remove installation folder
Remove-Item -Path "C:\Program Files\SystemRestorePointCreator" -Recurse -Force
```

### Remove Shortcuts
```powershell
# Remove desktop shortcut
Remove-Item -Path "$env:PUBLIC\Desktop\System Restore Point Creator.lnk" -Force
```

### Remove Configuration Files (Optional)
```powershell
# Remove AppData folder
Remove-Item -Path "$env:APPDATA\WindowsFormsApp1" -Recurse -Force

# Remove Registry entry
Remove-Item -Path "HKCU:\Software\WindowsFormsApp1" -Recurse -Force
```

## 📞 Need Help?

If you encounter issues:

1. **Check Troubleshooting**: See "Troubleshooting Installation" section
2. **GitHub Issues**: [Open an issue](https://github.com/yourusername/SystemRestorePointCreator/issues)
3. **GitHub Discussions**: [Ask a question](https://github.com/yourusername/SystemRestorePointCreator/discussions)

---

**Last Updated**: 2024-03-20  
**Installation Guide Version**: 1.0
