// Title: Generate Code128 barcode, add custom text with GDI+, and save as PNG
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode, drawing extra text onto the generated bitmap with GDI+, and persisting the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to combine barcode creation (BarcodeGenerator) with System.Drawing (Aspose.Drawing) graphics operations. Developers often need to augment barcode images with labels, instructions, or branding; this snippet shows the typical workflow of generating a bitmap, using Graphics to draw, and saving the final image.
// Prompt: Generate a barcode, obtain a Bitmap, draw additional text with GDI+, then save as PNG.
// Tags: code128, barcode, generation, gdi+, drawing, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, custom drawing, and PNG output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, adds extra text, and saves the image.
    /// </summary>
    static void Main(string[] args)
    {
        // Determine output file path in temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_with_text.png");
        // Ensure the directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a barcode generator for Code128 with the data "12345678"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Obtain a Graphics object to draw on the bitmap
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Define font and brush for the additional text
                    using (Font font = new Font("Helvetica", 12f))
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        // Position the text near the bottom-left corner
                        float x = 10f;
                        float y = bitmap.Height - 30f;
                        graphics.DrawString("Additional Text", font, brush, x, y);
                    }
                }

                // Save the modified bitmap to a PNG file
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fs, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}