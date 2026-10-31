// Title: QR Code Generation and Decoding Example
// Description: Demonstrates generating a QR code image and decoding it while restricting recognition to QR symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes and BarCodeReader with a specific DecodeType to limit scanning to QR symbology. Developers often need to generate barcodes for data encoding and then read them back, especially when focusing on a single symbology to improve performance and accuracy.
// Prompt: Set DecodeType to QR before reading an image to limit recognition to QR symbology only.
// Tags: qr, barcode, generation, recognition, decode type, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image, saving it, and decoding it using Aspose.BarCode while limiting the decode operation to QR symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary QR code image, reads it with a QR‑only decoder, outputs the result, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the QR code image
        string imagePath = Path.Combine(tempFolder, "qr.png");

        // Generate a QR code image containing the text "Hello World"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set DecodeType to QR to restrict recognition to QR codes only
        BaseDecodeType decodeType = DecodeType.QR;

        // Read the QR code from the saved image
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}