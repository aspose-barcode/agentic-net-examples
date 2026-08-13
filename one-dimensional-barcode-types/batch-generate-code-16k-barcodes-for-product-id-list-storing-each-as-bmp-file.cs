// Title: Batch generation of Code 16K barcodes to BMP files
// Description: Demonstrates how to generate Code 16K barcodes for a list of product IDs and save each barcode as a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and BarCodeImageFormat classes. Typical scenarios include bulk creation of product barcodes for inventory systems, packaging, or point‑of‑sale applications. Developers often need to automate barcode creation for multiple items and store them in common image formats such as BMP.
// Prompt: Batch generate Code 16K barcodes for product ID list, storing each as BMP file.
// Tags: code16k, barcode, generation, bmp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that creates Code 16K barcodes for a collection of product identifiers
/// and stores each barcode as a BMP image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that iterates over product IDs, generates corresponding Code 16K barcodes,
    /// and saves them as BMP files in a dedicated output folder.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product IDs to encode.
        string[] productIds = new[]
        {
            "PROD001",
            "PROD002",
            "PROD003",
            "PROD004",
            "PROD005"
        };

        // Determine the output directory for BMP files (creates "Barcodes" folder in the current directory).
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each product ID.
        foreach (string id in productIds)
        {
            // Initialize a BarcodeGenerator for the Code16K symbology.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K))
            {
                // Assign the product ID as the text to encode.
                generator.CodeText = id;

                // Optional: configure Code16K‑specific parameters.
                generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;          // Height/Width ratio.
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;   // Left quiet zone coefficient.
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;   // Right quiet zone coefficient.

                // Build the full file path and save the barcode as a BMP image.
                string filePath = Path.Combine(outputDir, $"{id}.bmp");
                generator.Save(filePath, BarCodeImageFormat.Bmp);
            }
        }
    }
}