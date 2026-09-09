// Title: Generate QR Code barcode and save as PNG
// Description: Demonstrates creating a QR Code from alphanumeric text using Aspose.BarCode and saving it as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include encoding URLs, contact information, or product data for scanning by mobile devices. Developers often need to configure error correction levels and export the barcode to common image formats such as PNG, JPEG, or BMP.
// Prompt: Generate a QR Code barcode from alphanumeric text and save as PNG image file.
// Tags: qr code, barcode generation, png output, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode from a text string and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a BarcodeGenerator for QR encoding, sets error correction level, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.png");
        try
        {
            // Initialize generator with QR type and the data to encode
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World 123"))
            {
                // Set QR error correction level to Medium (Level M)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                // Save the generated barcode as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
            // Inform user of successful save
            Console.WriteLine($"QR Code saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error generating QR Code: {ex.Message}");
        }
    }
}