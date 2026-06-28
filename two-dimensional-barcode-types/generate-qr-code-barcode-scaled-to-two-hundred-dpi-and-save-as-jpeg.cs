using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with a specific resolution using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image with 200 DPI resolution and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure the image resolution (dots per inch).
            generator.Parameters.Resolution = 200f;

            // Set the QR code error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code to a JPEG file.
            generator.Save("qr_200dpi.jpg");
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine("QR code saved as qr_200dpi.jpg with 200 DPI.");
    }
}