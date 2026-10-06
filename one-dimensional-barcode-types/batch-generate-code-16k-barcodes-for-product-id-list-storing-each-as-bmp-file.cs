// Title: Batch generation of Code 16K barcodes to BMP files
// Description: Demonstrates how to create Code 16K barcodes for a list of product IDs and save each barcode as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.Code16K. It illustrates typical tasks such as configuring barcode dimensions, aspect ratio, and quiet zones, then saving the output in a raster image format. Developers working on inventory, labeling, or batch barcode creation can reference this pattern for automating bulk barcode production.
// Prompt: Batch generate Code 16K barcodes for product ID list, storing each as BMP file.
// Tags: code16k, barcode generation, batch processing, bmp output, aspose.barcode, encode types

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code 16K barcodes for a predefined list of product IDs and saves each as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output directory, iterates over product IDs, configures barcode parameters, and saves each barcode image.
    /// </summary>
    static void Main()
    {
        // Define the list of product IDs to encode.
        List<string> productIds = new List<string>
        {
            "123456789012",
            "987654321098",
            "A1B2C3D4E5",
            "XYZ12345",
            "000111222333"
        };

        // Create a temporary output directory with a unique name.
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine("Generating Code 16K barcodes in: " + outputDir);

        // Iterate over each product ID and generate a barcode.
        foreach (string id in productIds)
        {
            // Build the full file path for the BMP image.
            string filePath = Path.Combine(outputDir, id + ".bmp");

            // Initialize the barcode generator with Code 16K symbology and the current ID.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, id))
            {
                // Configure visual parameters: X-dimension, aspect ratio, and quiet zones.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.Code16K.AspectRatio = 10;
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;

                // Save the generated barcode as a BMP file.
                generator.Save(filePath, BarCodeImageFormat.Bmp);
            }
        }

        // Indicate that the batch generation process has completed.
        Console.WriteLine("Barcode generation completed.");
    }
}