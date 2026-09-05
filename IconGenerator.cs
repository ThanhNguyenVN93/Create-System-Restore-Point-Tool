using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Icon Generator - Creates professional application icons
    /// </summary>
    public static class IconGenerator
    {
        /// <summary>
        /// Generate a professional System Restore Point icon (32x32 with multiple resolutions)
        /// </summary>
        public static Icon GenerateAppIcon()
        {
            // Create 32x32 icon
            using (Bitmap bitmap = new Bitmap(32, 32))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Draw background with gradient
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(32, 32),
                    Color.FromArgb(0, 150, 215),      // Light blue
                    Color.FromArgb(0, 100, 180)))     // Dark blue
                {
                    g.FillRectangle(brush, 0, 0, 32, 32);
                }

                // Draw folder shape (backup/restore concept)
                using (Pen pen = new Pen(Color.White, 2))
                {
                    // Main folder body
                    g.DrawRectangle(pen, 5, 10, 22, 16);
                    
                    // Folder tab
                    g.DrawLine(pen, 5, 10, 13, 2);
                    g.DrawLine(pen, 13, 2, 13, 10);
                }

                // Draw disk/restore symbol inside folder (downward arrow)
                using (Pen arrowPen = new Pen(Color.White, 2))
                using (SolidBrush arrowBrush = new SolidBrush(Color.White))
                {
                    // Arrow pointing down (restore concept)
                    Point[] arrowPoints = new Point[]
                    {
                        new Point(16, 12),   // Top
                        new Point(13, 16),   // Bottom left
                        new Point(15, 16),   // Shaft left
                        new Point(15, 20),   // Shaft bottom
                        new Point(17, 20),   // Shaft right
                        new Point(17, 16),   // Shaft right up
                        new Point(19, 16)    // Bottom right
                    };
                    
                    g.FillPolygon(arrowBrush, arrowPoints);
                }

                // Draw border
                using (Pen borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, 31, 31);
                }

                // Convert to icon
                return Icon.FromHandle(bitmap.GetHicon());
            }
        }

        /// <summary>
        /// Generate a simple solid color icon with folder
        /// </summary>
        public static Icon GenerateSimpleIcon()
        {
            using (Bitmap bitmap = new Bitmap(32, 32))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Blue background
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 120, 215)))
                {
                    g.FillRectangle(brush, 2, 2, 28, 28);
                }

                // Folder outline
                using (Pen pen = new Pen(Color.White, 2))
                {
                    g.DrawRectangle(pen, 6, 10, 20, 15);
                    g.DrawLine(pen, 6, 10, 14, 2);
                    g.DrawLine(pen, 14, 2, 14, 10);
                }

                return Icon.FromHandle(bitmap.GetHicon());
            }
        }

        /// <summary>
        /// Generate icon and save as .ico file
        /// </summary>
        public static bool SaveIconToFile(string filePath)
        {
            try
            {
                using (Icon icon = GenerateAppIcon())
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(
                        filePath,
                        System.IO.FileMode.Create))
                    {
                        icon.Save(fs);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving icon: {ex.Message}");
                return false;
            }
        }
    }
}
