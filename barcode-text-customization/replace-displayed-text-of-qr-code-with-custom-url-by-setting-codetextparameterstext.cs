using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with a custom display URL using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // The data that will be encoded inside the QR code.
        string encodedData = "SampleData";

        // The URL that will be shown as text below the QR code image.
        string displayUrl = "https://example.com";

        // Destination file path for the generated QR code image.
        string outputPath = "qr_code.png";

        // Initialize the QR barcode generator with the encoded data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, encodedData))
        {
            // Set the text that appears under the QR code (2‑D display text).
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = displayUrl;

            // Optionally configure the QR error correction level (Level M = ~15% correction).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Render and save the QR code image to the specified file.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}