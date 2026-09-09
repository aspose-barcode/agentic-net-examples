// Title: Batch Generation of MaxiCode Barcodes for Product IDs
// Description: Demonstrates how to generate MaxiCode barcodes for a collection of product identifiers and save each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating batch processing of barcodes using the BarcodeGenerator class. It covers creating a temporary output directory, iterating over a list of data, configuring barcode parameters (such as XDimension), and saving images in PNG format. Developers working with bulk barcode creation, inventory labeling, or shipping applications can use this pattern as a starting point.
// Prompt: Develop a batch process that generates MaxiCode barcodes for a list of product identifiers.
// Tags: maxicode, barcode generation, batch processing, png, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that creates MaxiCode barcodes for a predefined list of product IDs
/// and stores each barcode as a PNG file in a temporary batch folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves MaxiCode barcodes for each product identifier.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of product identifiers to be encoded as MaxiCode barcodes.
        List<string> productIds = new List<string>
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Create a unique temporary directory to hold the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine($"Generating MaxiCode barcodes in: {outputDir}");

        // Iterate over each product ID, generate a barcode, and save it as a PNG file.
        for (int i = 0; i < productIds.Count; i++)
        {
            string id = productIds[i];
            string filePath = Path.Combine(outputDir, $"{id}.png");

            try
            {
                // Initialize the barcode generator with MaxiCode symbology and the current product ID.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, id))
                {
                    // Set the module (pixel) size for the barcode; adjust as needed for readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 10f;

                    // Optional: specify a particular MaxiCode encoding mode (e.g., Mode4 for arbitrary text).
                    // generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Mode4;

                    // Save the generated barcode image to the designated file path in PNG format.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Saved barcode for '{id}' to '{filePath}'.");
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