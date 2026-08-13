// Title: Generate QR Code barcode and verify readability with Aspose.BarCode
// Description: Demonstrates creating a QR Code image, saving it as PNG, and reading it back to confirm that the barcode can be decoded.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator for QR Code creation and BarCodeReader for decoding. Typical use cases include embedding QR Codes in documents or applications and validating them programmatically. Developers often need to adjust error correction levels, module size, and verify scan results using the same library.
// Prompt: Generate a QR Code barcode and verify readability with external scanner library.
// Tags: qr code, barcode generation, barcode recognition, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation and subsequent reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code image, saves it, reads it back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare output folder and file path
        string folderPath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        string filePath = Path.Combine(folderPath, "qr.png");

        // Ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // ------------------- Generate QR Code -------------------
        // Create a BarcodeGenerator for QR encoding with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose QR"))
        {
            // Set QR specific parameters (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Medium error correction
            generator.Parameters.Barcode.XDimension.Point = 3f; // Module size

            // Save the barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to generate the QR code image.");
            return;
        }

        // ------------------- Read QR Code -------------------
        // DecodeType.QR returns a BaseDecodeType instance for QR codes
        BaseDecodeType decodeType = DecodeType.QR;

        // Use BarCodeReader to decode the generated image
        using (var reader = new BarCodeReader(filePath, decodeType))
        {
            bool anyFound = false;
            foreach (var result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                Console.WriteLine();
            }

            if (!anyFound)
            {
                Console.WriteLine("No barcode was detected in the generated image.");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(filePath);
            Directory.Delete(folderPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}