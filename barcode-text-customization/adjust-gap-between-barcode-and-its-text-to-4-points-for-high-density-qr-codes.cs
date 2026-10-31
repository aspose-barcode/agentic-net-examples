// Title: Adjust gap between QR code and text for high‑density QR codes
// Description: Demonstrates how to set a 4‑point gap between a QR code and its human‑readable text, useful when generating high‑density QR symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, CodeTextParameters, and image saving. Developers often need to fine‑tune text placement and spacing for dense barcodes to ensure readability and visual balance. The snippet shows typical steps for configuring QR code appearance and exporting to PNG.
// Prompt: Adjust the gap between barcode and its text to 4 points for high‑density QR codes.
// Tags: qr code, gap adjustment, png, barcodelibrary, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a high‑density QR code and sets a custom gap between the barcode and its text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the QR code, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "HighDensityQR.png");

        // Sample high‑density data to increase QR code density
        string data = new string('A', 2000);

        // Initialize the barcode generator for a QR code with the provided data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, data))
        {
            // Set the gap between barcode and its text to 4 points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;
            // Position the text below the barcode (optional)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}