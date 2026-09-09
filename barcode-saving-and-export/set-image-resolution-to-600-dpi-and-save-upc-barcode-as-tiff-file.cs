// Title: Generate a UPC-A barcode and save as TIFF with 600 DPI resolution
// Description: This example creates a UPC‑A barcode, sets the image resolution to 600 DPI, and saves it as a TIFF file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with high‑resolution image output. It uses the BarcodeGenerator class and its Parameters property to configure resolution, then saves the result using BarCodeImageFormat. Developers working with barcode imaging often need to control DPI for printing or scanning quality, making this pattern common in barcode generation workflows.
// Prompt: Set image resolution to 600 DPI and save a UPC‑A barcode as a TIFF file.
// Tags: upc-a,barcode,generation,resolution,tiff,aspobarcodes,aspobarcodes-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode, sets a high image resolution,
/// and saves the result as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Build the full path for the TIFF file.
        string outputPath = Path.Combine(outputDir, "upc_a.tiff");

        // Create a barcode generator for UPC‑A with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
        {
            // Set the image resolution to 600 DPI.
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode as a TIFF image.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}