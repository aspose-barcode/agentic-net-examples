// Title: Generate a UPC‑A barcode at 600 DPI and save as TIFF
// Description: Demonstrates how to create a UPC‑A barcode, set the image resolution to 600 DPI, and save the result as a TIFF file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce high‑resolution barcode images. Typical scenarios include printing barcodes on product packaging, labels, or documents where precise image quality is required. Developers often need to adjust resolution, select symbology, and choose an appropriate output format for downstream processing.
// Prompt: Set image resolution to 600 DPI and save a UPC‑A barcode as a TIFF file.
// Tags: upc-a, barcode, generation, tiff, resolution, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a UPC‑A barcode, configures a 600 DPI image resolution,
/// and saves the barcode as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, generates the barcode,
    /// sets the desired resolution, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory and file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "upc_a.tiff");

        // Create a UPC‑A barcode generator with a valid 12‑digit code
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "123456789012"))
        {
            // Set the image resolution to 600 DPI
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}