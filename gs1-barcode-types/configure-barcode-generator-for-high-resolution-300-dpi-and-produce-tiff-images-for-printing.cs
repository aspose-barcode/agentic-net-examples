// Title: Generate High‑Resolution Code128 Barcode and Save as TIFF
// Description: This example configures Aspose.BarCode to generate a Code128 barcode at 300 DPI and saves it as a TIFF image suitable for printing.
// Category-Description: Demonstrates barcode generation with Aspose.BarCode, focusing on setting image resolution and exporting to TIFF for high‑quality print output. It showcases the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—common tools for developers who need to create printable barcodes in various symbologies and formats.
// Prompt: Configure the barcode generator for high resolution (300 DPI) and produce TIFF images for printing.
// Tags: code128, barcode-generation, high-resolution, tiff, aspose-barcode, barcodegenerator, encode-types, barcodelimageformat

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a high‑resolution Code128 barcode and saves it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, sets resolution, and writes the image to disk.
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

            // Save the generated barcode as a TIFF image.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}