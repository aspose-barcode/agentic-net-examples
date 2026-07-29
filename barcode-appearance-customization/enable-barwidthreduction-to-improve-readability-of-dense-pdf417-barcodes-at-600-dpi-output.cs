// Title: Enable BarWidthReduction for dense PDF417 barcode at 600 dpi
// Description: Demonstrates how to generate a high‑resolution PDF417 barcode and improve its readability by applying bar‑width reduction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on PDF417 symbology configuration. It shows how to adjust resolution and bar‑width reduction using the BarcodeGenerator and its Parameters properties. Developers creating dense PDF417 barcodes for print or high‑dpi displays often need these settings to ensure scannability.
// Prompt: Enable BarWidthReduction to improve readability of dense PDF417 barcodes at 600 dpi output.
// Tags: pdf417, barwidthreduction, resolution, png, barcodegenerator, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a PDF417 barcode with bar‑width reduction applied to improve readability at 600 dpi.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a PDF417 barcode, configures resolution and bar‑width reduction, and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Sample dense data for PDF417
        string codeText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                          "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                          "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

        // Initialize a PDF417 barcode generator with the sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the output resolution to 600 dpi for high‑quality rendering
            generator.Parameters.Resolution = 600;

            // Enable bar‑width reduction (in points) to make dense barcodes more readable
            // Adjust the value as needed; 0.5 points is a typical starting point
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.5f;

            // Save the generated barcode as a PNG image file
            generator.Save("pdf417.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("PDF417 barcode generated with BarWidthReduction at 600 dpi.");
    }
}