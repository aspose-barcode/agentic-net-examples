// Title: Generate Code128 barcode, add custom text with GDI+, and save as PNG
// Description: This example creates a Code128 barcode, renders it to a bitmap, draws additional text using GDI+, and saves the result as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with Aspose.Drawing graphics manipulation. Shows how to use BarcodeGenerator, Bitmap, Graphics, Font, and SolidBrush to produce a customized barcode image. Useful for developers needing to overlay text or graphics on generated barcodes before saving in common image formats.
// Prompt: Generate a barcode, obtain a Bitmap, draw additional text with GDI+, then save as PNG.
// Tags: code128, barcode generation, gdi+, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, adds overlay text using GDI+, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_with_text.png");

        // Initialize the barcode generator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Create a Graphics object to draw on the bitmap.
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Set up the font and brush for the overlay text.
                    using (Font font = new Font("Helvetica", 12f))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        // Calculate the position for the text near the bottom of the image.
                        float textX = 10f;
                        float textY = bitmap.Height - 30f;

                        // Draw the custom text onto the bitmap.
                        graphics.DrawString("Sample Text", font, brush, textX, textY);
                    }
                }

                // Save the modified bitmap as a PNG file.
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Saved barcode image to {outputPath}");
    }
}