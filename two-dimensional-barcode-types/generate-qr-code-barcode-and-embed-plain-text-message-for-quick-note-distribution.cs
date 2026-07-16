// Title: Generate QR Code with embedded plain text note
// Description: Demonstrates creating a QR Code barcode containing a short text message and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include encoding URLs, contact info, or quick notes for distribution. Developers often need to set error correction levels, adjust module size, and export the barcode to common image formats.
// Prompt: Generate QR Code barcode and embed plain text message for quick note distribution.
// Tags: qr code, barcode generation, plain text, png, aspose.barcode, encode types, error correction

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code containing a plain‑text note and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_note.png";

        // Initialize a QR code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text to be encoded in the QR code.
            generator.CodeText = "Quick note: Meeting at 3 PM";

            // Configure a high error correction level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Optionally adjust the size of each QR module (pixel size) to 3 points.
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Save the generated QR code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}