using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name and path
        string outputPath = "qr_300dpi.png";

        // Initialize the barcode generator for QR code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set image resolution to 300 DPI for high‑resolution output
            generator.Parameters.Resolution = 300f;

            // Configure QR code error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}