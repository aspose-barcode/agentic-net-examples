// Title: Asynchronous QR Code Generation and Reading Example
// Description: Demonstrates generating a QR code image and reading it asynchronously using Aspose.BarCode, suitable for responsive desktop UI scenarios.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. It highlights asynchronous processing with Task.Run to keep UI threads responsive, a common requirement for desktop applications that need non‑blocking barcode scanning.
// Prompt: Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in desktop applications.
// Tags: qr, barcode generation, barcode recognition, async, task, aspose.barcode, desktop ui

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous barcode generation and reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, saves it to a temporary file, reads it asynchronously, and cleans up.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR code with the text "Hello Aspose" and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Asynchronously read the barcode from the saved image
        await ReadBarcodeAsync(barcodePath);

        // Clean up temporary files and folder; ignore any errors during deletion
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // ignore cleanup errors
        }
    }

    /// <summary>
    /// Reads barcodes from the specified image file asynchronously.
    /// </summary>
    /// <param name="imagePath">Full path to the image containing barcodes.</param>
    static async Task ReadBarcodeAsync(string imagePath)
    {
        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("File not found: " + imagePath);
            return;
        }

        // Initialize the barcode reader for all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Use high‑performance quality settings for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the synchronous read operation on a background thread to avoid blocking
            BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

            // Check if any barcodes were detected
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Output each detected barcode's type and decoded text
            foreach (var result in results)
            {
                Console.WriteLine($"Detected {result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}