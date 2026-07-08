// Title: Batch generate Code 16K barcodes and save as BMP files
// Description: Demonstrates creating Code 16K barcodes for a collection of product IDs and storing each barcode as an individual BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code16K for batch processing. Typical use cases include inventory labeling, product tracking, and bulk barcode creation where developers need to automate image output in a specific format.
// Prompt: Batch generate Code 16K barcodes for product ID list, storing each as BMP file.
// Tags: code16k, barcode, generation, bmp, batch, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for generating a set of Code 16K barcodes from a list of product identifiers
/// and saving each barcode as a BMP image file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates Code 16K barcodes for predefined product IDs and writes each image to the "Barcodes" folder.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of product identifiers.
        List<string> productIds = new List<string>
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each product ID and generate a corresponding barcode.
        foreach (string id in productIds)
        {
            // Initialize a barcode generator for the Code 16K symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K))
            {
                // Assign the product ID as the text to encode in the barcode.
                generator.CodeText = id;

                // Optional: adjust the aspect ratio if required (default is 1.0).
                // generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                // Construct the full file path for the BMP output.
                string filePath = Path.Combine(outputDir, $"Product_{id}.bmp");

                // Save the generated barcode image in BMP format.
                generator.Save(filePath);
            }
        }

        // Notify the user that the batch operation has completed.
        Console.WriteLine("Barcode generation completed.");
    }
}