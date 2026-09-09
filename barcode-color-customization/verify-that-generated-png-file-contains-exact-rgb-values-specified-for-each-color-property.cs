// Title: Generate Barcode PNG with Custom Colors and Verify RGB Values
// Description: This example creates a Code128 barcode PNG image using specific ARGB colors for background, bars, border, text, and captions, then checks that the generated image contains those exact RGB values.
// Category-Description: Demonstrates Aspose.BarCode image generation with color customization. It uses BarcodeGenerator, its Parameters property, and Aspose.Drawing Bitmap to inspect pixel colors. Typical use cases include branding, UI integration, and automated visual verification of barcode appearance. Developers often need to set colors, borders, and captions and confirm the output matches design specifications.
// Prompt: Verify that the generated PNG file contains the exact RGB values specified for each color property.
// Tags: barcode, code128, color, png, aspose.barcode, image verification, bitmap, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode image with custom colors,
/// save it as PNG, and verify that the image contains the expected RGB values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and validates color usage.
    /// </summary>
    static void Main()
    {
        // Define custom ARGB colors for various barcode elements
        Color backgroundColor = Color.FromArgb(255, 0, 255, 0); // Green background
        Color barColor = Color.FromArgb(255, 255, 0, 0);       // Red bars
        Color borderColor = Color.FromArgb(255, 0, 0, 255);    // Blue border
        Color textColor = Color.FromArgb(255, 255, 255, 0);   // Yellow text
        Color captionColor = Color.FromArgb(255, 255, 0, 255); // Magenta caption

        // Create a unique temporary folder to store the generated PNG
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string pngPath = Path.Combine(tempFolder, "barcode.png");

        try
        {
            // Generate the barcode with the specified colors and captions
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                generator.Parameters.BackColor = backgroundColor;
                generator.Parameters.Barcode.BarColor = barColor;
                generator.Parameters.Border.Visible = true;
                generator.Parameters.Border.Color = borderColor;
                generator.Parameters.Border.Width.Pixels = 5;
                generator.Parameters.Barcode.CodeTextParameters.Color = textColor;
                generator.Parameters.CaptionAbove.Text = "Top Caption";
                generator.Parameters.CaptionAbove.TextColor = captionColor;
                generator.Parameters.CaptionBelow.Text = "Bottom Caption";
                generator.Parameters.CaptionBelow.TextColor = captionColor;

                // Save the barcode as a PNG file
                generator.Save(pngPath, BarCodeImageFormat.Png);
            }

            // Load the generated image for pixel-level verification
            using (Bitmap bitmap = new Bitmap(pngPath))
            {
                // Verify background color at the top-left pixel
                bool backgroundOk = bitmap.GetPixel(0, 0).ToArgb() == backgroundColor.ToArgb();

                // Verify that the bar color appears somewhere in the image
                bool barOk = ContainsColor(bitmap, barColor);

                // Verify that the border color appears on the image edges
                bool borderOk = EdgeContainsColor(bitmap, borderColor);

                // Verify that the text color appears somewhere in the image
                bool textOk = ContainsColor(bitmap, textColor);

                // Verify that the caption color appears somewhere in the image
                bool captionOk = ContainsColor(bitmap, captionColor);

                // Output verification results
                Console.WriteLine($"Background color match: {backgroundOk}");
                Console.WriteLine($"Bar color present: {barOk}");
                Console.WriteLine($"Border color present on edges: {borderOk}");
                Console.WriteLine($"Text color present: {textOk}");
                Console.WriteLine($"Caption color present: {captionOk}");
            }
        }
        finally
        {
            // Clean up temporary files and folder
            if (File.Exists(pngPath))
            {
                try { File.Delete(pngPath); } catch { }
            }
            try { Directory.Delete(tempFolder, true); } catch { }
        }
    }

    /// <summary>
    /// Scans the entire bitmap to determine if the specified color is present.
    /// </summary>
    /// <param name="bitmap">The bitmap to search.</param>
    /// <param name="target">The ARGB color to find.</param>
    /// <returns>True if the color is found; otherwise, false.</returns>
    static bool ContainsColor(Bitmap bitmap, Color target)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (bitmap.GetPixel(x, y).ToArgb() == target.ToArgb())
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks the outer edges of the bitmap for the presence of a specific color.
    /// </summary>
    /// <param name="bitmap">The bitmap to examine.</param>
    /// <param name="target">The ARGB color to detect on the edges.</param>
    /// <returns>True if the color is found on any edge; otherwise, false.</returns>
    static bool EdgeContainsColor(Bitmap bitmap, Color target)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;

        // Check top and bottom edges
        for (int x = 0; x < width; x++)
        {
            if (bitmap.GetPixel(x, 0).ToArgb() == target.ToArgb()) return true;
            if (bitmap.GetPixel(x, height - 1).ToArgb() == target.ToArgb()) return true;
        }

        // Check left and right edges
        for (int y = 0; y < height; y++)
        {
            if (bitmap.GetPixel(0, y).ToArgb() == target.ToArgb()) return true;
            if (bitmap.GetPixel(width - 1, y).ToArgb() == target.ToArgb()) return true;
        }

        return false;
    }
}