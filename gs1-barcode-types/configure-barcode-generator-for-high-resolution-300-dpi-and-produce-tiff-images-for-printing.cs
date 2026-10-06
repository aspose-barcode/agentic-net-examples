// Title: Generate High‑Resolution Code128 Barcode and Save as TIFF
// Description: This example creates a Code128 barcode, sets the resolution to 300 DPI, and saves it as a TIFF image suitable for printing.
// Category-Description: Demonstrates Aspose.BarCode generation with high‑resolution settings, covering the BarcodeGenerator class, resolution configuration, and image format selection. Commonly used for creating printable barcodes in packaging, labels, and documents where crisp, high‑DPI output is required. Ideal for developers needing to produce TIFF files for high‑quality print workflows.
// Prompt: Configure the barcode generator for high resolution (300 DPI) and produce TIFF images for printing.
// Tags: code128, barcode, generation, high-resolution, tiff, resolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode at 300 DPI and saves it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting TIFF image.
        string outputPath = Path.Combine(outputDir, "barcode_300dpi.tiff");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Set the image resolution to 300 DPI for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF file.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}