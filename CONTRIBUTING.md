# Contributing to System Restore Point Creator

Thank you for your interest in contributing to this project! 🎉

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code.

## How to Contribute

### Reporting Bugs

Before creating bug reports, please check the issue list as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible:

- **Use a clear and descriptive title**
- **Describe the exact steps which reproduce the problem** in as many details as possible
- **Provide specific examples to demonstrate the steps**
- **Describe the behavior you observed after following the steps** and point out what exactly is the problem with that behavior
- **Explain which behavior you expected to see instead and why**
- **Include screenshots and animated GIFs if possible**
- **Include your environment details**:
  - Windows version (7, 10, 11, Server 2019, etc.)
  - .NET Framework version
  - Visual Studio version (if building from source)

### Suggesting Enhancements

When creating enhancement suggestions, please include:

- **Use a clear and descriptive title**
- **Provide a step-by-step description of the suggested enhancement** in as many details as possible
- **Provide specific examples to demonstrate the steps**
- **Describe the current behavior** and **the expected behavior**
- **Explain why this enhancement would be useful** to most users
- **List some other applications or projects where this enhancement exists**

### Pull Requests

- Fill in the required template
- Follow the C# styleguides
- Include appropriate test cases
- Update documentation as needed
- End all files with a newline

## Development Setup

### Prerequisites
- Visual Studio 2022 or later
- .NET Framework 4.8 SDK
- Git

### Building the Project
1. Clone the repository
   ```bash
   git clone https://github.com/yourusername/SystemRestorePointCreator.git
   cd SystemRestorePointCreator
   ```

2. Open `WindowsFormsApp1.sln` in Visual Studio

3. Build the project
   ```bash
   Ctrl+Shift+B
   ```

4. Run the application
   ```bash
   F5
   ```

### Project Structure
```
SystemRestorePointCreator/
├── Form1.cs                 # Main form logic
├── Form1.Designer.cs        # UI design
├── Program.cs               # Application entry point
├── App.config               # Application configuration
├── WindowsFormsApp1.csproj  # Project file
├── README.md                # Project documentation
├── LICENSE                  # MIT License
├── .gitignore               # Git ignore rules
└── CONTRIBUTING.md          # This file
```

## Coding Guidelines

### C# Style Guide

We follow the [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).

#### Key Points:
- Use 4 spaces for indentation
- Use meaningful variable names
- Prefix member variables with `_`
- Use PascalCase for class names and method names
- Use camelCase for local variables and parameters
- Add XML documentation comments for public methods

### Example:
```csharp
/// <summary>
/// Creates a system restore point with the specified description.
/// </summary>
/// <param name="description">The description for the restore point</param>
/// <returns>The exit code from the PowerShell command</returns>
private int RunElevatedWithExitCode(string fileName, string arguments)
{
    // Implementation
}
```

### Commit Messages

- Use the present tense ("Add feature" not "Added feature")
- Use the imperative mood ("Move cursor to..." not "Moves cursor to...")
- Limit the first line to 72 characters or less
- Reference issues and pull requests liberally after the first line

### Example:
```
Add support for custom descriptions in restore points

- Implement text input field for custom descriptions
- Update registry persistence logic
- Add validation for description length
- Update UI tests

Fixes #123
```

## Testing

Before submitting a pull request, please ensure:

1. **The application builds without errors**
   ```bash
   Ctrl+Shift+B
   ```

2. **No runtime errors occur** when running the application

3. **All features work as expected**:
   - Creating restore points
   - 3-month restriction logic
   - System information display
   - Auto-close functionality
   - Registry persistence

4. **Test edge cases**:
   - Running app within 90 days of last create
   - System Protection disabled
   - Low disk space
   - Admin privilege denial

## Pull Request Process

1. **Before submitting:**
   - Ensure code follows our style guide
   - Test the application thoroughly
   - Update documentation as needed
   - Rebase on latest master branch

2. **Create a descriptive pull request:**
   - Reference any related issues
   - Describe the changes made
   - Include test results
   - Add screenshots if UI changes

3. **Respond to feedback:**
   - Address any review comments
   - Make requested changes
   - Push updates to the PR branch

4. **After merge:**
   - Delete the feature branch
   - Check that the main branch build passes

## Release Process

The maintainers will handle releases following semantic versioning:

- **MAJOR**: Breaking changes
- **MINOR**: New features
- **PATCH**: Bug fixes

Releases are tagged with `v{major}.{minor}.{patch}` and released on GitHub.

## Questions?

Feel free to:
- Open an issue with the "question" label
- Participate in discussions
- Contact the maintainers

## Additional Notes

### Issue and Pull Request Labels

- **bug**: Something isn't working
- **enhancement**: New feature or request
- **documentation**: Improvements or additions to documentation
- **good first issue**: Good for newcomers
- **help wanted**: Extra attention is needed
- **question**: Further information is requested
- **wontfix**: This will not be worked on

## Attribution

This contributing guide is adapted from the [Atom Contributing Guide](https://github.com/atom/atom/blob/master/CONTRIBUTING.md).

---

Thank you for contributing! 🙏
