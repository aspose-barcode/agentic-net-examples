// Title: Generate QR Code with Embedded Plain Text
// Description: Demonstrates creating a QR Code barcode that encodes a short plain‑text note and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include encoding URLs, contact information, or quick notes for distribution. Developers often need to set QR error correction levels and specify text encoding before saving the barcode to common image formats.
// Prompt: Generate QR Code barcode and embed plain text message for quick note distribution.
// Tags: qr code, barcode generation, plain text, png, aspose.barcode, encode types, error correction

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode containing a plain‑text note and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code with a message, sets high error correction, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Define the plain‑text message to encode in the QR Code
        string message = "Quick note: meeting at 10am.";

        // Determine the output file path in the system's temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_note.png");

        // Initialize the barcode generator for QR Code with the specified message
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, message))
        {
            // Set a high error correction level (Level H) to improve readability after damage
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Ensure the message is encoded using UTF‑8 character set
            generator.SetCodeText(message, Encoding.UTF8);

            // Save the generated QR Code as a PNG image to the output path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved and display the original message
        Console.WriteLine($"QR code saved to: {outputPath}");
        Console.WriteLine($"Plain text message: {message}");
    }
}