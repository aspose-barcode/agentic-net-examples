// Title: High‑Resolution Barcode Generation and TIFF Export
// Description: Demonstrates configuring Aspose.BarCode to generate a Code128 barcode at 300 DPI and save it as a TIFF image suitable for printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to set resolution and output format using the BarcodeGenerator class. Developers often need to produce high‑quality printable barcodes, adjust DPI, and export to formats like TIFF for integration into print workflows.
// Prompt: Configure the barcode generator for high resolution (300 DPI) and produce TIFF images for printing.
// Tags: barcode, code128, high resolution, tiff, generation, aspose.barcode, resolution, image format

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a high‑resolution Code128 barcode and saves it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the barcode content and output file name
        const string codeText = "1234567890";
        const string outputFile = "barcode.tiff";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure the generator for printing quality (300 DPI)
            generator.Parameters.Resolution = 300f;

            // Export the barcode as a TIFF image
            generator.Save(outputFile, BarCodeImageFormat.Tiff);
        }

        // Inform the user that the file has been created
        Console.WriteLine($"Barcode saved to {outputFile}");
    }
}