// Title: Batch generation of Code 16K barcodes to BMP files
// Description: Demonstrates how to generate Code 16K barcodes for a list of product identifiers and save each barcode as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.Code16K. It shows typical tasks such as setting barcode parameters (module size, aspect ratio), iterating over a collection of data, and exporting images in BMP format—common operations for developers creating product labeling or inventory systems.
// Prompt: Batch generate Code 16K barcodes for product ID list, storing each as BMP file.
// Tags: code16k, barcode, generation, bmp, aspose.barcode, encode, batch, product-id

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Code 16K barcodes for a set of product IDs
/// and saves each barcode as a BMP image in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a predefined list of product IDs,
    /// creates a Code 16K barcode for each, and writes the result to a BMP file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a sample list of product identifiers to encode.
        List<string> productIds = new List<string>
        {
            "12345678901231",
            "98765432109876",
            "ABC123DEF456",
            "00112233445566",
            "XYZ7890"
        };

        // Create a unique temporary output folder for the generated images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Code16KBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        int count = 0; // Counter for naming output files sequentially.

        // Process each product ID, generate a barcode, and save it as BMP.
        foreach (string id in productIds)
        {
            // Initialize the barcode generator with Code 16K symbology and the current ID.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, id))
            {
                // Configure barcode appearance: set module (X) dimension in pixels.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Set a recommended aspect ratio (>8) for better readability.
                generator.Parameters.Barcode.Code16K.AspectRatio = 10f;

                // Build a unique file name that includes the counter and product ID.
                string fileName = $"Product_{++count}_{id}.bmp";
                string filePath = Path.Combine(outputFolder, fileName);

                // Save the generated barcode image in BMP format.
                generator.Save(filePath, BarCodeImageFormat.Bmp);

                // Output the location of the saved file for user reference.
                Console.WriteLine($"Saved barcode for '{id}' to '{filePath}'");
            }
        }

        // Inform the user where all barcode images have been stored.
        Console.WriteLine($"All barcodes have been saved to: {outputFolder}");
    }
}