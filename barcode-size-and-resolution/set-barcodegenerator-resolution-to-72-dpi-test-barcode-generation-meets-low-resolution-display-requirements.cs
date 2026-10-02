// Title: Low‑Resolution Barcode Generation and Verification
// Description: Demonstrates generating a Code128 barcode at 72 dpi and saving it as PNG, then reading it back to confirm readability on low‑resolution displays.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to configure the BarcodeGenerator resolution for low‑dpi scenarios. It uses BarcodeGenerator, BarCodeImageFormat, and BarCodeReader classes, typical for developers needing to produce barcodes for screens or printers with limited resolution. The pattern is common when validating that barcodes remain scannable after down‑sampling.
// Prompt: Set BarcodeGenerator resolution to 72 dpi, test barcode generation meets low‑resolution display requirements.
// Tags: code128, resolution, lowdpi, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting barcode generation resolution to 72 dpi and verifying readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a low‑resolution barcode, saves it, and reads it back.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo");
        Directory.CreateDirectory(tempDir);

        // Define the full path for the output PNG file
        string barcodePath = Path.Combine(tempDir, "lowres_barcode.png");

        // Generate a Code128 barcode with a resolution of 72 dpi
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Resolution = 72f; // Set low resolution
            generator.Save(barcodePath, BarCodeImageFormat.Png); // Save as PNG
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // Verify that the saved barcode can be read back correctly
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Read CodeText: {result.CodeText}");
                    Console.WriteLine($"Read CodeType: {result.CodeTypeName}");
                }
            }
        }
    }
}