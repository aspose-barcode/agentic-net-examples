using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code with custom label and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Initialize a QR code generator with the desired encode type and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleData"))
        {
            // Set the QR error correction level to Medium (LevelM) to balance data capacity and resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Specify custom human‑readable text that will appear below the QR code instead of the raw codetext.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My QR Label";

            // Configure the font properties for the displayed human‑readable text.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Save the generated QR code image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code image saved to: {outputPath}");
    }
}