// Title: Batch generation of 100 Code128 barcodes with varying XDimension (mm) saved as TIFF
// Description: Demonstrates how to create a batch of 100 Code128 barcodes, each with a unique XDimension measured in millimeters, and save them as TIFF images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include bulk barcode creation for inventory, labeling, or testing where precise module size control is required. Developers often need to adjust XDimension to meet printing specifications and generate images in various formats.
// Prompt: Implement batch processing to generate 100 barcodes with varying Millimeter XDimension values, storing each as TIFF.
// Tags: code128, batch processing, tiff, barcodelibrary, generator, parameters, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes with varying XDimension values and saves them as TIFF files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output directory, generates 100 barcodes with incremental XDimension, and writes them to TIFF files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory for the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Loop to generate 100 barcodes, each with a distinct XDimension value
        for (int i = 0; i < 100; i++)
        {
            // Build a unique code text for each barcode (e.g., CODE001, CODE002, ...)
            string codeText = $"CODE{i + 1:D3}";

            // Calculate XDimension in millimeters (starting at 0.5 mm, increasing by 0.05 mm per iteration)
            float xDimMm = 0.5f + i * 0.05f;

            // Initialize the barcode generator for Code128 symbology with the current code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the calculated XDimension (module width) in millimeters
                generator.Parameters.Barcode.XDimension.Millimeters = xDimMm;

                // Construct the full file path for the TIFF image (e.g., Barcode_001.tiff)
                string filePath = Path.Combine(outputDir, $"Barcode_{i + 1:D3}.tiff");

                // Save the generated barcode as a TIFF image to the specified path
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Generated 100 barcodes in: {outputDir}");
    }
}