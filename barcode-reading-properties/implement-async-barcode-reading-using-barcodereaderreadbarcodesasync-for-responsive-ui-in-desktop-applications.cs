// Title: Asynchronous QR Code Generation and Reading Example
// Description: Demonstrates generating a QR barcode image and reading it asynchronously using Aspose.BarCode's BarCodeReader pattern for responsive desktop applications.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader with DecodeType for recognizing them, and asynchronous patterns (Task.Run) to keep UI threads responsive. Developers often need to generate barcodes on‑the‑fly and decode them without blocking the UI, especially in desktop or mobile apps.
// Prompt: Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in desktop applications.
// Tags: qr, barcode, async, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous barcode generation and reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, saves it, and reads it asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Create a temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeAsyncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Asynchronously read barcodes from the generated image
        await ReadBarcodesAsync(barcodePath);

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the demo outcome
        }
    }

    /// <summary>
    /// Reads barcodes from the specified image file asynchronously.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image.</param>
    /// <returns>A task representing the asynchronous read operation.</returns>
    private static async Task ReadBarcodesAsync(string imagePath)
    {
        // Perform the blocking read operation on a background thread
        BarCodeResult[] results = await Task.Run(() =>
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                return reader.ReadBarCodes();
            }
        });

        // Check if any barcodes were detected
        if (results == null || results.Length == 0)
        {
            Console.WriteLine("No barcodes detected.");
            return;
        }

        // Output each detected barcode's type and text
        foreach (var result in results)
        {
            Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
        }
    }
}