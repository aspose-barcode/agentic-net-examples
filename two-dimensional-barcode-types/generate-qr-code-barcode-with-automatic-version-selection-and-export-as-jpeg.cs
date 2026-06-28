using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code for a sample URL and writes it to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_code.jpeg";

        // Initialize a BarcodeGenerator for QR encoding with the desired text (URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR version to automatic selection (default behavior) to let the library choose the optimal size.
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;

            // Save the generated QR code image to the specified path in JPEG format.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}