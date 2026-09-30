// Title: Validate barcode readability after applying Interpolation mode at 300 dpi
// Description: Generates a Code128 barcode using interpolation auto‑size mode at 300 dpi, saves it as PNG, then reads the image back to confirm the barcode can be decoded.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to create a barcode image with specific rendering settings and BarCodeReader to scan the saved image. Developers working with barcode imaging often need to adjust resolution and scaling options (e.g., Interpolation) to meet printing or scanning requirements, and then verify that the resulting image remains readable.
// Prompt: Validate barcode readability after applying Interpolation mode at 300 dpi by scanning the saved image.
// Tags: code128, barcode generation, barcode recognition, interpolation, 300dpi, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with interpolation scaling at 300 dpi,
/// saving it to a temporary PNG file, and verifying its readability by scanning the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, reads, and validates a barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode PNG file
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode with interpolation auto‑size mode and 300 dpi resolution
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply interpolation scaling (auto‑size mode)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the image resolution to 300 dots per inch
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read and decode the barcode from the saved PNG file
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Perform the barcode reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Determine readability: at least one result with non‑empty text indicates success
            bool readable = results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);

            Console.WriteLine($"Barcode readability after Interpolation at 300 dpi: {(readable ? "Success" : "Failure")}");
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the example outcome
        }
    }
}