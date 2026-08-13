// Title: Batch Generation of MaxiCode Barcodes for Product IDs
// Description: Demonstrates how to generate MaxiCode (Mode 4) barcodes in a batch, saving each as a PNG file to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as MaxiCode. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeStandardCodetext to encode data, and illustrates typical tasks like iterating over a collection of identifiers, handling errors, and saving images in PNG format. Developers working with shipping, logistics, or inventory systems often need to produce MaxiCode symbols programmatically.
// Prompt: Develop a batch process that generates MaxiCode barcodes for a list of product identifiers.
// Tags: maxicode, batch, barcode generation, png, aspose.barcode, complexbarcode, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

/// <summary>
/// Provides a console application that creates MaxiCode barcodes for a predefined list of product identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode for each product ID and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of product identifiers to be encoded.
        var productIds = new[]
        {
            "PROD-001",
            "PROD-002",
            "PROD-003",
            "PROD-004",
            "PROD-005"
        };

        // Create a unique temporary directory where all generated barcode images will be stored.
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        int index = 1; // Counter used to generate sequential file names.

        // Iterate over each product identifier and generate a corresponding MaxiCode barcode.
        foreach (var id in productIds)
        {
            try
            {
                // Configure MaxiCode data: use Mode4 (standard) and set the message to the product ID.
                var maxiCodeData = new MaxiCodeStandardCodetext
                {
                    Mode = MaxiCodeMode.Mode4,
                    Message = id
                };

                // Initialise the complex barcode generator with the configured MaxiCode data.
                using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
                {
                    // Generate the barcode image in memory (optional, but ensures the image is ready before saving).
                    generator.GenerateBarCodeImage();

                    // Build the full file path for the PNG output, using a zero‑padded index.
                    string filePath = Path.Combine(outputFolder, $"MaxiCode_{index:D3}.png");

                    // Save the generated barcode image to disk in PNG format.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for '{id}' -> {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation for the current product ID.
                Console.WriteLine($"Failed to generate barcode for '{id}': {ex.Message}");
            }

            index++; // Increment the file name counter.
        }

        Console.WriteLine("Batch processing completed.");
    }
}