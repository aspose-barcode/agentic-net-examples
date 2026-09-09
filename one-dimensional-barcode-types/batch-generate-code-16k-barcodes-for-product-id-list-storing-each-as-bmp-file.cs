// Title: Batch generation of Code 16K barcodes to BMP files
// Description: Demonstrates how to generate Code 16K barcodes for a list of product IDs and save each barcode as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.Code16K. It illustrates typical batch‑creation scenarios such as inventory labeling, where developers need to produce multiple barcodes quickly, configure barcode parameters (module size, aspect ratio), and export them to common image formats. The key API classes include BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Batch generate Code 16K barcodes for product ID list, storing each as BMP file.
// Tags: code16k, barcode, batch, bmp, generation, aspose.barcode, encode, imageformat

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that creates Code 16K barcodes for a collection of product identifiers
/// and stores each barcode as a BMP image in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for predefined product IDs and writes them to BMP files.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product identifiers.
        List<string> productIds = new List<string>
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Create a unique temporary output folder for the generated images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code16KBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Iterate over each product ID and generate a corresponding barcode.
        foreach (string id in productIds)
        {
            string filePath = Path.Combine(outputFolder, $"{id}.bmp");
            try
            {
                // Initialize the barcode generator with Code 16K symbology and the current product ID.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, id))
                {
                    // Configure barcode appearance: set module (X) dimension and aspect ratio.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;          // Module size in pixels.
                    generator.Parameters.Barcode.Code16K.AspectRatio = 10;      // Recommended aspect ratio (>8).

                    // Save the generated barcode as a BMP image.
                    generator.Save(filePath, BarCodeImageFormat.Bmp);
                }
                Console.WriteLine($"Generated barcode for '{id}' -> {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for the current ID.
                Console.WriteLine($"Failed to generate barcode for '{id}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch generation completed.");
    }
}