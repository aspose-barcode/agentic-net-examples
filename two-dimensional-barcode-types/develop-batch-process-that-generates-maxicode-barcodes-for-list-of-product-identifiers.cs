// Title: Batch Generation of MaxiCode Barcodes for Product IDs
// Description: Demonstrates how to generate MaxiCode barcodes in a batch for a collection of product identifiers and save them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.MaxiCode. It shows typical batch processing scenarios where developers need to create multiple barcodes, configure visual properties, and store images to disk. Useful for inventory, shipping, and logistics applications that require MaxiCode symbology.
// Prompt: Develop a batch process that generates MaxiCode barcodes for a list of product identifiers.
// Tags: maxicode, barcode generation, batch processing, png, aspose.barcode, encode types, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of MaxiCode barcodes for a list of product identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates and saves MaxiCode barcode images for each product ID.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product identifiers to be encoded.
        List<string> productIds = new List<string>
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Create a unique temporary folder to store the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Generating MaxiCode barcodes in: {outputFolder}");

        // Iterate over each product ID and generate a corresponding MaxiCode barcode.
        for (int i = 0; i < productIds.Count; i++)
        {
            string id = productIds[i];
            string filePath = Path.Combine(outputFolder, $"{id}.png");

            try
            {
                // Initialize the barcode generator with MaxiCode symbology and the current ID.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, id))
                {
                    // Optional visual settings: set module size and colors.
                    generator.Parameters.Barcode.XDimension.Pixels = 10f;
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;

                    // Save the generated barcode as a PNG image to the designated file path.
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

        Console.WriteLine("Batch processing completed.");
    }
}