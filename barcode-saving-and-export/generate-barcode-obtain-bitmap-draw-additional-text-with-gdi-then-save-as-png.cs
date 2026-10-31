// Title: Generate Code128 barcode, add custom text with GDI+, and save as PNG
// Description: This example creates a Code128 barcode, renders it to a bitmap, draws additional text using GDI+, and saves the result as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with Aspose.Drawing for image manipulation. It covers using BarcodeGenerator, configuring barcode parameters, obtaining a Bitmap, drawing graphics, and saving the image. Developers creating custom barcode visuals, labels, or integrating barcode images into UI can use this pattern as a reference.
// Prompt: Generate a barcode, obtain a Bitmap, draw additional text with GDI+, then save as PNG.
// Tags: code128, barcode generation, gdi+, drawing, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, augmenting it with custom text using GDI+, and saving the final image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, draws extra text, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_with_text.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the X-dimension (module width) to 3 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Create a Graphics object from the bitmap to allow drawing additional elements.
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Define font and brush for the custom text.
                    var font = new Font("Arial", 12f);
                    var brush = new SolidBrush(Color.Black);
                    string text = "Sample Text";

                    // Position the text near the bottom-left corner of the image.
                    float x = 5f;
                    float y = bitmap.Height - 20f;

                    // Draw the custom text onto the bitmap.
                    graphics.DrawString(text, font, brush, x, y);
                }

                // Save the modified bitmap to a PNG file.
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fs, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}