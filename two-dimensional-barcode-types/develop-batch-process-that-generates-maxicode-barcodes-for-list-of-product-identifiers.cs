// Title: Batch Generation of MaxiCode Barcodes for Product IDs
// Description: Demonstrates how to generate MaxiCode barcodes in a batch for a collection of product identifiers, saving each barcode as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.MaxiCode. It shows typical batch processing scenarios where developers need to create multiple MaxiCode symbols for inventory or shipping labels, covering key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Useful for automating barcode creation in .NET applications.
// Prompt: Develop a batch process that generates MaxiCode barcodes for a list of product identifiers.
// Tags: maxicode, batch, barcode generation, png, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of MaxiCode barcodes for a list of product identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates MaxiCode PNG files for each product ID and stores them in a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product identifiers to encode.
        List<string> productIds = new List<string>
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Create a unique temporary folder to hold the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Iterate over each product ID and generate a corresponding MaxiCode barcode.
        for (int i = 0; i < productIds.Count; i++)
        {
            string id = productIds[i];
            string filePath = Path.Combine(outputFolder, $"MaxiCode_{id}.png");

            try
            {
                // Initialize the barcode generator with MaxiCode symbology and the current product ID.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, id))
                {
                    // Configure barcode appearance: set module size, mode, and aspect ratio.
                    generator.Parameters.Barcode.XDimension.Pixels = 15f;               // Module size in pixels
                    generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;   // Mode 4 supports arbitrary text
                    generator.Parameters.Barcode.MaxiCode.AspectRatio = 1f;           // Default aspect ratio

                    // Save the generated barcode as a PNG file.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for '{id}' at '{filePath}'.");
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