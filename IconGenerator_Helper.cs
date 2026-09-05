// Icon Generator Helper - Run this to create app.ico
// 
// Usage:
// 1. Add this code to Program.cs Main() before Application.Run()
// 2. Or create separate console app
// 3. Generates app.ico in project root

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace WindowsFormsApp1
{
    public class IconGeneratorHelper
    {
        public static void GenerateAndSaveIcon(string outputPath = "app.ico")
        {
            try
            {
                // Create 32x32 bitmap
                using (Bitmap bitmap = new Bitmap(32, 32))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.Clear(Color.White);

                    // Gradient background (Blue theme - Windows colors)
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        new Point(0, 0),
                        new Point(32, 32),
                        Color.FromArgb(0, 150, 215),    // Light blue
                        Color.FromArgb(0, 100, 180)))   // Dark blue
                    {
                        g.FillRectangle(brush, 0, 0, 32, 32);
                    }

                    // Draw folder shape (represents backup/restore)
                    using (Pen penWhite = new Pen(Color.White, 2.5f))
                    {
                        // Folder body (main rectangle)
                        g.DrawRectangle(penWhite, 5, 11, 22, 15);

                        // Folder tab (top-left small rectangle)
                        g.DrawLine(penWhite, 5, 11, 13, 3);
                        g.DrawLine(penWhite, 13, 3, 13, 11);
                    }

                    // Draw restore arrow inside folder
                    using (SolidBrush brushWhite = new SolidBrush(Color.White))
                    using (Pen penArrow = new Pen(Color.White, 2))
                    {
                        // Down arrow (restore/recovery symbol)
                        int centerX = 16;
                        int centerY = 17;

                        // Arrow point (top)
                        g.FillPolygon(brushWhite, new Point[]
                        {
                            new Point(centerX, centerY - 2),      // Top point
                            new Point(centerX - 2, centerY + 2),  // Bottom left
                            new Point(centerX + 2, centerY + 2)   // Bottom right
                        });

                        // Arrow shaft
                        g.DrawLine(penArrow, centerX, centerY + 2, centerX, centerY + 5);
                    }

                    // Optional: Add border
                    using (Pen penBorder = new Pen(Color.FromArgb(0, 80, 150), 0.5f))
                    {
                        g.DrawRectangle(penBorder, 0, 0, 31, 31);
                    }

                    // Save as icon - Find project root (2 levels up from bin\Debug)
                    string currentDir = Directory.GetCurrentDirectory();
                    string projectRoot = Path.GetDirectoryName(Path.GetDirectoryName(currentDir));
                    string fullPath = Path.Combine(projectRoot, outputPath);
                    
                    using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                    {
                        using (Icon icon = Icon.FromHandle(bitmap.GetHicon()))
                        {
                            icon.Save(fs);
                        }
                    }

                    Console.WriteLine($"✓ Icon generated: {fullPath}");
                    Console.WriteLine($"✓ File size: {new FileInfo(fullPath).Length} bytes");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error generating icon: {ex.Message}");
            }
        }
    }
}

// ==========================================
// USAGE IN Program.cs
// ==========================================
/*
[STAThread]
static void Main()
{
    // Generate icon on first run (OPTIONAL - Comment out after first generation)
    // IconGeneratorHelper.GenerateAndSaveIcon("app.ico");

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new Form1());
}
*/

// ==========================================
// HOW TO SET ICON IN VISUAL STUDIO
// ==========================================
/*
1. After running the generator, app.ico will be created
2. In Visual Studio:
   - Right-click Project → Properties
   - Go to "Application" tab
   - Click "Icon and manifest" dropdown
   - Browse to app.ico file
   - Click "Open"
   - Save project

3. The icon will now appear:
   - On application title bar
   - In taskbar
   - In file explorer
   - When published

4. For Release build:
   - Build → Configuration Manager
   - Select "Release"
   - Build Project
   - Check bin/Release/WindowsFormsApp1.exe has icon
*/
