// Title: Asynchronous barcode reading from an image file
// Description: Demonstrates generating a Code128 barcode image and reading it asynchronously to keep the UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to use BarcodeGenerator to create barcodes and BarCodeReader with async patterns for non‑blocking operations. Developers often need to process uploaded images without freezing the UI, using classes like BarcodeGenerator, BarCodeReader, and DecodeType.
// Prompt: Use asynchronous BarCodeReader methods to read uploaded files while preserving UI responsiveness.
// Tags: code128, read, png, barcodegenerator, barcodereader, async, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image and reads it asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode, reads it asynchronously,
    /// and then cleans up the temporary file.
    /// </summary>
    static async Task Main()
    {
        // ------------------------------------------------------------
        // Generate a temporary barcode image (Code128) and save as PNG
        // ------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "sample.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Asynchronously read the barcode from the generated image
        // ------------------------------------------------------------
        await ReadBarcodeAsync(imagePath);

        // ------------------------------------------------------------
        // Clean up the temporary file
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary file: {ex.Message}");
        }
    }

    /// <summary>
    /// Reads barcodes from the specified file path using a background thread to avoid blocking the UI.
    /// </summary>
    /// <param name="filePath">Full path to the image file containing barcodes.</param>
    private static async Task ReadBarcodeAsync(string filePath)
    {
        // Verify that the file exists before attempting to read
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Run the blocking reading operation on a background thread
        await Task.Run(() =>
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and text
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Detected Text: {result.CodeText}");
                }
            }
        });
    }
}