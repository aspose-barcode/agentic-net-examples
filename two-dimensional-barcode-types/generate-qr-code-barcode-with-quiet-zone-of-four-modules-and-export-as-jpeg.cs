using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code for a sample URL and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_code.jpeg";

        // Initialize a QR Code generator with the desired content (sample URL).
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR code error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Set the image resolution to 300 DPI to improve visual quality.
            generator.Parameters.Resolution = 300f;

            // Note: Aspose.BarCode uses a default quiet zone of 4 modules for QR codes,
            // which meets typical requirements; no additional configuration needed.

            // Save the generated QR code as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the QR code has been successfully saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}