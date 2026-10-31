// Title: Generate a 600 DPI UPC‑A barcode and save as TIFF
// Description: Demonstrates how to create a UPC‑A barcode with a high image resolution of 600 DPI and store it in TIFF format using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include producing high‑resolution barcodes for print media, packaging, or inventory systems where precise DPI settings are required. Developers often need to adjust resolution and choose appropriate file formats for downstream processing.
// Prompt: Set image resolution to 600 DPI and save a UPC‑A barcode as a TIFF file.
// Tags: upc-a, barcode generation, tiff, resolution, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A barcode at 600 DPI and saving it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, sets resolution, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output TIFF file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "upc_a.tiff");

        // Initialize the barcode generator for UPC‑A with a sample numeric code
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.UPCA, "123456789012"))
        {
            // Configure the image resolution to 600 DPI for high‑quality output
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode image in TIFF format to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"UPC-A barcode saved to: {outputPath}");
    }
}