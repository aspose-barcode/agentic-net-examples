// Title: Generate QR Code with embedded vCard for digital business card
// Description: Creates a QR code containing vCard contact information and saves it as a PNG file.
// Category-Description: This example demonstrates how to use Aspose.BarCode's Generation API to produce QR Code barcodes. It shows how to embed structured vCard data (Version 3.0) into the QR code, configure UTF‑8 encoding and high error correction, and export the result as a PNG image. Developers working with digital business cards, contact sharing, or any scenario requiring QR‑encoded contact details will find this pattern useful.
// Prompt: Generate QR Code barcode and embed vCard contact information for digital business card.
// Tags: qr code, vcard, barcode generation, aspose.barcode, png, contact information

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code that encodes a vCard and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code and reports the output location.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "vcard_qr.png");

        try
        {
            // Generate the QR code with embedded vCard data.
            GenerateVCardQr(outputPath);

            // Inform the user where the image was saved.
            Console.WriteLine($"QR code saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occurred during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a QR Code containing a simple vCard and writes it to the specified file.
    /// </summary>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    static void GenerateVCardQr(string outputPath)
    {
        // Ensure the target directory exists.
        string? dir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Build a minimal vCard (Version 3.0) with basic contact fields.
        string vCard = "BEGIN:VCARD\r\n" +
                       "VERSION:3.0\r\n" +
                       "N:Doe;John;;;\r\n" +
                       "FN:John Doe\r\n" +
                       "ORG:Example Company\r\n" +
                       "TITLE:Software Engineer\r\n" +
                       "TEL;TYPE=WORK,VOICE:+1-111-555-0100\r\n" +
                       "EMAIL:john.doe@example.com\r\n" +
                       "END:VCARD";

        // Initialize the QR code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the vCard string as the code text to be encoded.
            generator.CodeText = vCard;

            // Use UTF‑8 encoding to support the full character set.
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Set a high error correction level (Level H) for better scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}