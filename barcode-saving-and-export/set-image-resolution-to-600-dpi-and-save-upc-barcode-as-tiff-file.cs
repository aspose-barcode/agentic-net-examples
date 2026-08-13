// Title: Generate a 600 DPI UPC‑A barcode and save as TIFF
// Description: Demonstrates setting a high image resolution and exporting a UPC‑A barcode to a TIFF file using Aspose.BarCode.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode, illustrating how to configure image resolution and output format. It uses the BarcodeGenerator class with EncodeTypes to create common symbologies, a typical task for developers needing high‑quality printable barcodes in formats like TIFF.
// Prompt: Set image resolution to 600 DPI and save a UPC‑A barcode as a TIFF file.
// Tags: upc-a, barcode, resolution, tiff, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a UPC‑A barcode, sets the image resolution to 600 DPI,
/// and saves the result as a TIFF file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the UPC‑A symbology with an 11‑digit code.
        // The check digit will be calculated automatically.
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "01234567890"))
        {
            // Configure the output image resolution to 600 DPI.
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode as a TIFF image file.
            generator.Save("upc_a.tiff");
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine("UPC-A barcode saved as 'upc_a.tiff' with 600 DPI resolution.");
    }
}