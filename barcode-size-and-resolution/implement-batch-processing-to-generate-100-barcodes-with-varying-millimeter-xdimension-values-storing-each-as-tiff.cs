// Title: Batch Generation of Code128 Barcodes with Varying XDimension Saved as TIFF
// Description: Demonstrates creating 100 Code128 barcodes, each with a different XDimension measured in millimeters, and saving them as TIFF images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce multiple barcodes in a batch. Typical use cases include bulk creation of product labels, inventory tags, or QR codes where each item requires a unique size or dimension. Developers often need to adjust parameters like XDimension, image format, and auto‑size mode while iterating over large datasets.
// Prompt: Implement batch processing to generate 100 barcodes with varying Millimeter XDimension values, storing each as TIFF.
// Tags: code128, barcode, batch-processing, tiff, xdimension, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of Code128 barcodes with incrementally changing XDimension values
/// and saves each barcode as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates 100 barcode images with varying XDimension
    /// measured in millimeters and stores them in a dedicated output folder.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Number of barcodes to generate (batch size)
        int sampleCount = 100; // change to desired batch size

        // Loop through the batch, creating each barcode with a unique XDimension
        for (int i = 1; i <= sampleCount; i++)
        {
            // Initialize a new BarcodeGenerator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode (e.g., Sample001, Sample002, ...)
                generator.CodeText = $"Sample{i:D3}";

                // Vary XDimension in millimeters (0.5mm increments per barcode)
                generator.Parameters.Barcode.XDimension.Millimeters = i * 0.5f;

                // Use interpolation mode to automatically adjust image size if needed
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Construct the full file path for the TIFF image
                string filePath = Path.Combine(outputFolder, $"barcode_{i:D3}.tiff");

                // Save the generated barcode as a TIFF file
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }

        // Output completion message to the console
        Console.WriteLine($"Generated {sampleCount} barcode images in '{outputFolder}'.");
    }
}