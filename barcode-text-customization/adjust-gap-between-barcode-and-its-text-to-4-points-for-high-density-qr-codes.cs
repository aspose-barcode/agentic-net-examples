// Title: Adjust QR Code Text Gap for High‑Density QR Codes
// Description: Demonstrates how to set the spacing between a QR code and its human‑readable text to 4 points, using Aspose.BarCode for a high‑density QR with error correction level H.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR code parameters such as error correction level, text location, and text‑barcode gap. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters classes—common tasks for developers creating printable or screen‑displayed barcodes with customized appearance.
// Prompt: Adjust the gap between barcode and its text to 4 points for high‑density QR codes.
// Tags: qr, barcode, text-gap, high-density, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a high‑density QR code and sets a 4‑point gap between the barcode and its human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, configures its parameters, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "high_density_qr.png");

        // Initialize the QR code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level (Level H) to increase data density.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Position the human‑readable text below the QR code.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Adjust the gap (space) between the QR code and its text to 4 points.
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Save the generated QR code image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}