using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code from a sample text and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the alphanumeric text to encode into the QR code.
        string codeText = "Hello World 123";

        // Build the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");

        // Specify the barcode type as QR code.
        BaseEncodeType encodeType = EncodeTypes.QR;

        // Initialize the barcode generator with the chosen type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optionally set the QR error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}