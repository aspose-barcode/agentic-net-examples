// Title: Generate Code128 barcode, add custom text with GDI+, save as PNG
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode, converting it to a Bitmap, drawing extra text with GDI+, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and Aspose.Drawing classes to produce a barcode image, modify it with GDI+ graphics, and export it. Developers often need to embed additional information or branding onto barcode images, and this pattern illustrates the typical workflow for such customizations.
// Prompt: Generate a barcode, obtain a Bitmap, draw additional text with GDI+, then save as PNG.
// Tags: code128, barcode generation, png, aspose.barcodes, aspose.drawing, gdi+, bitmap, text overlay

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode, adds custom text using GDI+, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final image.
        string outputPath = "barcode_with_text.png";

        // Initialize a barcode generator for Code128 with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the module (X) dimension to control barcode size.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the barcode image from the memory stream as a Bitmap.
                using (var barcodeImage = (Bitmap)Image.FromStream(ms))
                {
                    // Create a Graphics object to draw on the bitmap.
                    using (var graphics = Graphics.FromImage(barcodeImage))
                    {
                        string extraText = "Sample Text";

                        // Define the font and brush for the overlay text.
                        using (var font = new Font("Arial", 12f))
                        using (var brush = new SolidBrush(Color.Black))
                        {
                            // Calculate position near the bottom‑right corner.
                            var position = new PointF(barcodeImage.Width - 100f, barcodeImage.Height - 20f);
                            graphics.DrawString(extraText, font, brush, position);
                        }
                    }

                    // Save the modified bitmap (barcode + text) as a PNG file.
                    barcodeImage.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}