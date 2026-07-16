// Title: Generate QR Code and Save as PNG
// Description: This example creates a QR Code barcode from an alphanumeric string and writes it to a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.QR. Typical scenarios include creating QR codes for URLs, product IDs, or contact information and exporting them as image files (PNG, JPEG, etc.). Developers often need to adjust error correction levels and save the result for web or print use.
// Prompt: Generate a QR Code barcode from alphanumeric text and save as PNG image file.
// Tags: qr code, barcode generation, png output, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode from alphanumeric text
/// and saves it as a PNG image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the QR Code and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr_code.png";

        // Initialize the barcode generator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Optional: set a high error correction level to improve readability under damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code as a PNG image to the specified path.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code image saved to: {outputPath}");
    }
}