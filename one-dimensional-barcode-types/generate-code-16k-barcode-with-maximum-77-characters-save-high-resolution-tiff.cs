// Title: Generate Code 16K barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a Code 16K barcode with the maximum 77‑character payload and exporting it to a 300 DPI TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as resolution, aspect ratio, and quiet zones using the BarcodeGenerator and related parameter classes. Typical use cases include producing high‑quality printable barcodes for packaging, shipping labels, or archival documents. Developers often need to adjust DPI and module sizing to meet printing standards, and this snippet shows the essential API calls.
// Prompt: Generate Code 16K barcode with maximum 77 characters, save high‑resolution TIFF.
// Tags: code16k, barcode, generation, tiff, highresolution, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code 16K barcode with the maximum allowed
/// 77 characters and saves it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its parameters,
    /// and writes the result to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text – exactly 77 characters, the maximum for Code 16K.
        string codeText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789AB";

        // Initialize the barcode generator for the Code 16K symbology with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the output resolution to 300 DPI for high‑quality printing.
            generator.Parameters.Resolution = 300f;

            // Configure Code 16K‑specific visual parameters.
            generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;          // Square modules.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;    // Left quiet zone coefficient.
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;    // Right quiet zone coefficient.

            // Save the generated barcode as a TIFF image with the specified resolution.
            generator.Save("code16k.tif");
        }

        // Inform the user that the file has been created.
        Console.WriteLine("Code16K barcode saved as 'code16k.tif'.");
    }
}