using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code with sample text,
    /// sets a high error correction level, saves it as a PNG file, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Path where the generated QR code image will be saved
        string outputPath = "qr.png";

        // Initialize the barcode generator for a QR code with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Configure the QR code to use the highest error correction level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the generated QR code image to the specified file in PNG format
            generator.Save(outputPath);
        }

        // Inform the user about the location of the saved QR code image
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}