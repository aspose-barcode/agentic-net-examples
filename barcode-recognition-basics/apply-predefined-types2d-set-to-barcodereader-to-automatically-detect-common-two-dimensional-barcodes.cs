// Title: Detect 2D Barcodes Using BarCodeReader with Types2D Set
// Description: Generates a QR code image and reads it using BarCodeReader with the predefined Types2D decode set to automatically detect common 2‑dimensional barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates how to use the BarcodeGenerator class to create a QR code and the BarCodeReader class with DecodeType.Types2D to recognize a variety of 2D symbologies in a single call. Developers often need quick detection of QR, DataMatrix, PDF417 and other 2D barcodes without specifying each type individually.
// Prompt: Apply the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes.
// Tags: barcode, 2d, types2d, generation, reading, aspose.barcode, qrcode, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code and reading it with the predefined Types2D decode set.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, reads it using Types2D, and outputs the results.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample_qr.png");

        // Generate a QR code image with custom X‑dimension.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read barcodes from the image using the predefined Types2D set,
        // which automatically detects common 2D symbologies.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Types2D))
        {
            Console.WriteLine("Read Types2D results:");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional). Uncomment to delete the folder after execution.
        // Directory.Delete(tempDir, true);
    }
}