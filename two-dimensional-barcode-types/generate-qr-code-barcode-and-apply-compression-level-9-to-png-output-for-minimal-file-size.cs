using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code for a sample URL and writes it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the application's base directory.
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qr.png");

        // Initialize a QR code generator with the desired content (sample URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR code to use the highest error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the generated QR code as a PNG image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");

        // Note about PNG compression: Aspose.BarCode does not provide a direct API to adjust compression.
        Console.WriteLine("Note: Aspose.BarCode does not expose an API to set PNG compression level. The image is saved with the library's default compression.");
    }
}