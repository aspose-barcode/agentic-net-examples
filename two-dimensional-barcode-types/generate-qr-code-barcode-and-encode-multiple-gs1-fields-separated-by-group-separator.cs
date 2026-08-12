// Title: Generate GS1 QR Code with multiple fields
// Description: This example creates a QR Code barcode that encodes several GS1 application identifiers (GTIN, batch/lot, serial number) separated by the GS (group separator) character.
// Category-Description: Demonstrates barcode generation using Aspose.BarCode, focusing on GS1 QR symbology. It shows how to build GS1 data strings, configure QR error correction, and save the result as an image. Developers working with product identification, inventory, or logistics often need to generate GS1-compliant QR codes using the BarcodeGenerator, QR parameters, and image format classes.
// Prompt: Generate QR Code barcode and encode multiple GS1 fields separated by group separator.
// Tags: qr, gs1, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 QR Code containing multiple application identifiers.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds GS1 data, creates a QR code, sets error correction, and saves the image.
    /// </summary>
    static void Main()
    {
        // Sample GS1 QR code data:
        // (01) – GTIN, (10) – Batch/Lot, (21) – Serial Number.
        // Variable‑length fields are separated by the GS (group separator) character \u001D.
        string gs1Data = "(01)12345678901231(10)ABC123\u001D(21)XYZ";

        // Create a QR code generator for the GS1 QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1QR, gs1Data))
        {
            // Optional: set a higher error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelQ;

            // Determine the output file path.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1qr.png");

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            Console.WriteLine($"GS1 QR code saved to: {outputPath}");
        }
    }
}