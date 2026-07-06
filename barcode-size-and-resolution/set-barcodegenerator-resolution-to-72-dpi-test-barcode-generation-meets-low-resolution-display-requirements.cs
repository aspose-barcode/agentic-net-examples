// Title: Generate low‑resolution Code128 barcode image
// Description: Demonstrates setting the BarcodeGenerator resolution to 72 dpi and verifies the output image meets low‑resolution display requirements.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode rendering parameters such as resolution. It uses BarcodeGenerator and its Parameters property to produce PNG images, a common task for developers needing barcodes for low‑resolution screens or printers. Typical use cases include embedding barcodes in web pages, mobile apps, or low‑dpi print media.
// Prompt: Set BarcodeGenerator resolution to 72 dpi, test barcode generation meets low‑resolution display requirements.
// Tags: code128, barcode generation, low resolution, png, aspose.barcode, resolution

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode at 72 dpi and verifying the image resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode image with low resolution, saves it, and prints the actual DPI.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the resolution to 72 dpi to meet low‑resolution display requirements
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath);
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image to read its actual DPI values
        using (var image = Image.FromFile(outputPath))
        {
            float horizDpi = image.HorizontalResolution;
            float vertDpi = image.VerticalResolution;

            // Output the horizontal and vertical DPI of the generated image
            Console.WriteLine($"Barcode image resolution: {horizDpi} dpi (horizontal), {vertDpi} dpi (vertical)");
        }
    }
}