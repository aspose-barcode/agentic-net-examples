// Title: Generate UPC‑A barcode at 600 DPI and save as TIFF
// Description: Demonstrates creating a UPC‑A barcode, configuring the image resolution to 600 DPI, and saving the result as a TIFF file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce high‑resolution barcodes for product labeling, packaging, and print‑ready assets. Developers frequently need to adjust resolution for printing quality, select appropriate symbologies, and export to common image formats such as TIFF.
// Prompt: Set image resolution to 600 DPI and save a UPC‑A barcode as a TIFF file.
// Tags: upc-a, barcode, generation, resolution, tiff, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode, sets a 600 DPI resolution,
/// and saves the image as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Build the full path for the output TIFF file in the current directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "upc_a_600dpi.tiff");

        // Create a BarcodeGenerator for the UPC‑A symbology with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "123456789012"))
        {
            // Set the image resolution to 600 dots per inch.
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode as a TIFF image to the output path.
            generator.Save(outputFile, BarCodeImageFormat.Tiff);
        }

        // Write the location of the saved barcode to the console.
        Console.WriteLine($"UPC-A barcode saved to: {outputFile}");
    }
}