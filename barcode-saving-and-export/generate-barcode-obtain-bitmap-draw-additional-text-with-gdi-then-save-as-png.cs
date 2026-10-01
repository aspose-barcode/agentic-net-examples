// Title: Generate Code128 barcode, add custom text with GDI+, and save as PNG
// Description: This example creates a Code128 barcode, renders it to an Aspose.Drawing bitmap, draws additional text using GDI+, and saves the result as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with Aspose.Drawing graphics operations. The example uses BarcodeGenerator (EncodeTypes) to produce a bitmap, then employs GDI+ (Graphics, Font, Brush) to overlay custom text. Typical scenarios include adding labels, annotations, or branding to barcode images before exporting them in common formats such as PNG.
// Prompt: Generate a barcode, obtain a Bitmap, draw additional text with GDI+, then save as PNG.
// Tags: code128, barcode generation, png, aspose.barcode, aspose.drawing, gdi+

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode, draw extra text with GDI+, and save the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, adds custom text, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_with_text.png");

        // Initialize a barcode generator for Code128 with the sample code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: adjust the module (X) size to make the barcode larger or smaller.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Create a Graphics object from the bitmap to enable drawing operations.
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    // Set up the font and brush for the additional text.
                    using (var font = new Font("Arial", 12f, FontStyle.Regular))
                    using (var brush = new SolidBrush(Color.Black))
                    {
                        // Position the text near the bottom of the image.
                        float textX = 10f;
                        float textY = bitmap.Height - 30f;

                        // Draw the custom string onto the bitmap.
                        graphics.DrawString("Sample Text", font, brush, textX, textY);
                    }
                }

                // Save the final bitmap as a PNG file using a file stream.
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}