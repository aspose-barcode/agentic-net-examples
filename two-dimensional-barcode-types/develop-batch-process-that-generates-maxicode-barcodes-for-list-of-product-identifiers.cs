// Title: Batch generation of MaxiCode barcodes
// Description: Demonstrates how to generate MaxiCode barcodes for multiple product identifiers and save them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator and MaxiCodeStandardCodetext classes. It illustrates typical batch processing scenarios where developers need to produce MaxiCode (Mode 4) images for inventory or shipping labels, saving each barcode as a PNG file for downstream systems.
// Prompt: Develop a batch process that generates MaxiCode barcodes for a list of product identifiers.
// Tags: maxicode, batch, png, complexbarcode, generation, aspnet, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a console application that creates MaxiCode barcodes for a collection of product IDs
/// and stores each barcode as a PNG image in a dedicated output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a predefined list of product identifiers,
    /// generates a MaxiCode (Mode 4) barcode for each, and saves the resulting image to disk.
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

        // Specify the output directory where barcode images will be saved.
        string outputDir = "MaxiCodeBarcodes";

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each product identifier individually.
        foreach (string id in productIds)
        {
            // Configure the MaxiCode codetext: Mode 4 with the product ID as the message.
            var maxiCodeCodetext = new MaxiCodeStandardCodetext
            {
                Mode = MaxiCodeMode.Mode4,
                Message = id
            };

            // Initialize the ComplexBarcodeGenerator with the prepared codetext.
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Generate the barcode image in memory (required before saving).
                generator.GenerateBarCodeImage();

                // Build the full file path for the PNG output.
                string filePath = Path.Combine(outputDir, $"MaxiCode_{id}.png");

                // Save the generated barcode image to the specified file.
                generator.Save(filePath);

                // Inform the user that the barcode has been created.
                Console.WriteLine($"Generated MaxiCode for '{id}' at '{filePath}'.");
            }
        }
    }
}