using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a multilingual QR code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code with multilingual display text and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the same folder as the executable)
        string outputPath = "qr_multilingual.png";

        // Initialize a QR code generator within a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to be encoded in the QR code
            generator.CodeText = "SampleData123";

            // Set the human‑readable text displayed beneath the QR code (multilingual)
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Hello 世界 مرحبا";

            // Optionally adjust the QR error correction level (default is LevelL)
            // generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code image as a PNG file
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}