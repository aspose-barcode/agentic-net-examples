// Title: Asynchronous QR Code Generation and Reading Example
// Description: Demonstrates generating a QR Code image and reading it asynchronously using Aspose.BarCode's BarCodeReader.ReadBarCodesAsync pattern for responsive desktop applications.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include desktop or mobile apps that need non‑blocking barcode processing to keep the UI responsive. Developers often work with EncodeTypes, DecodeType, BarCodeImageFormat, and the asynchronous reading pattern shown here.
// Prompt: Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in desktop applications.
// Tags: qr, barcode, generation, recognition, async, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code image, reads it asynchronously, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR Code barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Asynchronously read the barcode from the generated image
        await ReadBarcodesAsync(barcodePath);

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo execution
        }
    }

    /// <summary>
    /// Reads barcodes from the specified image file asynchronously.
    /// </summary>
    /// <param name="imagePath">Path to the image containing barcodes.</param>
    private static async Task ReadBarcodesAsync(string imagePath)
    {
        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Specify the decode type(s) you expect; here we look for QR codes
        BaseDecodeType decodeType = DecodeType.QR;

        // Initialize the barcode reader with the image path and expected decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Perform the synchronous read operation on a background thread to avoid blocking the UI
            BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Output details of each detected barcode
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Quality     : {result.ReadingQuality}");
                Console.WriteLine();
            }
        }
    }
}