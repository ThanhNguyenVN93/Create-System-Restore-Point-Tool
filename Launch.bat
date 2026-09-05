@echo off
setlocal enabledelayedexpansion

set "APP_DIR=%~dp0"
set "NET48_EXE=%APP_DIR%Create System Restore Point Tool.exe"
set "NET35_EXE=%APP_DIR%net35\Create System Restore Point Tool.exe"

rem --- 1) Prefer .NET Framework 4.8 (release >= 528040) if present ---
set "NET4_RELEASE="
for /f "tokens=3" %%a in ('reg query "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release 2^>nul ^| findstr /i "Release"') do set "NET4_RELEASE=%%a"

if defined NET4_RELEASE (
    if %NET4_RELEASE% GEQ 528040 (
        if exist "%NET48_EXE%" (
            start "" "%NET48_EXE%"
            exit /b 0
        )
    )
)

rem --- 2) Fall back to .NET Framework 3.5 if already enabled ---
reg query "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5" /v Install 2>nul | findstr /i "0x1" >nul
if %errorlevel%==0 (
    if exist "%NET35_EXE%" (
        start "" "%NET35_EXE%"
        exit /b 0
    )
)

rem --- 3) Neither present - try to silently enable .NET Framework 3.5 (needs Admin + Windows Update or install source) ---
echo .NET Framework 4.8 was not found, and .NET Framework 3.5 is not enabled.
echo Attempting to enable .NET Framework 3.5 automatically. You may see a UAC prompt...
powershell -NoProfile -ExecutionPolicy Bypass -Command "Start-Process -FilePath 'dism.exe' -ArgumentList '/Online','/Enable-Feature','/FeatureName:NetFx3','/All','/NoRestart','/Quiet' -Verb RunAs -Wait" >nul 2>&1

reg query "HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5" /v Install 2>nul | findstr /i "0x1" >nul
if %errorlevel%==0 (
    if exist "%NET35_EXE%" (
        start "" "%NET35_EXE%"
        exit /b 0
    )
)

rem --- 4) Everything failed - tell the user clearly ---
echo.
echo ============================================================
echo   Could not find or enable a compatible .NET Framework.
echo.
echo   Please do ONE of the following, then run this app again:
echo    - Install .NET Framework 4.8:
echo      https://dotnet.microsoft.com/download/dotnet-framework/net48
echo    - Or enable ".NET Framework 3.5" manually:
echo      Control Panel ^> Programs ^> Turn Windows features on or off
echo      ^> check ".NET Framework 3.5 (includes .NET 2.0 and 3.0)"
echo      (requires an Internet connection or Windows installation media)
echo ============================================================
echo.
pause
exit /b 1
