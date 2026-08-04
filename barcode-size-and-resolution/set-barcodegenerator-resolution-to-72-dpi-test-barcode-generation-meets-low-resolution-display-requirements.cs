// Title: Generate low‑resolution barcode image (72 dpi) using Aspose.BarCode
// Description: Demonstrates setting the BarcodeGenerator resolution to 72 dpi and saving the barcode as a PNG file, useful for low‑resolution display scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image resolution with the BarcodeGenerator class. Developers often need to adjust DPI for screen or printer constraints, and this snippet shows typical usage of EncodeTypes, generator.Parameters, and saving the output. Ideal for quick reference in search results.
// Prompt: Set BarcodeGenerator resolution to 72 dpi, test barcode generation meets low‑resolution display requirements.
// Tags: barcode, code128, resolution, 72dpi, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode at 72 dpi resolution and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, sets low‑resolution DPI, saves the image, and reports success.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "barcode_72dpi.png";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the generator to use a low‑resolution of 72 dpi (suitable for low‑res displays)
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode image to the specified path (default format is PNG)
            generator.Save(outputPath);
        }

        // Check whether the barcode image file was successfully created
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"Barcode generated successfully at {outputPath} with 72 dpi resolution.");
        }
        else
        {
            Console.WriteLine("Failed to generate the barcode image.");
        }
    }
}