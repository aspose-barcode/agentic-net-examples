// Title: Large XDimension Barcode Generation and Recognition
// Description: Demonstrates generating a Code128 barcode with an XDimension larger than 10 pixels and recognizing it using the Large XDimension mode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating high‑resolution barcodes and BarCodeReader with XDimensionMode.Large for accurate decoding. Developers working with large‑module barcodes (e.g., industrial labeling, security printing) often need to adjust XDimension and quality settings to ensure reliable scanning.
// Prompt: Test recognition of very large barcodes (>10 pixels XDimension) using Large mode configuration.
// Tags: barcode, symbology, code128, generation, recognition, largexdimension, aspose.barcode, csharp, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a large XDimension
/// and then reads it back using the Large XDimension mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it to a temporary file,
    /// reads it using large‑module settings, and outputs the results to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "LargeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "large_barcode.png");

        // ---------------------------------------------------------------
        // Generate a Code128 barcode with XDimension set to 12 pixels.
        // ---------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "LargeXDimTest12345"))
        {
            // XDimension.Point defines the size of a single module in points (1 point = 1/72 inch).
            // Setting it to 12 results in approximately 12 pixels per module at 72 DPI.
            generator.Parameters.Barcode.XDimension.Point = 12f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // Read the generated barcode using the Large XDimension mode.
        // ---------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Configure the reader to expect large modules, improving detection accuracy.
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Perform the recognition and retrieve all detected barcodes.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes found and their details.
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Optional cleanup: delete the temporary folder and its contents.
        // Uncomment the line below to enable automatic cleanup.
        // --------------------------------------------------------------------
        // Directory.Delete(tempDir, true);
    }
}