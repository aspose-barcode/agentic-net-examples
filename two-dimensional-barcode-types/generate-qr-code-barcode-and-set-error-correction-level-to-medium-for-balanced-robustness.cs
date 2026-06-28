using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code with medium error correction level and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_medium.png";

        // Initialize a BarcodeGenerator for QR encoding with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Configure the QR code to use medium error correction (LevelM).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Persist the QR code image to the specified file.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}