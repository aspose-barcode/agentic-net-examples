// Title: Generate high‑resolution Code128 barcode and save as JPEG
// Description: This example creates a Code128 barcode, sets the image resolution to 300 DPI, and saves it as a high‑resolution JPEG file.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat options. Typical use cases include creating printable barcodes for labels, invoices, or product packaging where image quality and resolution are critical. Developers often need to control DPI settings and output formats to meet printing standards.
// Prompt: Configure barcode resolution to 300 DPI and save the generated image as a high‑resolution JPEG.
// Tags: code128, barcode, resolution, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a high‑resolution barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates a Code128 barcode at 300 DPI,
    /// saves it as a JPEG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting JPEG image
        string outputPath = Path.Combine(outputDir, "barcode_highres.jpg");

        // Initialize the barcode generator with Code128 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a JPEG image at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}