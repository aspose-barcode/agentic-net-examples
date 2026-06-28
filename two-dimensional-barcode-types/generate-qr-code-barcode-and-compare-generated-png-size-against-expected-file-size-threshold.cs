using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code image, verifying its file size,
/// and cleaning up the generated file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, checks its size against a threshold,
    /// and optionally deletes the created file.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the QR code image.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_code.png");

        // Expected maximum file size in bytes (5 KB).
        const long sizeThreshold = 5000L;

        // Create a QR code generator with the desired content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set error correction level if desired.
            // generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate the QR code image.");
            return;
        }

        // Retrieve the actual file size of the generated image.
        long actualSize = new FileInfo(outputPath).Length;

        // Compare the actual size with the defined threshold and report the result.
        if (actualSize <= sizeThreshold)
        {
            Console.WriteLine($"Success: QR code image size ({actualSize} bytes) is within the threshold ({sizeThreshold} bytes).");
        }
        else
        {
            Console.WriteLine($"Warning: QR code image size ({actualSize} bytes) exceeds the threshold ({sizeThreshold} bytes).");
        }

        // Attempt to delete the generated file; ignore any errors that occur during cleanup.
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Cleanup errors are intentionally ignored.
        }
    }
}