// Title: Batch generation of 100 Code128 barcodes with varying XDimension saved as TIFF
// Description: Generates 100 Code128 barcodes, each with a unique XDimension value in millimeters, and saves them as TIFF images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure barcode dimensions (XDimension) and perform batch processing. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create high‑resolution TIFF outputs—common tasks for developers needing bulk barcode creation for labeling, inventory, or printing workflows.
// Prompt: Implement batch processing to generate 100 barcodes with varying Millimeter XDimension values, storing each as TIFF.
// Tags: code128, generation, tiff, xdimension, aspose.barcode, aspose.drawing, batch-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of 100 Code128 barcodes with incremental XDimension values,
/// saving each barcode as a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode images and writes the output folder path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images.
        string outputDir = "Barcodes";

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Loop to generate 100 barcodes with incremental XDimension (0.1 mm steps).
        for (int i = 1; i <= 100; i++)
        {
            // Create a unique code text for each barcode (e.g., CODE001, CODE002, ...).
            string codeText = $"CODE{i:D3}";

            // Initialize the barcode generator for Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the XDimension in millimeters (0.1 mm, 0.2 mm, ..., 10.0 mm).
                float xDimensionMm = i * 0.1f;
                generator.Parameters.Barcode.XDimension.Millimeters = xDimensionMm;

                // Optional: increase resolution for higher quality output (300 DPI).
                generator.Parameters.Resolution = 300;

                // Build the full file path for the TIFF image.
                string filePath = Path.Combine(outputDir, $"barcode_{i:D3}.tiff");

                // Save the generated barcode as a TIFF file.
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }

        // Inform the user where the barcode images have been saved.
        Console.WriteLine($"Generated 100 barcode images in: {Path.GetFullPath(outputDir)}");
    }
}