// Title: Generate Low-Resolution Barcode and Verify Readability
// Description: Demonstrates setting the BarcodeGenerator resolution to 72 dpi, creating a PNG barcode, and confirming it can be decoded.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator (for creating barcodes) and BarCodeReader (for decoding them). Typical scenarios include preparing barcodes for low‑resolution displays or printers and validating that they remain readable. Developers often need to adjust image resolution, choose appropriate symbologies, and verify output quality in automated tests.
// Prompt: Set BarcodeGenerator resolution to 72 dpi, test barcode generation meets low‑resolution display requirements.
// Tags: barcode symbology, generation, recognition, low-resolution, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a low‑resolution barcode image and verifies its readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode at 72 dpi, saves it as PNG, and reads it back to confirm detection.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define output file path
        string barcodePath = Path.Combine(tempFolder, "lowres_barcode.png");

        // Generate a barcode with 72 dpi resolution
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test72DPI"))
        {
            // Set low resolution (72 dots per inch)
            generator.Parameters.Resolution = 72f;
            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Verify that the barcode can be read back
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected at 72 dpi.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Read code: {result.CodeText}, Type: {result.CodeTypeName}");
                }
            }
        }
    }
}