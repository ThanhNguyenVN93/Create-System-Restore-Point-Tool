# Security Policy

## Reporting a Vulnerability

**DO NOT** create a public GitHub issue to report a security vulnerability. Instead, please email your findings to [your-email@example.com] with the subject line "Security Vulnerability Report - System Restore Point Creator".

Please include the following information in your report:

- **Description**: A clear description of the vulnerability
- **Affected Versions**: Which version(s) are affected
- **Steps to Reproduce**: Step-by-step instructions to reproduce the vulnerability
- **Impact**: Explanation of the potential impact
- **Proof of Concept**: Code or screenshots demonstrating the vulnerability (optional)
- **Your Contact Information**: So we can follow up with you

## What to Expect

1. **Acknowledgment**: We'll acknowledge receipt of your vulnerability report within 48 hours
2. **Investigation**: We'll investigate and determine the severity
3. **Timeline**: 
   - Critical: 7 days
   - High: 14 days
   - Medium: 30 days
   - Low: 90 days
4. **Notification**: We'll notify you when a fix is released
5. **Credit**: With your permission, we'll credit you in the security advisory

## Supported Versions

We will provide security updates for the following versions:

| Version | Supported          | End of Support |
|---------|-------------------|-----------------|
| 1.x     | ✅ Yes            | TBD            |
| 0.x     | ❌ No (EOL)       | N/A            |

## Security Guidelines

To help keep this project secure, please follow these guidelines:

### For Users
- Keep your Windows operating system updated
- Enable Windows Defender or your preferred antivirus
- Use the latest .NET Framework 4.8 or later
- Only download from official GitHub releases
- Report suspicious behavior immediately

### For Developers
- Never commit sensitive information (API keys, passwords, tokens)
- Use secure coding practices
- Validate all user input
- Follow the principle of least privilege
- Keep dependencies updated

## Known Security Considerations

### Registry Access
- The application writes to `HKEY_CURRENT_USER\Software\WindowsFormsApp1`
- This is per-user registry access (no admin required for registry writes)
- No sensitive data is stored in registry

### PowerShell Execution
- PowerShell commands are executed with UAC elevation
- Commands are stored in temporary files in `%TEMP%`
- Temporary files are deleted after execution
- File permissions are inherited from the temp folder

### File System Access
- Application reads from `%APPDATA%` (user data folder)
- Application may create config files in `%APPDATA%\WindowsFormsApp1`
- No write access to Windows system directories required

## Security Best Practices

When using this application:

1. **Always run with latest Windows updates**
   ```powershell
   # Check for Windows updates
   Get-WindowsUpdate
   ```

2. **Verify System Protection is enabled**
   - Right-click "This PC" → Properties
   - Click "System Protection"
   - Verify protection is turned on for C: drive

3. **Review restore point descriptions**
   - Only use meaningful descriptions
   - Avoid storing sensitive information in descriptions

4. **Monitor disk space**
   - Ensure sufficient free space on C: drive
   - Restore points require significant disk space

5. **Use only official releases**
   - Download only from https://github.com/yourusername/SystemRestorePointCreator/releases
   - Verify file integrity if possible

## Vulnerability Disclosure

When a vulnerability is confirmed and fixed:

1. **Patch Release**: A new version will be released with the fix
2. **Security Advisory**: A GitHub security advisory will be published
3. **Notification**: Users will be notified through GitHub
4. **Credit**: Security researchers will be credited (if desired)

## Security Advisories

Security advisories for this project can be found at:
https://github.com/yourusername/SystemRestorePointCreator/security/advisories

## Third-Party Dependencies

This project has minimal dependencies:
- **.NET Framework 4.8** - Microsoft supported product
- **Windows Forms** - Built-in Microsoft library
- **PowerShell** - Built-in Microsoft tool

All dependencies are maintained by Microsoft and receive regular security updates.

## Responsible Disclosure

We follow the principle of responsible disclosure:

1. **Embargoed disclosure** - Vulnerability is kept confidential until fix is released
2. **Public disclosure** - After fix is released, full details are disclosed
3. **Coordinated disclosure** - Coordination with Microsoft if vulnerabilities are found in dependencies

## Security Audit

This project undergoes periodic security review. The last security audit was conducted on:
- **Date**: [TBD]
- **Conducted by**: [TBD]
- **Results**: [TBD]

## Additional Resources

- [Microsoft Security Documentation](https://docs.microsoft.com/en-us/windows/security/)
- [OWASP Security Guidelines](https://owasp.org/www-project-secure-coding-practices-quick-reference-guide/)
- [CWE - Common Weakness Enumeration](https://cwe.mitre.org/)

## Contact

For security inquiries:
- **Email**: [your-email@example.com]
- **GPG Key**: [TBD - if applicable]

---

**Last Updated**: 2024-03-20  
**Version**: 1.0  
**Policy Review**: Annually or when significant changes are made
