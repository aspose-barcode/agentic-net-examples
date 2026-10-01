// Title: Asynchronous QR Code Generation and Reading Example
// Description: Demonstrates generating a QR barcode image and reading it asynchronously using Aspose.BarCode to keep UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting asynchronous reading with Task.Run to avoid blocking the UI thread. Developers building desktop or mobile applications often need non‑blocking barcode processing for smooth user experiences.
// Prompt: Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in desktop applications.
// Tags: qr, barcode, async, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, saves it, then reads it asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeAsyncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Asynchronously read the barcode from the image
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Offload the synchronous ReadBarCodes call to a background thread
            BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}