using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code with Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code with version 40 and high error correction,
    /// then saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR Code image.
        string outputPath = "qr_version40.png";

        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose!"))
        {
            // Configure the QR Code to use the maximum version (40) for larger data capacity.
            generator.Parameters.Barcode.QR.Version = QRVersion.Version40;

            // Set the error correction level to high (Level H) to improve readability after damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}