// Title: Generate UPC-A barcode with 600 DPI resolution and save as TIFF
// Description: Demonstrates creating a UPC-A barcode, setting the image resolution to 600 DPI, and saving the result as a TIFF file.
// Prompt: Set image resolution to 600 DPI and save a UPC‑A barcode as a TIFF file.
// Tags: barcode, upc-a, resolution, tiff, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a UPC‑A barcode, configures a high‑resolution output,
/// and saves the image as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the UPC‑A symbology with a valid 12‑digit value.
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
        {
            // Configure the image resolution to 600 DPI for high‑quality output.
            generator.Parameters.Resolution = 600f;

            // Persist the generated barcode as a TIFF image file.
            generator.Save("upc_a.tiff");
        }
    }
}