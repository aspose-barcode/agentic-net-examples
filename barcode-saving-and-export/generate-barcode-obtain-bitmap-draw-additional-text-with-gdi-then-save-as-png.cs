// Title: Generate Code128 barcode, add custom text, and save as PNG
// Description: Creates a Code128 barcode, draws extra text using GDI+, and writes the result to a PNG file.
// Prompt: Generate a barcode, obtain a Bitmap, draw additional text with GDI+, then save as PNG.
// Tags: code128, barcode, gdi+, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode, overlay custom text using GDI+, and save the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, adds text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final PNG image
        string outputPath = "barcode_with_text.png";

        // Initialize a barcode generator for Code128 with the sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure auto‑size mode and set explicit image dimensions (points)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image as a bitmap
            using (var barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Create a Graphics object to draw additional text onto the bitmap
                using (var graphics = Graphics.FromImage(barcodeBitmap))
                {
                    // Define the font and brush used for the overlay text
                    using (var font = new Font("Arial", 12f))
                    using (var brush = new SolidBrush(Color.Black))
                    {
                        // Calculate the position near the bottom of the image
                        float x = 10f;
                        float y = barcodeBitmap.Height - 30f;

                        // Render the custom text onto the bitmap
                        graphics.DrawString("Sample Text", font, brush, new PointF(x, y));
                    }
                }

                // Save the modified bitmap to a PNG file using a file stream
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeBitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }

        // Output the full path of the saved image for verification
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
    }
}