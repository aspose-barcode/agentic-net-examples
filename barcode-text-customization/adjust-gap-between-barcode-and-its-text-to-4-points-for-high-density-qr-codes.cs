using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generation of a high‑density QR code with a custom text gap using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, configures its parameters, saves it to a file, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HighDensityQR"))
        {
            // Configure the space (gap) between the barcode and its human‑readable text to 4 points.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Set a higher error correction level (Level H) to increase data density and resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the generated QR code as a PNG image file.
            generator.Save("HighDensityQR.png");
        }

        // Inform the user that the QR code has been successfully generated.
        Console.WriteLine("QR code generated with 4‑point text gap.");
    }
}